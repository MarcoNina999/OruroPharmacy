namespace Pharmacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sp_Bill : DbMigration
    {
        public override void Up()
        {
            Sql(ResourceSQL.BillReport);
        }
        
        public override void Down()
        {
        }
    }
}
