using Frontend4;
using Frontend4.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seng301_asgn4.src
{
    class PaymentFacade 
    {
        VendingMachine vendingMachine;
        public int funds { get; set; }

        private Dictionary<int, int> coinToIndex;

        public PaymentFacade(VendingMachine vendingMachine)
        {
            this.vendingMachine = vendingMachine;
            this.funds = 0;

            CoinSlot coinSlot = vendingMachine.Hardware.CoinSlot;
            coinSlot.CoinAccepted += new EventHandler<CoinEventArgs>(coinSlot_AcceptCoin);

            this.coinToIndex = new Dictionary<int, int>();
            for(int i = 0; i < vendingMachine.Hardware.CoinRacks.Length; i++)
            {
                coinToIndex[this.vendingMachine.Hardware.GetCoinKindForCoinRack(i).Value] = i;

            }
            
        }

        private void coinSlot_AcceptCoin(object sender, CoinEventArgs e)
        {
            Cents cents = e.Coin.Value;
            funds += cents.Value;

        }


        public void storeCoins()
        {
            this.vendingMachine.Hardware.CoinReceptacle.StoreCoins();
        }

        public int deliverChange(int cost, int funds)
        {
            var changeNeeded = funds - cost;

            while (changeNeeded > 0)
            {
                var coinRacksWithMoney = this.coinToIndex.Where(ck => ck.Key <= changeNeeded && this.vendingMachine.Hardware.CoinRacks[ck.Value].Count > 0).OrderByDescending(ck => ck.Key);

                if (coinRacksWithMoney.Count() == 0)
                {
                    return changeNeeded; // this is what's left as available funds
                }

                var biggestCoinRackCoinKind = coinRacksWithMoney.First().Key;
                var biggestCoinRackIndex = coinRacksWithMoney.First().Value;
                var biggestCoinRack = this.vendingMachine.Hardware.CoinRacks[biggestCoinRackIndex];

                changeNeeded = changeNeeded - biggestCoinRackCoinKind;
                biggestCoinRack.ReleaseCoin();
            }

            return 0;
        }
    }
}
