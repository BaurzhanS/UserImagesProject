using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using UserImagesData;
using UserImagesRepo;

namespace UserImagesService
{
    public class NotificationService: INotificationService
    {
        private IRepository<Notification> notificationRepository;
        public NotificationService(IRepository<Notification> notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }
        public IQueryable<Notification> GetNotifications()
        {
            return notificationRepository.GetAll();
        }

        public Notification GetNotification(int id)
        {
            return notificationRepository.Get(id);
        }

        public IQueryable<Notification> FindNotificationByCondition(Expression<Func<Notification, bool>> expression)
        {
            return notificationRepository.FindByCondition(expression);
        }

        public void InsertNotification(Notification notification)
        {
            notificationRepository.Insert(notification);
        }
        public void UpdateNotification(Notification notification)
        {
            notificationRepository.Update(notification);
        }

        public void DeleteNotification(int id)
        {
            Notification notification = GetNotification(id);
            notificationRepository.Remove(notification);
            notificationRepository.SaveChanges();
        }
    }
}
