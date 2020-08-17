using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pharmacy.Models
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public int SaleId { get; set; }
        [ForeignKey("SaleId")]
        public virtual SaleType SaleType { get; set; }
        public string SellerId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Creacion")]
        public System.DateTime Create_at { get; set; }
        public float SubTotal { get; set; }
        public float Discount { get; set; }
        public float Bonus { get; set; }
        public float TotalIVA { get; set; }
        public float Total { get; set; }
    }
}