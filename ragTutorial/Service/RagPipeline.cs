using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.AI;

namespace ragTutorial.Service;
public class RagPipeline
{
    private readonly IEmbeddingGenerator<string,Embedding<float>> _embeddingGenerator;
    private readonly IVectorStoreService _vectorStore;
    private readonly IChatClient _chatClient;
    public RagPipeline(IEmbeddingGenerator<string,Embedding<float>> embeddingGenerator,IVectorStoreService vectorStore,IChatClient chatClient)
    {
        _embeddingGenerator = embeddingGenerator;
        _vectorStore = vectorStore;
        _chatClient = chatClient;
    }

    public async Task<string> AskAsync(string question, int topK = 3, CancellationToken cancellationToken = default)
    {
        // Generate embedding for the question
        var questionEmbedding = await _embeddingGenerator.GenerateAsync([question], cancellationToken: cancellationToken);
        var vector = questionEmbedding[0].Vector;
        // Search for relevant knowledge chunks
        var relevantChunks = await _vectorStore.SearchAsync(vector, topK, cancellationToken);
        // Combine the content of the relevant chunks
        var context = string.Join(
        "\n\n--- SOURCE ---\n\n",
        relevantChunks.Select(x =>
            $"""
            Title: {x.Title}
            {x.Content}
            """));

        var prompt =
            $"""
        You are a helpful enterprise assistant.

        Answer the user's question using ONLY
        the provided context.

        If the answer cannot be found in the context,
        say that the information is not available.

        Context:
        {context}
        """;
        List<ChatMessage> chat = [
                new ChatMessage(ChatRole.System, prompt),
                new ChatMessage(ChatRole.User, question)
        ];
        // Get the answer from the chat model
        var response = await _chatClient.GetResponseAsync(chat, cancellationToken : cancellationToken);
        return response.Text;
    }

}
