using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using UserImagesData;
using UserImagesRepo;

namespace UserImagesService
{
    public class ImageService: IImageService
    {
        private IRepository<Image> imageRepository;
        public ImageService(IRepository<Image> imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        public IQueryable<Image> GetImages()
        {
            return imageRepository.GetAll();
        }

        public Image GetImage(int id)
        {
            return imageRepository.Get(id);
        }

        public IQueryable<Image> FindImageByCondition(Expression<Func<Image, bool>> expression)
        {
            return imageRepository.FindByCondition(expression);
        }

        public void InsertImage(Image image)
        {
            imageRepository.Insert(image);
        }
        public void UpdateImage(Image image)
        {
            imageRepository.Update(image);
        }

        public void DeleteImage(int id)
        {
            Image image = GetImage(id);
            imageRepository.Remove(image);
            imageRepository.SaveChanges();
        }
    }
}
