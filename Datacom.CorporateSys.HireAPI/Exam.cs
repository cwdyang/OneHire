﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Text;

namespace Datacom.CorporateSys.HireAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "Datacom.CorporateSys.HireAPI.ContractType".
    [DataContract]
    [KnownType(typeof(Exam))]
    [KnownType(typeof(Option))]
    [KnownType(typeof(Question))]
    [KnownType(typeof(Category))]
    [KnownType(typeof(Candidate))]
    public class BaseObject
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Text { get; set; }
    }

    [DataContract]
    public class Candidate : BaseObject
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public virtual IList<Exam> Exams { get; set; }
    }

    [DataContract]
    public class Exam:BaseObject
    {

        [DataMember]
        public string Examiner { get; set; }

        [DataMember]
        public virtual IList<Question> Questions { get; set; }

        [DataMember]
        public virtual IList<Category> Categories { get; set; }

        [DataMember]
        public DateTimeOffset CreatedOn { get; set; }

        [DataMember]
        public DateTimeOffset? StartedOn { get; set; }

        [DataMember]
        public DateTimeOffset? CompletedOn { get; set; }
    }

    [DataContract]
    public class Option:BaseObject
    {
        [DataMember]
        public bool IsSelected { get; set; }
    }

    [DataContract]
    public class Answer : BaseObject
    {
        [DataMember]
        public Option Option { get; set; }
        
        [DataMember]
        public string AnswerText { get; set; }

        [DataMember]
        public int ScorePoint { get; set; }

        [DataMember]
        public int Level { get; set; }
    }

    [DataContract]
    public class Question:BaseObject
    {
        [DataMember]
        public string DataType { get; set; }

        [DataMember]
        public string ImageUri { get; set; }

        [DataMember]
        public int Level { get; set; }

        [DataMember]
        public int ScorePoint { get; set; }

        [DataMember]
        public virtual IList<Question> Questions { get; set; }

        [DataMember]
        public virtual IList<Option> Options { get; set; }
    }

    [DataContract]
    public class Category:BaseObject
    {
        
        [DataMember]
        public virtual IList<Question> Questions { get; set; }


    }

}