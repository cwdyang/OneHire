using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datacom.CorporateSys.HireAPI;

namespace Datacom.CorporateSys.Hire.ViewModels
{
    public class ExamViewModel
    {
        public Candidate Candidate { get; set; }
        public Exam Exam { get; set; }

        public ExamViewModel(Candidate candidate, Exam exam)
        {
            Candidate = candidate;
            Exam = exam;
        }
    }
}