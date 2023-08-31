using System.Reflection.Metadata.Ecma335;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodleExtensionAPI.Models
{
   
        public class SignatureConditionWrapper
        {
        public SignatureConditionWrapper() { }
            public SignatureCondition signatureCondition { get; set; }
        }

        public class SignatureCondition
        {
        public SignatureCondition() { }
            public string? type { get; set; }
            public List<Condition> conditions { get; set; }
        }

    public class Condition
    {
        public Condition() { }
        public Condition(string Type, bool Required)
        {
            this.type = Type;
            this.required = Required;
     
        }  public Condition(string Type, bool Required, int number)
        {
            this.type = Type;
            this.required = Required;
            if (Type == "multipleAssigment")
            this.numberOfAssigments = number;
            else if(Type == "allTestsWritten")
                this.numberOfTests = number;
            else if(Type == "smallTestsWritten")
                this.numberOfSmallTests = number;
        } 
        public Condition(string Type, int MinimumPercentage)
        {
            this.type = Type;
            this.minimumPercentage = MinimumPercentage;
        }
        public string? type { get; set; }
        public bool required { get; set; }
        public int? minimumPercentage { get; set; }
        public int? numberOfAssigments {get; set;}
        public int? numberOfTests {get; set;}
        public int? numberOfSmallTests {get; set;}
    }
    
}
