using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Infrastructure.Request
{
    public class ChangeStatusRequest
    {
        [Required(ErrorMessage ="Sipariş numarası boş olamaz")]
        public string Id { get; set; }
        [Required(ErrorMessage ="Sipariş durumu boş olamaz")]
        public string Status { get; set; }
    }
}
