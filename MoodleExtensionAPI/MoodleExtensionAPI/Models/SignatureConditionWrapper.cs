namespace MoodleExtensionAPI.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Condition
    {
        public string Type { get; set; }
        public int NumberOfAssigments { get; set; }
        public int RequiredNumberOfAssigments { get; set; }
        public double RequiredIndividualAssigmentPercentage { get; set; }
        public double RequiredAvgAssigmentPercentage { get; set; }
        public double Weight { get; set; }
        public int? NumberOfBigTests { get; set; }
        public int? RequiredNumberOfBigTests { get; set; }
        public double? RequiredIndividualBigTestPercentage { get; set; }
        public double? RequiredAvgBigTestPercentage { get; set; }
        public int? NumberOfSmallTests { get; set; }
        public int? RequiredNumberOfSmallTests { get; set; }
        public double? RequiredIndividualSmallTestPercentage { get; set; }
        public double? RequiredAvgSmallTestPercentage { get; set; }
        public double? GradeAPercentage { get; set; }
        public double? GradeBPercentage { get; set; }
        public double? GradeCPercentage { get; set; }
        public double? GradeDPercentage { get; set; }
        public double? OfferedGradeAPercentage { get; set; }
        public double? OfferedGradeBPercentage { get; set; }
        public double? OfferedGradeCPercentage { get; set; }
        public double? OfferedGradeDPercentage { get; set; }
        public Condition(string Type)
        {
            this.Type = Type;
        }
    }

    public class SignatureConditionWrapper
    {
        public SignatureCondition SignatureCondition { get; set; }
    }

    public class SignatureCondition
    {
        public string Type { get; set; }
        public List<Condition> Conditions { get; set; }
    }




}
