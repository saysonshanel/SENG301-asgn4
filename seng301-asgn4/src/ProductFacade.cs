using Frontend4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seng301_asgn4.src
{
    class ProductFacade
    {
        VendingMachine vendingMachine;
        public ProductFacade(VendingMachine vendingMachine)
        {
            this.vendingMachine = vendingMachine;
        }

        public void DispenseProduct(int i)
        {
            this.vendingMachine.Hardware.ProductRacks[i].DispenseProduct();
        }
        public void UnloadProduct(int i)
        {
            this.vendingMachine.Hardware.ProductRacks[i].Unload();
        }
    }
}
