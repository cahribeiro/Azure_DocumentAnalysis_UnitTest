public class DocAnalyzeTest
{

    [Fact]
    public async Task Map_ShouldMapAnalyzeResult()
    {
        // Arrange
        var mapper = Example();
        var doc = Document();

        // Act
        var result = await mapper.Map(doc);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Document);
        Assert.NotNull(result.Keywords);
        Assert.Equal("test", result.Keywords.ToArray()[0].Item.Value);
    }

    private AnalyzeResult Document()
    {
        var fields = new Dictionary<string, DocumentField>
        {
            { "test", DocumentField(DocumentFieldType.String, "test", "test", "", 0.90f) }
        };

        var analyzedDocument = DocumentAnalysisModelFactory.AnalyzedDocument(
            documentType: "Example",
            confidence: 0.95f,
            fields: fields,
            boundingRegions: new List<BoundingRegion>(),
            spans: new List<DocumentSpan>()
        );

        return DocumentAnalysisModelFactory.AnalyzeResult(
            modelId: "test",
            content: "Sample content",
            pages: new List<DocumentPage>(),
            tables: new List<DocumentTable>(),
            keyValuePairs: new List<DocumentKeyValuePair>(),
            styles: new List<DocumentStyle>(),
            documents: new List<AnalyzedDocument> { analyzedDocument }
        );
    }

    private DocumentField DocumentField(DocumentFieldType fieldType, string content, object value, string currencyCode, float confidence)
    {
        return DocumentAnalysisModelFactory.DocumentField(
            fieldType,
            fieldType == DocumentAnalysisModelFactory.DocumentFieldValueWithStringFieldType((string)value),
            content: content,
            boundingRegions: new List<BoundingRegion>(),
            spans: new List<DocumentSpan>(),
            confidence: confidence
        );
    }
}
