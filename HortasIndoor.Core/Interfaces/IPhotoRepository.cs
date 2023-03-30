using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HortasIndoor.Core.Interfaces
{
    public interface IPhotoRepository
    {
        Task DeletePhoto(int id);
    }
}
