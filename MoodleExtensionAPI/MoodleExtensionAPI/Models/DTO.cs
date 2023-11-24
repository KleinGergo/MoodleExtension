namespace MoodleExtensionAPI.Models
{
    public class StudentTestSignatureDTO
    {
        public string NeptunCode { get; set; }
        public List<TestResultDTO> Tests { get; set; }
        public string SignatureStatus { get; set; }
        public string Grade { get; set; }
        public string OfferedGrade { get; set; }
    }

    public class TestResultDTO
    {
        public string TestName { get; set; }
        public string Type { get; set; }
        public double? GradeMax { get; set; }
        public double? Result { get; set; }
    }
}
