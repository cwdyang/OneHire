using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Datacom.CorporateSys.Hire.Domain.Models
{
    /// <summary>
    /// http://www.humanworkflow.net/post/2014/01/30/Managing-Hierarchal-ParentChild-Data-With-DatabaseCode-First-Entity-Framework-And-Recursion.aspx
    /// </summary>
    public class Question:BaseObject
    {
        [StringLength(255)]
        [Display(Name = "Data Type")]
        public string DataType { get; set; }

        [StringLength(511)]
        [Display(Name = "Image Uri")]
        public string ImageUri { get; set; }

        [DefaultValue(5)]
        [Display(Name = "Level")]
        public int Level { get; set; }

        [DefaultValue(1)]
        [Display(Name = "Score / Point")]
        public int ScorePoint { get; set; }

        //ForeignKey
        //Navigation property 
        //leaving this here commented out this causes an extra foreign key in TPT inheritance!!!!
        //public virtual BaseObject BaseObject { get; set; } 
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }


        public Question()
        {
            this.Exams = new HashSet<Exam>();
            this.QuestionOptions = new HashSet<QuestionOption>();
        }

        //http://www.entityframeworktutorial.net/code-first/configure-many-to-many-relationship-in-code-first.aspx
        //many to many Exam<>Questions , questions used in exams
        //lazy loading 
        public virtual ICollection<Exam> Exams { get; set; }

        //one to many Question>QuestionOption
        public virtual ICollection<QuestionOption> QuestionOptions { get; set; }

        //one to many optional QuestionOption>question
        public Guid? QuestionOptionId { get; set; }
        public virtual QuestionOption QuestionOption { get; set; }
    }
}
