using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessMasterUTH.Control
{
    public partial class ControlCuadricula : Component
    {
        public ControlCuadricula()
        {
            InitializeComponent();
        }

        public ControlCuadricula(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
