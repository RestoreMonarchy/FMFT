using FMFT.Web.Shared.Enums;

namespace FMFT.Web.Server.Models.Orders.DTOs
{
    public class CreateOrderDTO
    {
		public int UserId { get; set; }
		public decimal Amount { get; set; }
		public string Currency { get; set; }
		public PaymentMethod PaymentMethod { get; set; }
		public DateTime ExpireDate { get; set;}
		public List<CreateOrderItemDTO> Items { get; set; }
		public List<int> SeatIds { get; set; }
	}
}

/*

expected JSON example: 

{  
    "UserId":1,  
    "Amount":123.12, 
	"Currency":"PLN",
	"PaymentMethod":"0",
	"ExpireDate":"2023-01-17T20:01:12",
    "Items": [{  
		"ShowProductId":1,
		"Price":100.12,  
		"Quantity":1
    },
	{  
		"ShowProductId":2,
		"Price":23.00,  
		"Quantity":2
    }],
	"SeatIds": [10,11,12]
 }'

 */