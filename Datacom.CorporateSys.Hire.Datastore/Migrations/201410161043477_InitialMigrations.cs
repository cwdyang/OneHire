namespace Datacom.CorporateSys.Hire.Datastore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigrations : DbMigration
    {
        public override void Up()
        {
            return;
            MoveTable(name: "dbo.ExamCategory", newSchema: "Hire");
            MoveTable(name: "dbo.ExamQuestion", newSchema: "Hire");
        }
        
        public override void Down()
        {
            return;
            MoveTable(name: "Hire.ExamQuestion", newSchema: "dbo");
            MoveTable(name: "Hire.ExamCategory", newSchema: "dbo");
        }
    }
}
