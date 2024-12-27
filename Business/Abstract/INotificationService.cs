using Core.Utilities.Results;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface INotificationService
    {
        IDataResult<List<Notification>> GetAll();
        IDataResult<Notification> GetById(int id);
        IDataResult<Notification> GetByFilter<T>(string propertyName, T value);
        IResult Add(Notification Notification);
        IResult Update(int id, Notification? Notification);
        IResult Delete(int id);
    }
}
