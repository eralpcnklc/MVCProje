﻿using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProje.Controllers
{
    public class HeadingController : Controller
    {
        HeadingManager hm = new HeadingManager(new EFHeadingDal());
        CategoryManager cm = new CategoryManager(new EFCategoryDal());
        WriterManager wm = new WriterManager(new EFWriterDal());

        public ActionResult Index()
        {
            var headingValues = hm.GetList();
            return View(headingValues);
        }

        public ActionResult AddHeading()
        {
            //kategori sınıfındaki değerleri bir id ve isim olarak tutucak
            List<SelectListItem> valueCategory = (from x in cm.GetList() select new SelectListItem { Text =x.CategoryName,Value = x.CategoryID.ToString()}).ToList();

            //dropdownlist
            List<SelectListItem> valueWriter = (from x in wm.GetList() select new SelectListItem { Text = x.WriterName + " " + x.WriterSurName, Value = x.WriterID.ToString() }).ToList();
            ViewBag.vlw = valueWriter;
            //view tarafına taşımak için
            ViewBag.vlc = valueCategory;
            return View();
        }

        [HttpPost]
        public ActionResult AddHeading(Heading heading)
        {
            heading.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            hm.HeadingAdd(heading);
            return RedirectToAction("Index");
        
        }
        
        public ActionResult DeleteHeading(int id)
        {
            var headingValue = hm.GetByID(id);
            headingValue.HeadingStatus = false;
            hm.HeadingDelete(headingValue);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditHeading(int id)
        {
            List<SelectListItem> valueCategory = (from x in cm.GetList() 
                                                  select new SelectListItem
                                                  { 
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString() }).
                                                      ToList();
            ViewBag.vlc = valueCategory;
            var headingValue = hm.GetByID(id);
            return View(headingValue);
        }
        [HttpPost]
        public ActionResult EditHeading(Heading heading) 
        {
            hm.HeadingUpdate(heading);
            return RedirectToAction("Index");
        }

        
    }
}