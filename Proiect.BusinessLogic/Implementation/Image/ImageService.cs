using Proiect.BusinessLogic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.BusinessLogic.Implementation.Image
{
    public class ImageService : BaseService
    {
        public ImageService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {

        }

        public byte[] GetImageById(int imageId)
        {
            var image = UnitOfWork.Images.Get().Where(u => u.Id == imageId).Select(image => image.ImageContent).FirstOrDefault();
            return image;
        }
    }
}
