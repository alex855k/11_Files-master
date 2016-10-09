using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Milkshake
{
    public class FileStockRepository : IStockRepository, IFileRepository
    {
        private DirectoryInfo repositoryDir;
        private const string idPath = "currentid.txt";
        private int currentID = 1;

        public FileStockRepository(DirectoryInfo repositoryDir)
        {
            this.repositoryDir = repositoryDir;
            DirectoryExists();
        }

        private void DirectoryExists() {
            if (!this.repositoryDir.Exists)
            {
                repositoryDir.Create();
            }
        }

        private void WriteCurrentIdToFile() {
            using (StreamWriter sw = new StreamWriter(repositoryDir + idPath))
            {
                sw.WriteLine(currentID);
                sw.Close();
            }
        }

        public long NextId()
        {
            DirectoryExists();
            
            if (!File.Exists(repositoryDir + idPath)) {
                WriteCurrentIdToFile();
            }
            else
            {
                using (StreamReader sr = new StreamReader(repositoryDir + idPath))
                {
                    string cID = sr.ReadLine();
                    sr.Close();
                    int.TryParse(cID, out currentID);
                    currentID++;
                    WriteCurrentIdToFile();
                }
            }
            return currentID;
        }

        public string StockFileName(long id)
        {
            return "stock" + id + ".txt";
        }

        public string StockFileName(Stock s)
        {
            return "stock" + s.Id + ".txt";
        }

        public void SaveStock(Stock s)
        {
            if (s.Id == 0) s.Id = NextId();
            using (StreamWriter sw = new StreamWriter(repositoryDir + StockFileName(s)))
            {
                sw.WriteLine(s.Symbol);
                sw.WriteLine(s.PricePerShare);
                sw.WriteLine(s.NumShares);
                sw.Close();
            }
        }

        public Stock LoadStock(long id)
        {
            Stock s = new Stock();
            s.Id = id;
            using (StreamReader sr = new StreamReader(repositoryDir + StockFileName(id)))
            {
                int nOfShares = 0;
                double pPerShare = 0;
                s.Symbol = sr.ReadLine();
                double.TryParse(sr.ReadLine(),out pPerShare);
                int.TryParse(sr.ReadLine(),out nOfShares);
                s.NumShares = nOfShares;
                s.PricePerShare = pPerShare;
            }
            return s;
        }

        public void Clear()
        {
            this.DeleteDirectory(repositoryDir.ToString());
            repositoryDir.Create();
            currentID = 1;
        }

        public ICollection FindAllStocks()
        {
            List<Stock> _stocks = new List<Stock>();
            foreach (FileInfo f in repositoryDir.GetFiles()) {
                if (f.Name.Contains("stock"))
                {
                    long id; 
                    long.TryParse(Regex.Match(f.Name, @"\d+").Value, out id);
                    _stocks.Add(LoadStock(id));
                }
            }
            return _stocks;    
        }

        private void DeleteDirectory(string directory)
        {
            string[] files = Directory.GetFiles(directory);
            string[] dirs = Directory.GetDirectories(directory);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(directory, false);
        }
    }
}