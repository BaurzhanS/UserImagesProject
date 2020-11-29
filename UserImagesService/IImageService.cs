using System;
using System.Collections.Generic;
using System.Text;
using UserImagesData;

namespace UserImagesService
{
    public interface IImageService
    {
        IEnumerable<Image> GetImages();
        Image GetImage(long id);
        void InsertImage(Image user);
        void UpdateImage(Image user);
        void DeleteImage(long id);
    }
}
