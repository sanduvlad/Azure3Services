using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NumberGenerators.Models
{
    public class Model
    {
        public Generator generator { get; set; }
        public List<IntNumber> numberList { get; set; }
    }
}