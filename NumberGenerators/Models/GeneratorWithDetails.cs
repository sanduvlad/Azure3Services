using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NumberGenerators.Models
{
    public class GeneratorWithDetails
    {
        public Generator _Generator { get; set; }
        public List<int> Numbers { get; set;}
    }
}