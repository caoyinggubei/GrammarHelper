﻿using GrammarHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace text.Extenions
{
    public partial class TypeTryParse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var int1 = "2".TryToInt();//转换为int失败返回0
            var int2 = "2x".TryToInt();
            var int3 = "2".TryToInt(1);//转换为int失败返回1
            var int4 = "2x".TryToInt(1);


            var d1 = "2".TryToDouble();
            var d2 = "2x".TryToDouble();
            var d3 = "2".TryToDouble(1);
            var d4 = "2x".TryToDouble(1);

            string a = null;
            var s1 = a.TryToString();
            var s3 = a.TryToString("1");


            var d11 = "2".TryToDecimal();
            var d22 = "2x".TryToDecimal();
            var d33 = "2".TryToDecimal(1);
            var d44 = "2x".TryToDecimal(1);


            var de1 = "2013-1-1".TryToDate();
            var de2 = "x2013-1-1".TryToDate();
            var de3 = "x2013-1-1".TryToDate(DateTime.Now);


            //json和model转换
            var json = new { id = 1 }.ModelToJson();
            var model = "{id:1}".JsonToModel<user>();


            //list和dataTable转换
            user m = new user();
            m.id = 111;
            m.name = "aa";
            var dt = new List<user>() { m }.TryToDataTable();
            var list = dt.TryToList<user>();
            //list 转hashtable
            var clist = dt.DataTableToArrayList().TryIListToList();
            var ilist = dt.DataTableToArrayList();
            foreach (var item in ilist)
            {
                foreach (DictionaryEntry obj in item)
                {
                    var k = obj.Key;
                    var v = obj.Value;
                }
            }
        }
        public class user
        {
            public int id { get; set; }
            public string name { get; set; }

        }
    }
}