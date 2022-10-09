using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rentACar.Application.Features.Documents.Dtos
{
    public class TransferDocumentDto
    {
        public string Token { get; set; }
        public string NewFolderPath { get; set; }
    }
}
