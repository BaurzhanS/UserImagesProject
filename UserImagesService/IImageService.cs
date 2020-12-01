using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using UserImagesData;

namespace UserImagesService
{
    public interface IImageService
    {
        IQueryable<Image> GetImages();
        Image GetImage(long id);
        IQueryable<Image> FindImageByCondition(Expression<Func<Image, bool>> expression);
        void InsertImage(Image user);
        void UpdateImage(Image user);
        void DeleteImage(long id);
    }
}
