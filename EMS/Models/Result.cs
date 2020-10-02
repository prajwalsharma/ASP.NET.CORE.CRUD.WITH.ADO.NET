using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Models
{
    public class Result
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
