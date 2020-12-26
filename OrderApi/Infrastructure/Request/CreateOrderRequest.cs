using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Infrastructure.Request
{
    public class CreateOrderRequest
    {
        [BsonId]
        public string Id { get; set; }
        [Required(ErrorMessage ="Görsel boş olamaz")]
        [Url]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage ="Fiyat boş olamaz")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Adet boş olamaz")]
        public int Quantity { get; set; }

        [Required(ErrorMessage ="Müşteri boş olamaz")]
        public string CustomerId { get; set; }
    }
}
