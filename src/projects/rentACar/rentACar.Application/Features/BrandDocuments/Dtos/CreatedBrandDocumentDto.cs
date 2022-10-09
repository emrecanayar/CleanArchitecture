namespace rentACar.Application.Features.BrandDocuments.Dtos
{
    public class CreatedBrandDocumentDto
    {
        public List<string> DocumentTokens { get; set; }
        public int BrandId { get; set; }
        public string WebRootPath { get; set; }
    }
}
