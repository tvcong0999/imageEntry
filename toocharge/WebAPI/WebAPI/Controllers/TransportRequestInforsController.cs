﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using WebAPI.DL.IServices;
using WebAPI.Service.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportRequestInforsController : ControllerBase
    {
        private readonly ITransportRequestInforService _transportRequestInforService;
        public TransportRequestInforsController(ITransportRequestInforService transportRequestInforService)
        {
            _transportRequestInforService = transportRequestInforService;
        }
        //GET: api/TransportRequestInfors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransportRequestInfor>> GetTransportRequestInfor(string id)
        {
            var rs = await _transportRequestInforService.getTransportRequestInfor(id);
            return Ok(rs);
        }
    }
}
