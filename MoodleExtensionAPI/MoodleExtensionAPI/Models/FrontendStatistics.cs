namespace MoodleExtensionAPI.Models
{
    public class FrontendStatistics
    {
        public string SubjectName { get; set; }
        public int TotalTests { get; set; }
        public int TotalSignatures { get; set; }
        public int TotalNumberOfPassedStudents { get; set; }
        public List<FrontendChartData> frontendChartData { get; set; }
        public FrontendStatistics()
        {
            this.TotalTests = 0;
            this.TotalSignatures = 0;
            this.TotalNumberOfPassedStudents = 0;
            frontendChartData = new List<FrontendChartData>();

        }

    }
    public class FrontendChartData
    {
        public string name { get; set; }
        public int Jegy { get; set; }

        public FrontendChartData(string name)
        {
            this.name = name;
            Jegy = 0;
        }
    }
}

