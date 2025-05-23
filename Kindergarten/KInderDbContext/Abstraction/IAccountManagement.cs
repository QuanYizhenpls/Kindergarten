﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderDbContext.Abstraction
{
    public interface IAccountManagement<T>
    {
        public Task<T?> GetAccount(string login, string password);
    }
}
