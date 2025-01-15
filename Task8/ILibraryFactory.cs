namespace Task8
{
    public abstract class ILibraryFactory
    {
        protected string FilePath;
        protected ILibraryFactory(string filePath)
        {
            FilePath = filePath;
        }
        public abstract Catalog GetCatalog();
        public abstract List<string> GetPressRelease();
    }
}