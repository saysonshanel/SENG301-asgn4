using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seng301_asgn4.src
{
    class BusinessRule
    {
        PaymentFacade paymentFacade;
        CommunicativeFacade communicativeFacade;
        ProductFacade productFacade;

        public BusinessRule(PaymentFacade paymentFacade, CommunicativeFacade communicativeFacade, ProductFacade productFacade)
        {
            this.paymentFacade = paymentFacade;
            this.communicativeFacade = communicativeFacade;
            this.productFacade = productFacade;

            this.communicativeFacade.SelectionMade += new EventHandler<SelectionEventArgs>(selection_made);
        }

        private void selection_made(object sender, SelectionEventArgs e)
        {
            if(this.paymentFacade.funds >= e.pCost)
            {
                productFacade.DispenseProduct(e.index);
                paymentFacade.storeCoins();
                this.paymentFacade.funds = paymentFacade.deliverChange(e.pCost, paymentFacade.funds);
            }
        }
    }
}
