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
            public string type { get; set; }
            public List<Condition> conditions { get; set; }
        }

        public class Condition
        {
        public Condition() { }
        public Condition(string Type, bool Required)
        {
            this.type = Type;
            this.required = Required;
        }
        public Condition(string Type, int MinimumPercentage)
        {
            this.type = Type;
            this.minimumPercentage = MinimumPercentage;
        }
            public string type { get; set; }
            public bool required { get; set; }
            public int? minimumPercentage { get; set; }
        }
    
}
