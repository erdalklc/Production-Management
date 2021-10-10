﻿using EPM.Production.Monitoring.Dto.Models;
using EPM.Service.Base;
using EPM.Web.ServiceHelper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Web.Controllers
{
    public class UretimIzleController : Controller
    {
        private readonly IUretimIzleService _uretimIzleService;

        public UretimIzleController(IUretimIzleService uretimIzleService)
        {
            _uretimIzleService = uretimIzleService;
        }
        public IActionResult UretimIzle()
        {
            return View(new FilterModel());
        }
        [HttpPost, HttpGet]
        public IActionResult UretimIzleFilter(FilterModel tt)
        { 
            return Json(MonitoringServiceHelper.GetTerminList(tt));
        }

        [HttpPost, HttpGet]
        public IActionResult GetProductList(HaftaModel model,FilterModel filterModel)
        {
            return Json(MonitoringServiceHelper.GetProductList(model,filterModel));
        }

        [HttpPost, HttpGet]
        public IActionResult GetProductionDetails(HaftaModel haftaModel,ProductModel productModel, FilterModel filterModel)
        {
            return Json(MonitoringServiceHelper.GetProductionDetails(haftaModel, productModel, filterModel));
        }
        [HttpPost, HttpGet]
        public IActionResult GetProductionDetailsByDate(HaftaModel haftaModel, ProductModel productModel, FilterModel filterModel,DateTime Tarih)
        {
            return Json(MonitoringServiceHelper.GetProductionDetailsByDate(haftaModel, productModel, filterModel, Tarih));
        }


    }
}
