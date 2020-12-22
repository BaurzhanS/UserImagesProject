using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using UserImagesData;
using UserImagesRepo;

namespace UserImagesService
{
    public interface INotificationService
    {
        IQueryable<Notification> GetNotifications();
        Notification GetNotification(int id);
        IQueryable<Notification> FindNotificationByCondition(Expression<Func<Notification, bool>> expression);
        void InsertNotification(Notification image);
        void UpdateNotification(Notification image);
        void DeleteNotification(int id);
    }
}
