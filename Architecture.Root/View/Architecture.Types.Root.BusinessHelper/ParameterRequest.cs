using Architecture.Base;
using Architecture.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Types.Root.BusinessHelper
{
    [Serializable]
    public partial class ParameterRequest:RequestBase
    {
        public string ParamType { get; set; }
        public string ParamCode { get; set; }
        public ParameterRequest() { }
    }
}
