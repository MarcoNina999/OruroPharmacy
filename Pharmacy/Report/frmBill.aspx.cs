using Pharmacy.Controllers;
using Pharmacy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pharmacy.Report
{
    public partial class frmBill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ApplicationDbContext Db = new ApplicationDbContext();
            int BillId;
            BillId = SaleController.BillId;
            BillReport billReport = new BillReport();
            billReport.SetParameterValue("@billId", BillId);
            CrystalReportViewer1.ReportSource = billReport;
        }
    }
}