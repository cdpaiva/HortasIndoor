using HortasIndoor.Core.Interfaces;
using HortasIndoor.DataAccess.Data;

namespace HortasIndoor.DataAccess.Service
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly HIContext _context;

        public PhotoRepository(HIContext context)
        {
            this._context = context;
        }

        public async Task DeletePhoto(int id)
        {
            var photo = _context.Photos.FirstOrDefault(p => p.Id == id);
            if(photo != null)
            {
                _context.Photos.Remove(photo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
