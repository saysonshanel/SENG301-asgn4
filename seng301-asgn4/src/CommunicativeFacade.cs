using Frontend4.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seng301_asgn4.src
{
    class CommunicativeFacade 
    {
        VendingMachine vendingMachine;
        private Dictionary<SelectionButton, int> buttonToIndex;

        public event EventHandler<SelectionEventArgs> SelectionMade;


        public CommunicativeFacade(VendingMachine vendingMachine)
        {
            this.vendingMachine = vendingMachine;

            this.buttonToIndex = new Dictionary<SelectionButton, int>();
            for (int i = 0; i < this.vendingMachine.Hardware.SelectionButtons.Length; i++)
            {
                this.vendingMachine.Hardware.SelectionButtons[i].Pressed += new EventHandler(SelectionButton_Pressed);
                this.buttonToIndex[this.vendingMachine.Hardware.SelectionButtons[i]] = i;
            }
        }

        private void SelectionButton_Pressed(object sender, EventArgs e)
        {
            var i = this.buttonToIndex[(SelectionButton)sender];
            var productName = this.vendingMachine.Hardware.ProductKinds[i].Name;
            var productCost = this.vendingMachine.Hardware.ProductKinds[i].Cost.Value;

            if(SelectionMade != null)
            {
                SelectionMade(this, new SelectionEventArgs() { index = i, pName = productName, pCost = productCost });
            }

        }
    }
}
