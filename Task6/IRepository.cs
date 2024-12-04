namespace Task6
{
    public interface IRepository
    {
        void Save(Catalog catalog);
        Catalog Load();
    }
}