using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pharmacy.Models
{
    public class SaleDetails
    {
        [Key]
        public int Id { get; set; }
        //[Display(Name = "NumFactura")]
        public int BillId { get; set; }
        [ForeignKey("BillId")]
        public virtual Bill Bill { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        [Display(Name = "Cantidad")]
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        [Display(Name = "Descuento")]
        public float Discount { get; set; }        
        public float SubTotal { get; set; }
        public float IVA { get; set; }
        public float NetTotal { get; set; }
    }
}