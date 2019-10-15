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
    public partial class CountryRequest:RequestBase
    {
        public CountryContract Contract { get; set; }

        public CountryRequest()
        {
            Contract = new CountryContract();
        }
    }
}
