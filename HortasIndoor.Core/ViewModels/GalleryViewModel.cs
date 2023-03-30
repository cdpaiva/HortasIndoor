using HortasIndoor.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HortasIndoor.Core.ViewModels
{
    public class GalleryViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Localizacao { get; set; }
        public List<DisplayPhotoViewModel> Photos { get; set; }

        public GalleryViewModel() { }

        public GalleryViewModel(ApplicationUser user)
        {
            UserName = user.UserName;
            Email = user.Email;
            if (user.Localizacao != null)
            {
                Localizacao = user.Localizacao;
            }
            Photos = new List<DisplayPhotoViewModel>();

            if (user.Photos != null)
            {
                foreach (var photo in user.Photos)
                {
                    var photoVM = new DisplayPhotoViewModel()
                    {
                        Bytes = Convert.ToBase64String(photo.Bytes),
                        Id = photo.Id,
                        Description = photo.Description,
                    };
                    Photos.Add(photoVM);
                }
            }
        }
    }

    public class DisplayPhotoViewModel
    {
        public int Id;
        public string Description;
        public string Bytes;
    }
}
