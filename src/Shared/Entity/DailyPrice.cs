using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.ComponentModel.DataAnnotations;
#region prop
/// <summary>
/// Date
/// Open
/// Max
/// Min
/// Close
/// Turnover
/// Amount
/// </summary>
#endregion

namespace Shared.Entity
{
    public class DailyPrice : BaseEntity
    {
        #region ctor
        private static readonly string[] feilds = new string[] {"Date",
"Open",
"Max",
"Min",
"Close",
"Turnover",
"Amount"};
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public decimal Open { get; set; }
        public decimal Max { get; set; }
        public decimal Min { get; set; }
        public decimal Close { get; set; }
        public decimal Turnover { get; set; }
        public decimal Amount { get; set; }
        DailyPrice() { }
        #endregion
        public static DailyPrice NewDailyPrice(string input)
        {
            try
            {
                StringBuilder temp = new StringBuilder();
                var values = input.Split(',');
                temp.Append("{");
                for (int i = 0; i < values.Length; i++)
                {
                    temp.Append(string.Format("\"{0}\":\"{1}\",", feilds[i], values[i].Trim()));
                }
                temp.Append("\"Remark\":\"\"}");
                return JsonConvert.DeserializeObject<DailyPrice>(temp.ToString());
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [StringLength(10)]
        public string Code { get; set; }
    }
}