using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overhaul.Domain.Common;

namespace Overhaul.Domain.Entities.App
{
    [Table(nameof(ErrorLog))]
    public class ErrorLog : Entity
    {
        public new int Id { get; set; }
        //public string Assembly { get; set; }
        public string Endpoint { get; set; }
        public string Error { get; set; }
        public string Path { get; set; }
    }
}
