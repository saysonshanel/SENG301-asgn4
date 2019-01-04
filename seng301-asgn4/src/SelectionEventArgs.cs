using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seng301_asgn4.src
{
    class SelectionEventArgs : EventArgs
    {
        public int index { get; set; }
        public string pName { get; set; }
        public int pCost { get; set; }
    }
}
