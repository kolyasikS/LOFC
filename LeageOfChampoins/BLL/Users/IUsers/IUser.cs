﻿using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Users
{
    public interface IUser
    {
        public string? GetUser();
        public void SetUser(string user);
        public void SetUser(Owner owner);
        public Owner? GetOwnerUser();
    }
}
