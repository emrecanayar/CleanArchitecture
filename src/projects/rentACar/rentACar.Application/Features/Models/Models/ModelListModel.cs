using Core.Persistence.Paging;
using rentACar.Application.Features.Brands.Dtos;
using rentACar.Application.Features.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rentACar.Application.Features.Models.Models
{
    public class ModelListModel : BasePageableModel
    {
        public IList<ModelListDto> Items { get; set; }
    }
}
