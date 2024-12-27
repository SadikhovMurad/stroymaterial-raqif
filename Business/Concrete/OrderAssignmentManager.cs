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
    public class OrderAssignmentManager : IOrderAssignmentService
    {
        private readonly IOrderAssignmentDal orderAssignmentDal;
        private readonly IMapper _mapper;

        public OrderAssignmentManager(IOrderAssignmentDal orderAssignmentDal, IMapper mapper)
        {
            this.orderAssignmentDal = orderAssignmentDal;
            _mapper = mapper;
        }

        public IResult Add(OrderAssignmentDto orderAssignmentDto)
        {
            var orderAssignment = _mapper.Map<OrderAssignment>(orderAssignmentDto);
            orderAssignmentDal.Add(orderAssignment);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(int id)
        {
            var orderAssignment = orderAssignmentDal.Get(x => x.Id == id);
            if (orderAssignment == null)
            {
                return new ErrorResult("Hal hazirda bele bir sifaris yoxdur");
            }
            orderAssignmentDal.Delete(orderAssignment);
            return new SuccessResult(Messages.ProductRemoved);
        }

        public IDataResult<List<OrderAssignment>> GetAll()
        {
            return new SuccessDataResult<List<OrderAssignment>>(orderAssignmentDal.GetAll(), Messages.CategoryListed);
        }

        public IDataResult<List<OrderAssignment>> GetAllNotSuccessOrder()
        {
            return new SuccessDataResult<List<OrderAssignment>>(orderAssignmentDal.GetAll(oa => !oa.Status));
        }

        public IDataResult<List<OrderAssignment>> GetAllSuccessOrder()
        {
            return new SuccessDataResult<List<OrderAssignment>>(orderAssignmentDal.GetAll(oa => oa.Status));
        }

        public IDataResult<OrderAssignment> GetByFilter<T>(string propertyName, T value)
        {
            throw new NotImplementedException();
        }

        public IDataResult<OrderAssignment> GetById(int id)
        {
            return new SuccessDataResult<OrderAssignment>(orderAssignmentDal.Get(oa => oa.Id == id), Messages.GetProductById);
        }

        public IResult Update(int id, OrderAssignment? orderAssignment)
        {
            orderAssignmentDal.Update(orderAssignment);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
