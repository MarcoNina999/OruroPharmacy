using Microsoft.AspNet.Identity;
using Pharmacy.Models;
using Pharmacy.Report;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Pharmacy.Controllers
{
    public class SaleController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        private static int billId;
        public static int BillId
        {
            get { return billId; }
            set { billId = value; }
        }
        [HttpGet]
        public ActionResult ObtainingClient()
        {
            List<ApplicationUser> users = db.Database.SqlQuery<ApplicationUser>(ResourceSQL.ListUsersName).ToList();
            return View(users);
        }

        [HttpPost]//para buscar clientes
        public ActionResult ObtainingClient(string txtFirstName, string txtLastName, string txtCi, string txtId)
        {
            int txtci = Convert.ToInt32(txtCi);
            if (txtFirstName == "")
            {
                txtFirstName = "-1";
            }
            if (txtLastName == "")
            {
                txtLastName = "-1";
            }
            if (txtCi == "")
            {
                txtCi = "-1";
            }
            var objUsers = db.Users.ToList();
            objUsers[0].First_Name = txtFirstName;
            objUsers[0].Last_Name = txtLastName;
            objUsers[0].Id = txtId;
            objUsers[0].Ci = txtci;
            return View(objUsers);
        }

        [HttpPost]
        public ActionResult Select(int id)
        {            
            var objProduct = (from s in db.Products where s.Id == id select s);

            return Json(objProduct, JsonRequestBehavior.AllowGet);

        }
        
        public void loadingProduct()
        {
            var product = from s in db.Products select s;
            SelectList list = new SelectList(product, "Id", "Name");
            ViewBag.ListProduct = list;
        }
        public void loadingSaleType()
        {
            var sale = from s in db.SaleTypes select s;
            SelectList list = new SelectList(sale, "Id", "Name");
            ViewBag.ListSaleType = list;
        }
        
        public ActionResult NewSale()
        {
            loadingSaleType();
            loadingProduct();
            return View();
        }


        [HttpPost]
        public ActionResult GuardarVenta(string Fecha, string modoPago, string IdCliente, string Total, List<SaleDetails> ListadoDetalle)
        {
            Random random = new Random();
            string mensaje = "";            
            string SellerId = User.Identity.GetUserId();
            int SaleId = 1;
            double SubTotal = Convert.ToDouble(Total);
            double TotalIVA = 0;
            double Bonus = 0;
            double Discount = 0;
            double total = SubTotal - Discount - Bonus - TotalIVA;

            string codigoCliente = IdCliente.Trim();

            if (Fecha == "" /*|| modoPago == "" */|| IdCliente == "" || Total == "")
            {
                if (Fecha == "") mensaje = "ERROR EN EL CAMPO FECHA";
                if (modoPago == "") mensaje = "SELECCIONE UN MODO DE PAGO";
                if (IdCliente == "") mensaje = "ERROR CON EL CODIGO DEL CLIENTE";
                if (Total == "") mensaje = "ERROR EN EL CAMPO TOTAL";
            }
            else
            {
                int BillId = random.Next(999999);
                //codigoPago = Convert.ToInt32(modoPago);
                //codigoCliente = Convert.ToInt64(IdCliente);

                //SALE REGISTER
                db.Database.ExecuteSqlCommand(ResourceSQL.CreateBillName + "@id, @userId, @saleId, @sellerId, @Create_at, @subTotal, @discount, @bonus, @totaliva, @total", new SqlParameter("@id", BillId), new SqlParameter("@userId", codigoCliente), new SqlParameter("@saleId", SaleId), new SqlParameter("@sellerId", SellerId), new SqlParameter("@Create_at", Fecha), new SqlParameter("@subTotal", SubTotal), new SqlParameter("@discount", Discount), new SqlParameter("@bonus", Bonus), new SqlParameter("@totaliva", TotalIVA), new SqlParameter("@Total", total));
                //BILL REGISTER                                                       
                //var sale = db.Sales.Select(x => new { Id = x.Id, Total = x.Total }).ToList().Select(x => new Sale() { Id = x.Id, Total = x.Total }).ToList();
                if (BillId == null)
                {
                    mensaje = "ERROR AL REGISTRAR LA FACTURA";
                }
                else
                {

                    int SDId = 0;
                    //SALEDETAIL REGISTER
                    foreach (var data in ListadoDetalle)
                    {
                        SDId = random.Next(999999);
                        int ProductId = Convert.ToInt32(data.Id.ToString());
                        int Quantity = Convert.ToInt32(data.Quantity.ToString());
                        double discount = Convert.ToDouble(data.Discount.ToString());
                        double subTotal = Convert.ToDouble(data.SubTotal.ToString());
                        double UnitPrice = Convert.ToDouble(data.UnitPrice.ToString());
                        double IVA = 0;
                        double NetTotal = Convert.ToDouble(data.NetTotal.ToString());
                        NetTotal = subTotal - IVA;
                        db.Database.ExecuteSqlCommand(ResourceSQL.CreateSaleDetailsName + "@id, @billId, @productId, @quantity, @unitPrice, @discount, @subtotal, @iva, @netTotal", new SqlParameter("@id", SDId), new SqlParameter("@billId", BillId), new SqlParameter("@productId", ProductId), new SqlParameter("@quantity", Quantity), new SqlParameter("@unitPrice", UnitPrice), new SqlParameter("@discount", discount), new SqlParameter("@subtotal", subTotal), new SqlParameter("iva", IVA), new SqlParameter("@netTotal", NetTotal));
                    }

                    mensaje = "VENTA GUARDADA CON EXITO...";
                }
                billId = BillId;
            }

            return Json(mensaje);
        }

        //public ActionResult reporteActual()
        //{
        //    if (Session["idVenta"].ToString() != null)
        //    {
        //        string idVenta = Session["idVenta"].ToString();
        //        return Redirect("~/Reportes/frmReporteFactura.aspx?IdVenta=" + idVenta);
        //    }
        //    else
        //    {
        //        return View("GuardarVenta");
        //    }

        //}

        //public ActionResult ReporteVentas()
        //{
        //    List<Venta> lista = objVentaNeg.findAll();
        //    return View(lista);
        //}

        //public ActionResult DetallesVenta(long id)
        //{
        //    DetalleVenta objDetalleVenta = new DetalleVenta();
        //    objDetalleVenta.IdVenta = id;
        //    List<DetalleVenta> lista = objDetalleVentaNeg.detallesPorIdVenta(objDetalleVenta);
        //    return View(lista);
        //}

        //public ActionResult Bill()
        //{
        //    //List<SaleDetails> lista = objventaneg.findall();
        //    var sale = from s in db.SaleDetails select s;
        //    return View(sale);
        //}
    }
}
