using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Aspects
{
    //public class SecuredOperation : MethodInterception
    //{
    //    private string[] _roles;
    //    private readonly IHttpContextAccessor _httpContextAccessor;

    //    public SecuredOperation(string roles)
    //    {
    //        _roles = roles.Split(',');
    //        _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
    //    }

    //    protected override void OnBefore(IInvocation invocation)
    //    {
    //        var httpContext = _httpContextAccessor.HttpContext;

    //        // Authorization headerini yoxlayırıq
    //        var authorizationHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();

    //        // Əgər Authorization headeri yoxdursa, 401 qaytarırıq
    //        if (string.IsNullOrEmpty(authorizationHeader))
    //        {
    //            throw new Exception("Authorization header is missing.");
    //        }

    //        // İstifadəçinin rolunu əldə edirik
    //        var roleClaims = httpContext.User.ClaimRoles();

    //        // Rol yoxlaması
    //        bool roleFound = false;
    //        foreach (var role in _roles)
    //        {
    //            if (roleClaims.Contains(role))
    //            {
    //                roleFound = true;
    //                break;
    //            }
    //        }

    //        // Əgər uyğun rol tapılmadısa, icazə verilmir
    //        if (!roleFound)
    //        {
    //            throw new Exception(Messages.AuthorizationDenied);
    //        }
    //    }
    //}
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
