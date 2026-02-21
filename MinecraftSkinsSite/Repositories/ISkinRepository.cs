using MinecraftSkinsSite.Models;

namespace MinecraftSkinsSite.Repositories
{
    public interface ISkinRepository
    {
        List<Skin> GetAll();
        Skin? GetById(int id);
    }
}
