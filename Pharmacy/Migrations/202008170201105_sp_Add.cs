namespace Pharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sp_Add : DbMigration
    {
        public override void Up()
        {
            Sql(ResourceSQL.AddProdQuantity);
        }
        
        public override void Down()
        {
        }
    }
}
