﻿using Core.DataAccess;
using Core.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal : IRepositoryBase<User>
    {
        List<OperationClaim> GetClaims(User user);
    }
}
