using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Response
{
    public class OrderResponseModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string PlacedById { get; set; } = null!;
        public List<OrderProductModel> Products { get; set; } = null!;
    }
}
