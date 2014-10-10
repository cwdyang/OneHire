using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Datacom.CorporateSys.Hire.Domain.Models
{
    public class Answer:BaseObject
    {

        //[ForeignKey("FKQuestionId")]
        public Guid? QuestionOptionId { get; set; }
        public virtual QuestionOption QuestionOption { get; set; }

        public Guid? ExamId { get; set; }
        public virtual Exam Exam { get; set; }

        [StringLength(1000)]
        [Display(Name = "Answer Text")]
        public string AnswerText { get; set; }

        [Display(Name = "Score / Point")]
        public int ScorePoint { get; set; }

        [Display(Name = "Level")]
        public int Level { get; set; }

    }
}
