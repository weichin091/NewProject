using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FM3.Models
{
    public class Commons
    {
    }

    public class LicCategory
    {
        public string CODE { get; set; }
        public string NAME2 { get; set; }
        public string IS_USED { get; set; }
    }

    public class LicData
    {
        public string CODE { get; set; }
        public string NAME1 { get; set; }
        public string NAME2 { get; set; }
        public string NAME3 { get; set; }
        public string ANNOTATION { get; set; }
        public decimal SORTORDER { get; set; }
        public string IS_USED { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DT { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime UPDATED_DT { get; set; }

    }

}