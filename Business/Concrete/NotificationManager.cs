using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using Entity.DtoS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class NotificationManager : INotificationService
    {
        private readonly INotificationDal notificationDal;
        private readonly IMapper _mapper;

        public NotificationManager(INotificationDal notificationDal, IMapper mapper)
        {
            this.notificationDal = notificationDal;
            _mapper = mapper;
        }

        public IResult Add(Notification Notification)
        {
            notificationDal.Add(Notification);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(int id)
        {
            var notification = notificationDal.Get(x => x.Id == id);
            if (notification == null)
            {
                return new ErrorResult("Hal hazirda bele bir bildiris yoxdur");
            }
            notificationDal.Delete(notification);
            return new SuccessResult(Messages.ProductRemoved);
        }

        public IDataResult<List<Notification>> GetAll()
        {
            return new SuccessDataResult<List<Notification>>(notificationDal.GetAll(), Messages.CategoryListed);
        }

        public IDataResult<Notification> GetByFilter<T>(string propertyName, T value)
        {
            throw new NotImplementedException();
        }

        public IDataResult<Notification> GetById(int id)
        {
            return new SuccessDataResult<Notification>(notificationDal.Get(oa => oa.Id == id), Messages.GetProductById);
        }

        public IResult Update(int id, Notification? Notification)
        {
            notificationDal.Update(Notification);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
