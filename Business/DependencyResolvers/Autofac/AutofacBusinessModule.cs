using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();

            builder.RegisterType<SubcategoryManager>().As<ISubcategoryService>();
            builder.RegisterType<EfSubcategoryDal>().As<ISubcategoryDal>();

            builder.RegisterType<ProductManager>().As<IProductService>();
            builder.RegisterType<EfProductDal>().As<IProductDal>();

            builder.RegisterType<OrderManager>().As<IOrderService>();
            builder.RegisterType<EfOrderDal>().As<IOrderDal>();

            builder.RegisterType<OrderHistoryManager>().As<IOrderHistoryService>();
            builder.RegisterType<EfOrderHistoryDal>().As<IOrderHistoryDal>();

            builder.RegisterType<OrderAssignmentManager>().As<IOrderAssignmentService>();
            builder.RegisterType<EfOrderAssignmentDal>().As<IOrderAssignmentService>();

            builder.RegisterType<NotificationManager>().As<INotificationService>();
            builder.RegisterType<EfNotificationDal>().As<INotificationDal>();

            builder.RegisterType<EmployeeManager>().As<IEmployeeService>();
            builder.RegisterType<EfEmployeeDal>().As<IEmployeeDal>();
        }
    }
}
