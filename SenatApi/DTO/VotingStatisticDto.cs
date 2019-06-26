using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenatApi
{
    public class VotingStatisticDto
    {
        public Guid Id { get; set; }
        public Int32 For { get; set; }
        public Int32 Against { get; set; }
        public Int32 Abstain { get; set; }
        public Int32 Veto { get; set; }
    }
}
