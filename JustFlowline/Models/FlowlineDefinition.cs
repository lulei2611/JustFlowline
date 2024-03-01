using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustFlowline.Models
{
    public class FlowlineDefinition
    {
        public string Id { get;set; }

        public Version Version { get; set; }

        public string Description { get;set; }

        public FlowlineUnitCollection Units { get; set; }

        public Type DataType { get; set; }
    }
}
