using System;

namespace Milkshake
{
    public class Stock : IAsset
    {
        public int NumShares { get; internal set; }

        public double PricePerShare { get; internal set; }

        public string Symbol { get; internal set; }
        public long Id { get; internal set; }

        public Stock()
        {
        }
        public Stock(string symbol, double priceshare, int amount)
        {
            this.Symbol = symbol;
            this.PricePerShare = priceshare;
            this.NumShares = amount;
        }
        public double GetValue()
        {
            return (double)(NumShares * PricePerShare);
        }

        public override string ToString()
        {

            string str = "Stock[symbol=" + this.Symbol + ",pricePerShare=" + this.PricePerShare + ",numShares=" + this.NumShares + "]";
            return str;
        }
        public override bool Equals(object obj)
        {
            Stock s = (Stock)obj;
            if (this.NumShares == s.NumShares && this.PricePerShare == s.PricePerShare && this.Symbol == s.Symbol)
                return true;

            return false;
        }

        public static double TotalValue(IAsset [] portfolio)
        {
            double totalvalue = 0;
            foreach (IAsset s in portfolio)
            {
                totalvalue +=s.GetValue();
            }
            return totalvalue;



        }
        public static double TotalValue(Stock[] portfolio)
        {
            double totalvalue = 0;
            foreach (Stock s in portfolio)
            {
                totalvalue += s.GetValue();
            }
            return totalvalue;
        }
    }
}