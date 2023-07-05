namespace MoodleExtensionAPI.Models
{
    public class Condition
    {
        public string type { get; set; }
        public bool required { get; set; }
        public int? minimumPercentage { get; set; }
    }
}
