using Core.Persistence.ComplexTypes;

namespace rentACar.Application.Features.Documents.Dtos
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public FileType FileType { get; set; }


    }
}
