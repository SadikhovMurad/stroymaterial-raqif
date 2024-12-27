using Business.Abstract;
using Core.Utilities.Results;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class NotificationManager : INotificationService
    {
        public IResult Add(Notification Notification)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Notification>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<Notification> GetByFilter<T>(string propertyName, T value)
        {
            throw new NotImplementedException();
        }

        public IDataResult<Notification> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IResult Update(int id, Notification? Notification)
        {
            throw new NotImplementedException();
        }
    }
}
