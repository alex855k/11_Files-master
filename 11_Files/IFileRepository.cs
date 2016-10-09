namespace Milkshake
{
    internal interface IFileRepository
    {
        string StockFileName(long id);
        string StockFileName(Stock s);
        void SaveStock(Stock yhoo);
    }
}