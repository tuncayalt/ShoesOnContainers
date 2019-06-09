namespace ProductCatalog.Domain.Business
{
    public interface IPicBusiness
    {
        byte[] GetImageById(int id, string webRootPath);
    }
}
