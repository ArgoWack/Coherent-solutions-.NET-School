namespace Task8
{
    public interface IRepository
    {
        void Save(Catalog catalog);
        Catalog Load();
    }
}