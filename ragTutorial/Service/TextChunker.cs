namespace ragTutorial.Service;

public class TextChunker
{
    public IEnumerable<string> Split(
        string text,
        int chunkSize = 1000,
        int overlap = 150)
    {
        if (string.IsNullOrWhiteSpace(text))
            yield break;

        var start = 0;

        while (start < text.Length)
        {
            var length = Math.Min(
                chunkSize,
                text.Length - start);

            yield return text.Substring(start, length);

            start += chunkSize - overlap;
        }
    }
}
