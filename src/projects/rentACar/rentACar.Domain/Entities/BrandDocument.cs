using Core.Persistence.Repositories;

namespace rentACar.Domain.Entities
{
    public class BrandDocument : Entity
    {
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int DocumentId { get; set; }
        public Document Document { get; set; }
    }
}
