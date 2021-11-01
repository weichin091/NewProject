using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FM3.Models
{
    public class LicLinkViewModel
    {
            public string EMP_ID { get; set; }
            public string EMP_NAME { get; set; }
            //        public string EMP_ENGNAME { get; set; }
            public string EMP_STATUS { get; set; }
            public string EMP_STATUS_DESC { get; set; }
            public string DEPT_NO { get; set; }
            public string DEPT_NAME { get; set; }
            //        public string DEPT_FULL_NAME { get; set; }
            public string DIV_DEPT_FULL_NAME { get; set; }
            public string DEPT_NO_20 { get; set; }
            public string DEPT_NAME_20 { get; set; }
            public string DEPT_NO_30 { get; set; }
            public string DEPT_NAME_30 { get; set; }
            public string LEVEL_CD { get; set; }
            public string PJOB_CD { get; set; }
            public string PJOB_DESC { get; set; }
            public string WORK_SHIFT_CD { get; set; }
            public string WORK_SHIFT_DESC { get; set; }
            public string WORK_CD { get; set; }
            //Licdata
            public string PK_NO { get; set; }
            public string LIC_CD { get; set; }
            public string LIC_NO { get; set; }
            //Lic name 從 tb_m_code取得
            public string LIC_NAME { get; set; }
            public string LIC_DATE { get; set; }
            public string RE_LIC_DATE { get; set; }
            public string IS_ASSIGN { get; set; }
            public string IS_TEACHER { get; set; }
            public string IS_GRD { get; set; }
            public string GRD_NO { get; set; }
            public string CREATED_BY { get; set; }
            public Nullable<System.DateTime> CREATED_DT { get; set; }
            public string UPDATED_BY { get; set; }
            public Nullable<System.DateTime> UPDATED_DT { get; set; }
    }
}