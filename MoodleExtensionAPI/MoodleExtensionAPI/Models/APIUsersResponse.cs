namespace MoodleExtensionAPI.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class Enrolledcourse
    {
        public int id { get; set; }
        public string? fullname { get; set; }
        public string? shortname { get; set; }
    }

    public class Preference
    {
        public string? name { get; set; }
        public object? value { get; set; }
    }

    public class Role
    {
        public int roleid { get; set; }
        public string? name { get; set; }
        public string? shortname { get; set; }
        public int? sortorder { get; set; }
    }

    public class APIUsersResponse
    {
        public int id { get; set; }
        public string? username { get; set; }
        public string? firstname { get; set; }
        public string? lastname { get; set; }
        public string? fullname { get; set; }
        public string? email { get; set; }
        public string? department { get; set; }
        public int? firstaccess { get; set; }
        public int? lastaccess { get; set; }
        public int? lastcourseaccess { get; set; }
        public string? description { get; set; }
        public int? descriptionformat { get; set; }
        public string? profileimageurlsmall { get; set; }
        public string? profileimageurl { get; set; }
        public List<Role>? roles { get; set; }
        public List<Preference>? preferences { get; set; }
        public List<Enrolledcourse>? enrolledcourses { get; set; }
    }


}
