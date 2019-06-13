using System;

namespace SenatApi
{
    public class Identific
    {
        public Guid Id { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }

    public class IssueVersionShort
    {
        public Identific Issue { get; set; }

        public override string ToString()
        {
            return Issue.ToString();
        }
    }
}
