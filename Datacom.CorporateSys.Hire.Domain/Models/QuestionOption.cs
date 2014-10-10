using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Datacom.CorporateSys.Hire.Domain.Models
{
    public class QuestionOption:BaseObject
    {

        //[ForeignKey("FKQuestionId")]
        public Guid? QuestionId { get; set; }
        public virtual Question Question { get; set; }

        [Display(Name = "Is Selected?")]
        public bool? IsSelected { get; set; }

        [Display(Name = "Default Answer")]
        public string DefaultAnswer { get; set; }

        public QuestionOption()
        {
            this.Answers = new HashSet<Answer>();
        }

        //one to many QuestionOption>Answer
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
