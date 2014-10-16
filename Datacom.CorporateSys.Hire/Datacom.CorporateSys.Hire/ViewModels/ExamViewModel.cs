using System;
using System.Collections.Generic;
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

        public ExamViewModel(Candidate candidate, Exam exam, List<Category> categories )
        {
            Candidate = candidate;
            Exam = exam;
            Categories = categories;
        }
    }


}