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
            return UnitOfWork.Images.Get().Where(u => u.Id == imageId).Select(u => u.ImageContent).FirstOrDefault();
        }
    }
}
