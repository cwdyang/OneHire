using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Datacom.CorporateSys.HireAPI;

namespace Datacom.CorporateSys.Hire.ViewModels
{
    public partial class ExamViewModel
    {
        public Candidate Candidate { get; set; }
        public Exam Exam { get; set; }
        public List<Category> Categories { get; set; }

        
        [Required(ErrorMessage = "Examiner Email is Required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Email is not valid")]
        [Display(Name = "Examiner Email Address")]
        public string ExaminerEmail { get; set; }

        public ExamViewModel(Candidate candidate, Exam exam, List<Category> categories, string examinerEmail)
        {
            Candidate = candidate;
            Exam = exam;
            Categories = categories;
            ExaminerEmail = examinerEmail;
        }
    }


}