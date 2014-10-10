using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datacom.CorporateSys.Hire.Domain.Models
{
    public class Candidate
    {
        public Guid Id { get; set; }

        [StringLength(255)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(255)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(255)]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [StringLength(255)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public Candidate()
        {
            this.Exams = new HashSet<Exam>();
        }

        //lazy loading 
        //one to many Candidate>Exams
        public virtual ICollection<Exam> Exams { get; set; }
    }
}
