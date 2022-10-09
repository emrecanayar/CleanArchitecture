using Core.Persistence.ComplexTypes;

namespace rentACar.Application.Features.Documents.Dtos
{
    public class FileUploadResultDto
    {
        public int Id { get; set; }
        public string NameKey { get; set; }
        public string Path { get; set; }
        public FileType FileType { get; set; }
    }
}
