namespace Task6
{
    public interface IRepository
    {
        void Save(Catalog catalog, string filePath);
        Catalog Load(string filePath);
    }
}