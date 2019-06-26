using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenatApi
{
    public class MaterialVersionDto
    {
        public Int32 Version { get; set; }
        public string FileName { get; set; }
        public EmployeeDto CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Int32 Size { get; set; }
        public bool Encrypted { get; set; } 
    }
}
