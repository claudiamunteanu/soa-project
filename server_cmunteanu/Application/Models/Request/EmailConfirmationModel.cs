using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class EmailConfirmationModel
    {
        public string ReceiverEmail { get; set; } = null!;
        public string ReceiverName { get; set; } = null!;
        public string OrderNumber { get; set; } = null!;
        public string OrderDate { get; set; } = null!;
    }
}
