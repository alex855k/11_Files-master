using System;
using System.IO;

namespace Milkshake
{
    internal class StockIO
    {
        public void WriteStock(StringWriter sw, Stock s)
        {
            sw.WriteLine(s.Symbol);
            sw.WriteLine(s.PricePerShare);
            sw.WriteLine(s.NumShares);
            sw.Close();

        }

        internal Stock ReadStock(StringReader data)
        {
            Stock s = new Stock();
            string name = data.ReadLine();
            double price = double.Parse (data.ReadLine());
            int amount = int.Parse(data.ReadLine());
            s = new Stock(name, price, amount);
            data.Close();
            return s;
        }

        public void WriteStock(FileInfo output, Stock s)
        {
            using (StreamWriter sr = output.CreateText())
            {
            sr.Write(s.Symbol + "\n");
            sr.Write(s.PricePerShare + "\n");
            sr.Write(s.NumShares + "\n");
            sr.Close();
            }
            
        }

        public Stock ReadStock(FileInfo output)
        {
            Stock s = null;
            using (StreamReader sr = output.OpenText())
            {
                string name = sr.ReadLine();
                double price;
                Double.TryParse(sr.ReadLine(), out price);
                int amount;
                int.TryParse(sr.ReadLine(), out amount);
                sr.Close();
                s = new Stock(name, price, amount);
            }   
            return s;
        }
    }
}