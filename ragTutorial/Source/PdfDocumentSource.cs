using RagMultiSource.Ingestion;
using System.Runtime.CompilerServices;
using System.Text;
using UglyToad.PdfPig;

namespace ragTutorial.Source;

public class PdfDocumentSource(IEnumerable<string> filePaths) : IDocumentSource
{
    public async IAsyncEnumerable<SourceDocument> LoadAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        foreach (var path in filePaths)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"[PdfDocumentSource] Skipping missing file: {path}");
                continue;
            }

            // PdfPig's API is synchronous; for large batches, wrap in Task.Run to avoid
            // blocking the async pipeline while parsing big PDFs.
            var text = await Task.Run(() =>
            {
                var sb = new StringBuilder();
                using var pdf = PdfDocument.Open(path);
                foreach (var page in pdf.GetPages())
                {
                    sb.AppendLine(page.Text);
                }
                return sb.ToString();
            }, cancellationToken);

            yield return new SourceDocument("Pdf", path, Path.GetFileNameWithoutExtension(path), text);
        }
    }
}
