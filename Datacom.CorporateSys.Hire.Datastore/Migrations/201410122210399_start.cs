namespace Datacom.CorporateSys.Hire.Datastore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class start : DbMigration
    {
        public override void Up()
        {
            return;
            CreateTable(
                "Hire.BaseObject",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ParentId = c.Guid(),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdatedBy = c.String(maxLength: 50),
                        IsEnabled = c.Boolean(nullable: false),
                        DisplaySequence = c.Int(),
                        Caption = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Hire.BaseObject", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "Hire.Exam",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CandidateId = c.Guid(nullable: false),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        StartedOn = c.DateTimeOffset(precision: 7),
                        CompletedOn = c.DateTimeOffset(precision: 7),
                        Examiner = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Hire.Candidate", t => t.CandidateId, cascadeDelete: true)
                .Index(t => t.CandidateId);
            
            CreateTable(
                "Hire.Candidate",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(maxLength: 255),
                        LastName = c.String(maxLength: 255),
                        MobileNumber = c.String(maxLength: 255),
                        Email = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExamCategory",
                c => new
                    {
                        ExamId = c.Guid(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ExamId, t.CategoryId })
                .ForeignKey("Hire.Exam", t => t.ExamId, cascadeDelete: true)
                .ForeignKey("Hire.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.ExamId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.ExamQuestion",
                c => new
                    {
                        ExamId = c.Guid(nullable: false),
                        QuestionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ExamId, t.QuestionId })
                .ForeignKey("Hire.Exam", t => t.ExamId, cascadeDelete: true)
                .ForeignKey("Hire.Question", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.ExamId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "Hire.Answer",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        QuestionOptionId = c.Guid(nullable: false),
                        ExamId = c.Guid(nullable: false),
                        AnswerText = c.String(maxLength: 1000),
                        ScorePoint = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Hire.BaseObject", t => t.Id)
                .ForeignKey("Hire.QuestionOption", t => t.QuestionOptionId)
                .ForeignKey("Hire.Exam", t => t.ExamId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.QuestionOptionId)
                .Index(t => t.ExamId);
            
            CreateTable(
                "Hire.Category",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Details = c.String(maxLength: 1000),
                        CategoryType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Hire.BaseObject", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "Hire.QuestionOption",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        QuestionId = c.Guid(nullable: false),
                        IsSelected = c.Boolean(),
                        DefaultAnswer = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Hire.BaseObject", t => t.Id)
                .ForeignKey("Hire.Question", t => t.QuestionId)
                .Index(t => t.Id)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "Hire.Question",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DataType = c.String(maxLength: 255),
                        ImageUri = c.String(maxLength: 511),
                        Level = c.Int(nullable: false),
                        ScorePoint = c.Int(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                        QuestionOptionId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Hire.BaseObject", t => t.Id)
                .ForeignKey("Hire.Category", t => t.CategoryId)
                .ForeignKey("Hire.QuestionOption", t => t.QuestionOptionId)
                .Index(t => t.Id)
                .Index(t => t.CategoryId)
                .Index(t => t.QuestionOptionId);
            
        }
        
        public override void Down()
        {
            return;
            DropForeignKey("Hire.Question", "QuestionOptionId", "Hire.QuestionOption");
            DropForeignKey("Hire.Question", "CategoryId", "Hire.Category");
            DropForeignKey("Hire.Question", "Id", "Hire.BaseObject");
            DropForeignKey("Hire.QuestionOption", "QuestionId", "Hire.Question");
            DropForeignKey("Hire.QuestionOption", "Id", "Hire.BaseObject");
            DropForeignKey("Hire.Category", "Id", "Hire.BaseObject");
            DropForeignKey("Hire.Answer", "ExamId", "Hire.Exam");
            DropForeignKey("Hire.Answer", "QuestionOptionId", "Hire.QuestionOption");
            DropForeignKey("Hire.Answer", "Id", "Hire.BaseObject");
            DropForeignKey("dbo.ExamQuestion", "QuestionId", "Hire.Question");
            DropForeignKey("dbo.ExamQuestion", "ExamId", "Hire.Exam");
            DropForeignKey("dbo.ExamCategory", "CategoryId", "Hire.Category");
            DropForeignKey("dbo.ExamCategory", "ExamId", "Hire.Exam");
            DropForeignKey("Hire.Exam", "CandidateId", "Hire.Candidate");
            DropForeignKey("Hire.BaseObject", "ParentId", "Hire.BaseObject");
            DropIndex("Hire.Question", new[] { "QuestionOptionId" });
            DropIndex("Hire.Question", new[] { "CategoryId" });
            DropIndex("Hire.Question", new[] { "Id" });
            DropIndex("Hire.QuestionOption", new[] { "QuestionId" });
            DropIndex("Hire.QuestionOption", new[] { "Id" });
            DropIndex("Hire.Category", new[] { "Id" });
            DropIndex("Hire.Answer", new[] { "ExamId" });
            DropIndex("Hire.Answer", new[] { "QuestionOptionId" });
            DropIndex("Hire.Answer", new[] { "Id" });
            DropIndex("dbo.ExamQuestion", new[] { "QuestionId" });
            DropIndex("dbo.ExamQuestion", new[] { "ExamId" });
            DropIndex("dbo.ExamCategory", new[] { "CategoryId" });
            DropIndex("dbo.ExamCategory", new[] { "ExamId" });
            DropIndex("Hire.Exam", new[] { "CandidateId" });
            DropIndex("Hire.BaseObject", new[] { "ParentId" });
            DropTable("Hire.Question");
            DropTable("Hire.QuestionOption");
            DropTable("Hire.Category");
            DropTable("Hire.Answer");
            DropTable("dbo.ExamQuestion");
            DropTable("dbo.ExamCategory");
            DropTable("Hire.Candidate");
            DropTable("Hire.Exam");
            DropTable("Hire.BaseObject");
        }
    }
}
