using Core.Persistence.Paging;

namespace rentACar.Application.Features.OperationClaims.Models
{
    public class OperationClaimListModel : BasePageableModel
    {
        public IList<OperationClaimListDto> Items { get; set; }
    }
}
