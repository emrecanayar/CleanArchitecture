using Core.Domain.Entities.Base;

namespace Core.Domain.Entities
{
    public class OperationClaim : Entity
    {
        public string Name { get; set; }
        public OperationClaim()
        {

        }

        public OperationClaim(int id, string name) : this()
        {
            Id = id;
            Name = name;
        }
    }
}
