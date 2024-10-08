﻿using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProje.Controllers
{
    public class AboutController : Controller
    {
        AboutManager am = new AboutManager(new EFAboutDal());
        public ActionResult Index()
        {
            var aboutValues = am.GetList();
            return View(aboutValues);            
        }

        [HttpGet]
        public ActionResult AddAbout() 
        {
            return View();  
        }


        [HttpPost]
        public ActionResult AddAbout(About p)
        {
            am.AboutAdd(p);
            return RedirectToAction("Index");
        }

        public PartialViewResult AboutPartial()
        {
            return PartialView();
        }
    }
}