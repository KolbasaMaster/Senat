using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenatApi
{
    public class PageOfIssueVersionIssuesListItemDto
    {
        public Int32 PageNum { get; set; }
        public Int32 PageSize { get; set; }
        public List<IssueVersionInIssuesListItemDto>Items { get; set; }
        public Int32 ItemsTotal { get; set; }

    }
}
