namespace MoodleExtensionAPI.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Condition
    {
        public string Type { get; set; }
        public int? NumberOfAssigments { get; set; }
        public int? RequiredNumberOfAssigments { get; set; }
        public double? RequiredIndividualAssigmentPercentage { get; set; }
        public double? RequiredAvgAssigmentPercentage { get; set; }
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

        public Condition()
        {
            
        }
        public Condition(string Type, int NumberOfTest, int RequiredNumberOfTest, double RequiredIndividualTestPercentage,double RequiredAvgTestPercentage, double Weight)
        {
            this.Type = Type;
            this.Weight = Weight;
            switch (Type)
            {
                case "multipleAssigment":
                    this.NumberOfAssigments = NumberOfTest;
                    this.RequiredNumberOfAssigments = RequiredNumberOfTest;
                    this.RequiredIndividualAssigmentPercentage = RequiredIndividualTestPercentage;
                    this.RequiredAvgAssigmentPercentage = RequiredAvgTestPercentage;
                    break;
                case "bigTests":
                    this.NumberOfBigTests = NumberOfTest;
                    this.RequiredNumberOfBigTests = RequiredNumberOfTest;
                    this.RequiredIndividualBigTestPercentage = RequiredIndividualTestPercentage;
                    this.RequiredAvgBigTestPercentage = RequiredAvgTestPercentage;
                    break;
                case "smallTests":
                    this.NumberOfSmallTests = NumberOfTest;
                    this.RequiredNumberOfSmallTests = RequiredNumberOfTest;
                    this.RequiredIndividualSmallTestPercentage = RequiredIndividualTestPercentage;
                    this.RequiredAvgSmallTestPercentage = RequiredAvgTestPercentage;
                    break;
            }
        }

        public Condition (string Type, double GradeA, double GradeB, double GradeC, double GradeD)
        {
            this.Type = Type;
            switch (Type)
            {
                case "grading":
                    this.GradeAPercentage = GradeA;
                    this.GradeBPercentage = GradeB;
                    this.GradeCPercentage = GradeC;
                    this.GradeDPercentage = GradeD;
                    break;
                case "offeredGrade":
                    this.OfferedGradeAPercentage = GradeA;
                    this.OfferedGradeBPercentage = GradeB;
                    this.OfferedGradeCPercentage = GradeC;
                    this.OfferedGradeDPercentage = GradeD;
                    break;
            }
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
