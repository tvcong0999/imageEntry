﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DL.Models;
using WebAPI.Service.Models;

namespace WebAPI.DL.IServices
{
    public interface ICatChargeService
    {
        public Task<List<catCharge>> GetAll();
    }
}
