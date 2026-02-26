using MinecraftSkinsSite.Data;
using MinecraftSkinsSite.Models;

namespace MinecraftSkinsSite.Repositories
{
    public class SkinRepository : ISkinRepository
    {
        private readonly IInMemoryDatabase db;

        public SkinRepository(IInMemoryDatabase db)
        {
            this.db = db;
        }

        public List<Skin> GetAll()
        {
            return db.Skins;
        }

        public Skin? GetById(int id)
        {
            return db.Skins.FirstOrDefault(x => x.Id == id);
        }
    }
}
