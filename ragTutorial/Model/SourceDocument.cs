namespace RagMultiSource.Ingestion;

/// <summary>
/// One raw document as read from any source, before chunking.
/// SourceId should be stable and unique within its SourceType (a URL, file path, or DB row id).
/// </summary>
public record SourceDocument(string SourceType, string SourceId, string Title, string Text);
