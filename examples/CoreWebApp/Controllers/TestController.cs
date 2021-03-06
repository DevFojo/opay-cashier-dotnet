﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpayCashier;
using OpayCashier.Models;

namespace CoreWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ICashierService _cashierService;

        public TestController(ICashierService cashierService)
        {
            _cashierService = cashierService;
        }

        [HttpGet("order")]
        public async Task<BaseResponse<OrderData>> Order()
        {
            var orderRequest = new OrderRequest
            {
                Reference = $"test_{DateTime.Now.Ticks}",
                MchShortName = "Jerry's shop",
                ProductName = "Apple AirPods Pro",
                ProductDesc = "The best wireless earphone in history. Cannot agree more! Right!",
                UserPhone = "+2349876543210",
                UserRequestIp = "154.113.66.58",
                Amount = "100",
                Currency = "NGN",
                PayTypes = new List<PayType>
                {
                    PayType.BalancePayment,
                    PayType.BonusPayment
                },
                PayMethods = new List<PayMethod>
                {
                    PayMethod.Account,
                    PayMethod.QrCode
                },
                CallbackUrl = "http://example.com/callback",
                ReturnUrl = "http://example.com/return",
                ExpireAt = "1"
            };

            return await _cashierService.Order(orderRequest);
        }

        [HttpGet("query")]
        public async Task<BaseResponse<QueryData>> Query()
        {
            var queryRequest = new QueryRequest
            {
                OrderNo = "200408141665571675",
                Reference = "test_637219088240003560"
            };

            return await _cashierService.Query(queryRequest);
        }

        [HttpGet("close")]
        public async Task<BaseResponse<CloseData>> Close()
        {
            var closeRequest = new CloseRequest
            {
                OrderNo = "200408141665571675",
                Reference = "test_637219088240003560"
            };

            return await _cashierService.Close(closeRequest);
        }
    }
}