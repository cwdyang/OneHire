using System.Collections.Generic;
using Datacom.CorporateSys.Hire.Datastore.Contexts;
using Datacom.CorporateSys.Hire.Domain.Models;

namespace Datacom.CorporateSys.Hire.Datastore.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OneHireMainContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        /// <summary>
        /// http://blog.oneunicorn.com/2013/05/28/database-initializer-and-migrations-seed-methods/
        /// 
        /// in Package Manager Console run
        /// 
        /// Update-Database
        /// 
        /// http://www.itorian.com/2012/10/entity-frameworks-database-seed-method.html
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(OneHireMainContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.DeleteAll<Answer>();
            context.DeleteAll<QuestionOption>();
            context.DeleteAll<Question>();
            context.DeleteAll<Exam>();
            context.DeleteAll<Category>();
            context.DeleteAll<Candidate>();
            context.DeleteAll<BaseObject>();


            var catMicrosoft = new Category
            {
                Caption = "Microsoft Solutions",
                Details = "Microsoft Solutions",
                Id = Guid.NewGuid(),
                DisplaySequence = 0,
                ParentBaseObject = null,
                CategoryType = CategoryType.Division,
                IsEnabled = true,
                UpdatedBy = "davidy",
                UpdatedOn = DateTimeOffset.Now,
                CreatedOn = DateTimeOffset.Now
            };


            var catDotNet = new Category
            {
                Caption = ".Net",
                Details = ".Net",
                Id = Guid.NewGuid(),
                DisplaySequence = 0,
                ParentBaseObject = catMicrosoft,
                CategoryType = CategoryType.Framework,
                IsEnabled = true,
                UpdatedBy = "davidy",
                UpdatedOn = DateTimeOffset.Now,
                CreatedOn = DateTimeOffset.Now
            };

            var catCSharp = new Category
            {
                Caption = "C# General",
                Details = "C# General",
                Id = Guid.NewGuid(),
                DisplaySequence = 0,
                ParentBaseObject = catDotNet,
                CategoryType = CategoryType.Technology,
                IsEnabled = true,
                UpdatedBy = "davidy",
                UpdatedOn = DateTimeOffset.Now,
                CreatedOn = DateTimeOffset.Now
            };

            var catComputerGeneral = new Category
            {
                Caption = "Computer General",
                Details = "Computer General",
                Id = Guid.NewGuid(),
                DisplaySequence = 0,
                ParentBaseObject = null,
                CategoryType = CategoryType.Framework,
                IsEnabled = true,
                UpdatedBy = "davidy",
                UpdatedOn = DateTimeOffset.Now,
                CreatedOn = DateTimeOffset.Now
            };

            var catProgAdvanced = new Category
            {
                Caption = "Programming Advanced",
                Details = "Programming Advanced",
                Id = Guid.NewGuid(),
                DisplaySequence = 0,
                ParentBaseObject = catComputerGeneral,
                CategoryType = CategoryType.Framework,
                IsEnabled = true,
                UpdatedBy = "davidy",
                UpdatedOn = DateTimeOffset.Now,
                CreatedOn = DateTimeOffset.Now
            };

            context.Categories.AddOrUpdate(catMicrosoft);
            context.Categories.AddOrUpdate(catDotNet);
            context.Categories.AddOrUpdate(catCSharp);
            context.Categories.AddOrUpdate(catComputerGeneral);
            context.Categories.AddOrUpdate(catProgAdvanced);


            

            var candidate = new Candidate
            {
                Id = Guid.NewGuid(),
                Email = "davidy@datacom.co.nz",
                FirstName = "david",
                LastName = "yang",
                MobileNumber = "021003533"
            };

            context.Candidates.AddOrUpdate(candidate);

            var q1 = new Question
            {
                Id = Guid.NewGuid(),
                DataType = "RadioButtonGroup",
                Category = catProgAdvanced,
                Caption = "What is a B-Tree?",
                DisplaySequence = 1,
                CreatedOn = DateTimeOffset.Now,
                IsEnabled = true,
                UpdatedBy = "davidy"
            };

            var q1a = new Question
            {
                Id = Guid.NewGuid(),
                DataType = "RadioButtonGroup",
                Category = catProgAdvanced,
                Caption = "What is the primary use of B-trees?",
                DisplaySequence = 1,
                CreatedOn = DateTimeOffset.Now,
                IsEnabled = true,
                UpdatedBy = "davidy",
                ParentBaseObject = q1
            };

            context.Questions.AddOrUpdate(q1);
            context.Questions.AddOrUpdate(q1a);


            var q1_1 = new QuestionOption()
            {
                Id = Guid.NewGuid(),
                Caption = "B-Tree stands for Binary Tree and is a data structure in which each node has at most two children, which are referred to as left child and the right child.",
                DisplaySequence = 1,
                CreatedOn = DateTimeOffset.Now,
                IsEnabled = true,
                UpdatedBy = "davidy",
                Question = q1,
                IsSelected = true
            };

            var q1_2 = new QuestionOption()
            {
                Id = Guid.NewGuid(),
                Caption = "B-Tree stands for Babbage Tree, representing the evolution of computers as envisioned by Charles Babbage.",
                DisplaySequence = 1,
                CreatedOn = DateTimeOffset.Now,
                IsEnabled = true,
                UpdatedBy = "davidy",
                Question = q1,
                IsSelected = false
            };

            var q1_3 = new QuestionOption()
            {
                Id = Guid.NewGuid(),
                Caption = "B-Tree stands for Boot sequence Tree and is used by Operating System’s boot sequencer to select a specific branch from the Tree. E.g. Boot using Safe mode in Windows.",
                DisplaySequence = 1,
                CreatedOn = DateTimeOffset.Now,
                IsEnabled = true,
                UpdatedBy = "davidy",
                Question = q1,
                IsSelected = false
            };

            var q1_4 = new QuestionOption()
            {
                Id = Guid.NewGuid(),
                Caption = "B-Tree is a hypothetical term used by the Computer Hackers on how a Worm/Virus spreads across the systems.",
                DisplaySequence = 1,
                CreatedOn = DateTimeOffset.Now,
                IsEnabled = true,
                UpdatedBy = "davidy",
                Question = q1,
                IsSelected = false
            };

            context.QuestionOptions.AddOrUpdate(q1_1);
            context.QuestionOptions.AddOrUpdate(q1_2);
            context.QuestionOptions.AddOrUpdate(q1_3);
            context.QuestionOptions.AddOrUpdate(q1_4);

            var q1a_1 = new QuestionOption()
            {
                Id = Guid.NewGuid(),
                Caption = "B-Tree is a hypothetical term used by the Computer Hackers on how a Worm/Virus spreads across the systems.",
                DisplaySequence = 1,
                CreatedOn = DateTimeOffset.Now,
                IsEnabled = true,
                UpdatedBy = "davidy",
                Question = q1a,
                IsSelected = false
            };

            var q1a_2 = new QuestionOption()
            {
                Id = Guid.NewGuid(),
                Caption = "B-Trees are used to implement binary search trees and binary heaps, and are used for efficient Searching and Sorting.",
                DisplaySequence = 1,
                CreatedOn = DateTimeOffset.Now,
                IsEnabled = true,
                UpdatedBy = "davidy",
                Question = q1a,
                IsSelected = true
            };

            var q1a_3 = new QuestionOption()
            {
                Id = Guid.NewGuid(),
                Caption = "the primary use of a B-Tree is within the compilers logic to parse class libraries, identify dependencies and avoid conflicts.",
                DisplaySequence = 1,
                CreatedOn = DateTimeOffset.Now,
                IsEnabled = true,
                UpdatedBy = "davidy",
                Question = q1a,
                IsSelected = false
            };

            var q1a_4 = new QuestionOption()
            {
                Id = Guid.NewGuid(),
                Caption = "B-Trees are used to implement Hardware level data structures that help efficiently reading and writing binary files or communicate machine language.",
                DisplaySequence = 1,
                CreatedOn = DateTimeOffset.Now,
                IsEnabled = true,
                UpdatedBy = "davidy",
                Question = q1a,
                IsSelected = false
            };


            context.QuestionOptions.AddOrUpdate(q1a_1);
            context.QuestionOptions.AddOrUpdate(q1a_2);
            context.QuestionOptions.AddOrUpdate(q1a_3);
            context.QuestionOptions.AddOrUpdate(q1a_4);

            var questions = new HashSet<Question> {q1};
            var categories = new HashSet<Category>{catProgAdvanced};

            var exam = new Exam
            {
                Id = Guid.NewGuid(),
                Categories = categories,
                Candidate = candidate,
                CreatedOn = DateTimeOffset.Now,
                CompletedOn = DateTimeOffset.Now,
                StartedOn = DateTimeOffset.Now,
                Examiner = "davidy",
                Questions = questions
            };

            var a1 = new Answer
            {
                Id = Guid.NewGuid(),
                QuestionOption = q1_2,
                CreatedOn = DateTimeOffset.Now,
                UpdatedBy = "davidy",
                Exam = exam,
                AnswerText = "Selected"
            };

            context.Exams.AddOrUpdate(exam);

            context.Answers.AddOrUpdate(a1);

            context.SaveChanges();

        }
    }
}
