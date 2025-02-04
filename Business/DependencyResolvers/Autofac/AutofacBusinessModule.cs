using Autofac;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
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

            builder.RegisterType<OrderItemManager>().As<IOrderItemService>();
            builder.RegisterType<EfOrderItemDal>().As<IOrderItemDal>();

            builder.RegisterType<OrderHistoryManager>().As<IOrderHistoryService>();
            builder.RegisterType<EfOrderHistoryDal>().As<IOrderHistoryDal>();

            builder.RegisterType<OrderAssignmentManager>().As<IOrderAssignmentService>();
            builder.RegisterType<EfOrderAssignmentDal>().As<IOrderAssignmentDal>();

            builder.RegisterType<CartManager>().As<ICartService>();
            builder.RegisterType<EfCartDal>().As<ICartDal>();

            builder.RegisterType<EmployeeManager>().As<IEmployeeService>();
            builder.RegisterType<EfEmployeeDal>().As<IEmployeeDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JWTHelper>().As<ITokenHelper>();

            builder.Register(context =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    // Burada yalnız lazım olan yığmaları əlavə edin
                    cfg.AddMaps(typeof(CategoryManager).Assembly);
                });
                return config.CreateMapper();
            }).As<IMapper>().InstancePerLifetimeScope();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
