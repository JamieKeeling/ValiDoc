using System.Collections.Generic;

namespace ValiDoc.Output
{
    public class RuleDescriptor
    {
        public string MemberName { get; set; }
        public IEnumerable<RuleDescription> Rules { get; set; }
    }
}
