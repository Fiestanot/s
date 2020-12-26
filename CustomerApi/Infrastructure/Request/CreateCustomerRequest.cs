using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApi.Infrastructure.Request
{
    public class CreateCustomerRequest
    {
        [BsonId]
        public string Id { get; set; }
        [Required(ErrorMessage ="İsim boş olamaz")]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage ="Geçersiz mail")]
        [Required(ErrorMessage ="Mail boş olamaz")]
        public string Email { get; set; }
    }
}
