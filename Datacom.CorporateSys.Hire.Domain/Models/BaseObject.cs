using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datacom.CorporateSys.Hire.Domain.Models
{
    /// <summary>
    /// Inheritance with EF Code First: Part 2 – Table per Type (TPT)     
    /// declare abstract
    /// </summary>

    public abstract class BaseObject
    {
        
        //[Key]
        public Guid Id { get; set; }

        //recursion
        //[ForeignKey("FKBaseObjectParent")]
        public  Guid? ParentId { get; set; }

        //Navigation property 
        //lazy loading 
        public virtual ICollection<BaseObject> BaseObjects { get; set; }
        public virtual BaseObject ParentBaseObject { get; set; } 

        [Display(Name = "Created On")]
        public DateTimeOffset CreatedOn { get; set; }

        [Display(Name = "Updated On")]
        public DateTimeOffset UpdatedOn { get; set; }

        [StringLength(50)]
        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Enabled?")]
        public bool IsEnabled { get; set; }

        [Display(Name = "Display Order")]
        public int? DisplaySequence { get; set; }

        [StringLength(1000)]
        [Display(Name = "Caption")]
        public string Caption { get; set; }

    }
}