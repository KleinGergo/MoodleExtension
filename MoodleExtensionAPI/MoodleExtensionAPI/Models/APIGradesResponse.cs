namespace MoodleExtensionAPI.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Gradeitem
    {
        public int id { get; set; }
        public string itemname { get; set; }
        public string itemtype { get; set; }
        public string itemmodule { get; set; }
        public int iteminstance { get; set; }
        public int? itemnumber { get; set; }
        public string idnumber { get; set; }
        public int? categoryid { get; set; }
        public object outcomeid { get; set; }
        public object scaleid { get; set; }
        public bool locked { get; set; }
        public int cmid { get; set; }
        public int weightraw { get; set; }
        public string weightformatted { get; set; }
        public int graderaw { get; set; }
        public int? gradedatesubmitted { get; set; }
        public int gradedategraded { get; set; }
        public bool gradehiddenbydate { get; set; }
        public bool gradeneedsupdate { get; set; }
        public bool gradeishidden { get; set; }
        public bool gradeislocked { get; set; }
        public bool gradeisoverridden { get; set; }
        public string gradeformatted { get; set; }
        public int grademin { get; set; }
        public int grademax { get; set; }
        public string rangeformatted { get; set; }
        public string percentageformatted { get; set; }
        public string feedback { get; set; }
        public int feedbackformat { get; set; }
    }

    public class APIGradesResponse
    {
        public List<Usergrade> usergrades { get; set; }
        public List<object> warnings { get; set; }
    }

    public class Usergrade
    {
        public int courseid { get; set; }
        public string courseidnumber { get; set; }
        public int userid { get; set; }
        public string userfullname { get; set; }
        public string useridnumber { get; set; }
        public int maxdepth { get; set; }
        public List<Gradeitem> gradeitems { get; set; }
    }


}
