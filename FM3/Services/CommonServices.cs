using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FM3.Models;
using FM3.Services;

namespace FM3.Services
{
    public class CommonServices
    {
        private FM3DBEntities FM3db = new FM3DBEntities();

        public List<TB_M_CODE> GetLicCategory()
        {
            var licList = (from t in FM3db.TB_M_CODE
                           where t.TYPE == "LT01"
                           select t).ToList();
            return licList;
        }

        public List<SelectListItem> GetSelectList()
        {
            CommonServices common = new CommonServices();
            List<TB_M_CODE> licCategories = common.GetLicCategory();

            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "000", Text = "請選擇" });
            foreach (TB_M_CODE liccategory in licCategories)
            {
                selectList.Add(new SelectListItem()
                {
                    Text = liccategory.NAME2,
                    Value = liccategory.CODE,

                });
            };
            return selectList;
        }
    }
}