namespace Task7
{
    public interface IRepository
    {
        void Save(Catalog catalog);
        Catalog Load();
    }
}