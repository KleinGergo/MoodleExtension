namespace MoodleExtensionAPI.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class GradeCondition
    {
        public string Type { get; set; }
        public int? NumberOfAssigments { get; set; }
        public int? RequiredNumberOfAssigments { get; set; }
        public double? RequiredIndividualAssigmentPercentage { get; set; }
        public double? RequiredAvgAssigmentPercentage { get; set; }
        public int? NumberOfBigTests { get; set; }
        public int? RequiredNumberOfBigTests { get; set; }
        public double? RequiredIndividualBigTestPercentage { get; set; }
        public double? RequiredAvgBigTestPercentage { get; set; }
        public int? NumberOfSmallTests { get; set; }
        public int? RequiredNumberOfSmallTests { get; set; }
        public double? RequiredIndividualSmallTestPercentage { get; set; }
        public double? RequiredAvgSmallTestPercentage { get; set; }
        public double? OfferedGradeAPercentage { get; set; }
        public double? OfferedGradeBPercentage { get; set; }
        public double? OfferedGradeCPercentage { get; set; }
        public double? OfferedGradeDPercentage { get; set; }
        public bool? IsCorrectionTestCanWorseTheGrade { get; set; }

        public GradeCondition()
        {

        }
        public GradeCondition(string Type, int NumberOfTest, int RequiredNumberOfTest, double RequiredIndividualTestPercentage, double RequiredAvgTestPercentage, double Weight)
        {
            this.Type = Type;
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

        public GradeCondition(string Type, double GradeA, double GradeB, double GradeC, double GradeD)
        {
            this.Type = Type;
            switch (Type)
            {

                case "offeredGrade":
                    this.OfferedGradeAPercentage = GradeA;
                    this.OfferedGradeBPercentage = GradeB;
                    this.OfferedGradeCPercentage = GradeC;
                    this.OfferedGradeDPercentage = GradeD;
                    break;
            }
        }
    }

    public class OfferedGradeConditionWrapper
    {
        public OfferedGradeCondition GradeCondition { get; set; }
        public OfferedGradeConditionWrapper()
        {

        }
    }

    public class OfferedGradeCondition
    {
        public string Type { get; set; }
        public List<GradeCondition> Conditions { get; set; }

        public OfferedGradeCondition() { }
    }




}
