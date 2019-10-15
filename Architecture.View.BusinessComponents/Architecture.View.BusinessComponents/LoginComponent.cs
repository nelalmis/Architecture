using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.UI.BusinessComponents
{
    public partial class LoginComponent : Component
    {
        public LoginComponent()
        {
            InitializeComponent();
        }

        public LoginComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
