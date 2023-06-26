using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Sign
    {
        public int SignId { get; set; }

        public string SignName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Coordinates { get; set; }
        public string Comment { get; set; }
        public string Incident { get; set; }
        public string Ice { get; set; }
        public string PhotoFileName { get; set; }
    }
}
