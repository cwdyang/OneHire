using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Datacom.CorporateSys.Hire.Domain.Models
{
    /// <summary>
    /// http://www.humanworkflow.net/post/2014/01/30/Managing-Hierarchal-ParentChild-Data-With-DatabaseCode-First-Entity-Framework-And-Recursion.aspx
    /// http://www.asp.net/mvc/tutorials/getting-started-with-ef-using-mvc/implementing-inheritance-with-the-entity-framework-in-an-asp-net-mvc-application
    /// </summary>
    public class Category: BaseObject
    {
        [StringLength(1000)]
        [Display(Name = "Details")]
        public string Details { get; set; }

        public CategoryType CategoryType { get; set; }

        //Navigation property 
        //leaving this here commented out this causes an extra foreign key in TPT inheritance!!!!
        //public virtual BaseObject BaseObject { get; set; } 

        public Category ()
        {
            this.Questions = new HashSet<Question>();
            this.Exams = new HashSet<Exam>();
        }

        //one to many Category>Question
        public virtual ICollection<Question> Questions { get; set; }


        //many to many Exam<>Questions , questions used in exams
        //lazy loading 
        public virtual ICollection<Exam> Exams { get; set; }
    }

    public enum CategoryType
    {
        Division = 0,
        Framework = 1,
        Technology = 2,
        SubjectArea = 3
    }
}
