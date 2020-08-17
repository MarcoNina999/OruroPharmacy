namespace Pharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Sp : DbMigration
    {
        public override void Up()
        {
            Sql(ResourceSQL.CreateBill);
            Sql(ResourceSQL.CreateSaleDetails);
            Sql(ResourceSQL.ListRole);
            Sql(ResourceSQL.ListUserRole);
            Sql(ResourceSQL.ListUsers);
            Sql(ResourceSQL.RandomImage);
        }
        
        public override void Down()
        {
        }
    }
}
