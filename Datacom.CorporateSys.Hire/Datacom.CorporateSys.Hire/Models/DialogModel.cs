using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Datacom.CorporateSys.Hire.Models
{
    public class DialogModel
    {
        [Required]
        public int Value { get; set; }
    }
}