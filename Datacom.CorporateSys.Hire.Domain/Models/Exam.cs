using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Datacom.CorporateSys.Hire.Domain.Models
{
    public class Exam
    {

        public Guid Id { get; set; }

        //lazy loading 
        //one to many Candidate>Exams
        //Foreign key
        public Guid CandidateId { get; set; }
        public virtual Candidate Candidate { get; set; }
        

        [Display(Name = "Created On")]
        public DateTimeOffset CreatedOn { get; set; }

        [Display(Name = "Started On")]
        public DateTimeOffset? StartedOn { get; set; }

        [Display(Name = "Completed On")]
        public DateTimeOffset? CompletedOn { get; set; }

        [StringLength(255)]
        [Display(Name = "Examiner")]
        public string Examiner { get; set; }

        public Exam()
        {
            this.Questions = new HashSet<Question>();
            this.Categories = new HashSet<Category>();
            this.Answers = new HashSet<Answer>();
        }

        //http://www.entityframeworktutorial.net/code-first/configure-many-to-many-relationship-in-code-first.aspx
        //many to many Exam<>Questions
        //lazy loading 
        public virtual ICollection<Question> Questions { get; set; }
        //many to many Exam<>Category
        public virtual ICollection<Category> Categories { get; set; }

        //one to many Exam>Answer
        public virtual ICollection<Answer> Answers { get; set; }

    }
}
