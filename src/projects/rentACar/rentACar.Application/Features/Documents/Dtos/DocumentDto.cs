using Core.Persistence.ComplexTypes;

namespace rentACar.Application.Features.Documents.Dtos
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string DocumentName { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public FileType FileType { get; set; }


    }
}
