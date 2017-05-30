using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;

namespace NumberGenerators.Models
{
    public class IntNumber : TableEntity
    {
        public int Number { get; set; }
    }
}