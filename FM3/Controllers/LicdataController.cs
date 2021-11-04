﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FM3.Models;
using FM3.Services;

namespace FM3.Controllers
{
    public class LicdataController : Controller
    {
        private FM3DBEntities fM3DB = new FM3DBEntities();
        private FB3DBEntities fB3DB = new FB3DBEntities();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                fM3DB.Dispose();
                fB3DB.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Licdata
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LicMaintain(ACESFunc fundata)
        {
            UsersServices userService = new UsersServices();
            List<Users> users = userService.GetUsers(fundata);
            CommonServices commonServices = new CommonServices();

            ViewBag.SelectList = commonServices.GetSelectList();
            Session.Add("UserID", users[0].Userid);
            Session.Add("UserName", users[0].Username);

            return View();
        }

        [HttpGet]
        public ActionResult NewLic(string selectstr)
        {
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem()
            {
                Text = "是",
                Value = "1",

            });
            selectList.Add(new SelectListItem()
            {
                Text = "否",
                Value = "0",

            });
            Session["SelectList"] = selectList;
            TempData["SelectList"] = selectList;
            ViewBag.SelectList = selectList;
            CommonServices commonServices = new CommonServices();
            ViewBag.SelectList = commonServices.GetSelectList();
            TB_M_CODE mod = new TB_M_CODE();
            var maxmod = (from t in fM3DB.TB_M_CODE
                       where t.TYPE == selectstr orderby t.CODE descending, t.NAME1 descending, t.SORTORDER descending
                       select t).FirstOrDefault();

            mod.PK_NO = Guid.NewGuid().ToString();
            int max = Int32.Parse(maxmod.CODE.Substring(2, 3)) + 1;
            string maxSting = string.Empty;
            if(max/100 ==0)
            {
                maxSting = '0' +max.ToString();
            }
            else
            {
                maxSting = max.ToString();
            }
            mod.CODE = maxmod.CODE.Substring(0, 2) + maxSting;
            mod.TYPE = selectstr;
            mod.NAME1 = (Int32.Parse(maxmod.NAME1) + 1).ToString();
            mod.SORTORDER = maxmod.SORTORDER + 1;
            mod.CREATED_BY = Session["UserID"].ToString();
            mod.CREATED_DT = DateTime.Now;
            mod.UPDATED_BY = Session["UserID"].ToString();
            mod.UPDATED_DT = DateTime.Now;
            return PartialView("_NewLicdata", mod);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewLic(TB_M_CODE modMaster)
        {
            fM3DB.TB_M_CODE.Add(modMaster);
            fM3DB.SaveChanges();
            TempData["Message"] = "新增成功。";
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem()
            {
                Text = "是",
                Value = "1",

            });
            selectList.Add(new SelectListItem()
            {
                Text = "否",
                Value = "0",

            });
            Session["SelectList"] = selectList;
            TempData["SelectList"] = selectList;
            ViewBag.SelectList = selectList;

            return PartialView("_NewLicdata", modMaster);
        }

        [HttpGet]
        public ActionResult EditLic(string code)
        {
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem()
            {
                Text = "是",
                Value = "1",

            });
            selectList.Add(new SelectListItem()
            {
                Text = "否",
                Value = "0",

            });
            Session["SelectList"] = selectList;
            TempData["SelectList"] = selectList;
            ViewBag.SelectList = selectList;
            //var mod = fM3DB.ModMasters.Where(a => a.ModNo == modNo && a.Country == country && a.CarType == carType).FirstOrDefault();
            var mod = (from t in fM3DB.TB_M_CODE
                           where t.CODE == code
                           select t).FirstOrDefault();
            return PartialView("_editLicdata", mod);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLic(TB_M_CODE modMaster)
        {
            fM3DB.Entry(modMaster).State = System.Data.Entity.EntityState.Modified;
            fM3DB.SaveChanges();
            TempData["Message"] = "更新成功。";
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem()
            {
                Text = "是",
                Value = "1",

            });
            selectList.Add(new SelectListItem()
            {
                Text = "否",
                Value = "0",

            });
            Session["SelectList"] = selectList;
            TempData["SelectList"] = selectList;
            ViewBag.SelectList = selectList;

            return PartialView("_editLicdata", modMaster);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LicMaintain(LicData data)
        {
            CommonServices commonServices = new CommonServices();
            ViewBag.SelectList = commonServices.GetSelectList();
            var empList = (from t in fM3DB.TB_M_CODE
                           where t.TYPE == data.CODE
                           select t).ToList();
            //查無資料確認
            ViewBag.SearchResult = empList;
            if (!Enumerable.Any(ViewBag.SearchResult))
            {
                TempData["Message"] = "查無資料";
            }//查無資料確認
            return View();
        }

        public ActionResult EmpLicLink(ACESFunc fundata, string empid)
        {
            UsersServices userService = new UsersServices();
            List<Users> users = userService.GetUsers(fundata);
            Session.Add("UserID", users[0].Userid);
            Session.Add("UserName", users[0].Username);

            return View();
        }

        [HttpPost]
        public ActionResult EmpLicLink(string empid)
        {
            var empList = (from t in fB3DB.VW_H_EMP_DATA
                           where t.EMP_ID == empid && t.EMP_STATUS == "01"
                           select new { t.EMP_ID, t.EMP_NAME, t.DIV_DEPT_FULL_NAME }).FirstOrDefault();
            if (empList == null)
            {
                Session.Add("empname", "查無資料");
                Session.Add("empdept", "查無資料");
                VW_H_EMP_DATA emp = new VW_H_EMP_DATA();
                emp.EMP_NAME = "查無資料";
                emp.DIV_DEPT_FULL_NAME = "查無資料";
                return Json(new { rows = emp }, JsonRequestBehavior.AllowGet);
            }
            Session.Add("empname", empList.EMP_NAME);
            Session.Add("empdept", empList.DIV_DEPT_FULL_NAME);

            return Json(new { rows = empList }, JsonRequestBehavior.AllowGet);
        }

    }
}