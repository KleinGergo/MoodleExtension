using System.Collections.Generic;

namespace MoodleExtensionAPI.Models
{
    public class SignatureCondition
    {

        public SignatureCondition(
            string type,
            List<Condition> conditions
        )
        {
            this.type = type;
            this.conditions = conditions;
        }

        public string type { get; }
        public IReadOnlyList<Condition> conditions { get; }
    }
}
