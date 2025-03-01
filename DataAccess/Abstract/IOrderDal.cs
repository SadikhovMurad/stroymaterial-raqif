using Core.DataAccess;
using Entity.Concrete;
using Entity.DtoS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IOrderDal:IRepositoryBase<Order>
    {
        void CreateOrder(OrderDto order,Guid userId);
        public List<OrderForListDto> GetAllOrderWithDetails();
        public List<OrderForListDto> GetAllSuccessOrders();
        public List<OrderForListDto> GetAllNotSuccessOrders();
        void ChangeStatus(Guid id);
    }
}
