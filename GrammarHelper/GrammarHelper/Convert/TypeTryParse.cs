﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace GrammarHelper
{

    /// <summary>
    /// **描述：类型转换
    /// **作者：lilun
    /// </summary>
    public static class TypeTryParse
    {
        #region 强转成bool 失败返回0
        /// <summary>
        /// 强制转换成bool，转换失败返回false
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static bool TryToBoolean(this object thisValue)
        {
            bool result = false;
            if (thisValue != null && Boolean.TryParse(thisValue.ToString(), out result))
            {
                return result;
            }

            return result;
        }
        #endregion

        #region 强转成int 失败返回0
        /// <summary>
        /// 强制转换成int，转换失败返回0
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static int TryToInt(this object thisValue)
        {
            int result = 0;
            if (thisValue != null && int.TryParse(thisValue.ToString(), out result))
            {
                return result;
            }

            return result;
        }
        #endregion


        #region 强转成int 失败返回错误的值
        /// <summary>
        /// 重载
        /// 强制转换成int，失败返回错误的值　　
        /// </summary>
        /// <param name="thisValue">值</param>
        /// <param name="errorValue">自定义错误返回的值</param>
        /// <returns></returns>
        public static int TryToInt(this object thisValue, int errorValue)
        {
            int result = 0;
            if (thisValue != null && int.TryParse(thisValue.ToString(), out result))
            {
                return result;
            }

            return errorValue;
        }
        #endregion

        #region 强转成double 失败返回0
        /// <summary>
        /// 强制转换成double，转换失败返回0
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static double TryToDouble(this object thisValue)
        {
            double result = 0;
            if (thisValue != null && double.TryParse(thisValue.ToString(), out result))
            {
                return result;
            }

            return result;
        }
        #endregion

        #region 强转成double 失败返回错误的值
        /// <summary>
        /// 强制转换成double，转换失败返回
        /// </summary>
        /// <param name="thisValue">值</param>
        /// <param name="errorValue">自定义错误返回的值</param>
        /// <returns></returns>
        public static double TryToDouble(this object thisValue, double errorValue)
        {
            double result = 0;
            if (thisValue != null && double.TryParse(thisValue.ToString(), out result))
            {
                return result;
            }
            return errorValue;
        }
        #endregion


        #region 强转成string 失败返回""
        /// <summary>
        /// 强制转换成string，转换失败返回0
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static string TryToString(this object thisValue)
        {
            if (thisValue != null) return thisValue.ToString().Trim();
            return "";
        }
        #endregion


        #region 强转成string 失败指定错误的字符
        /// <summary>
        /// 强制转换成string，转换失败返回0
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue">自定义错误返回的值</param>
        /// <returns></returns>
        public static string TryToString(this object thisValue, string errorValue)
        {
            if (thisValue != null)
            {
                return thisValue.ToString().Trim();
            }
            return errorValue;
        }
        #endregion

        #region 强转成decimal 失败返回0
        /// <summary>
        /// 强制转换成decimal，转换失败返回0
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static decimal TryToDecimal(this object thisValue)
        {
            decimal result = 0;
            if (thisValue != null && decimal.TryParse(thisValue.ToString(), out result))
            {
                return result;
            }
            return 0;
        }
        #endregion

        #region 强转成decimal 失败指定错误的字符
        /// <summary>
        /// 强制转换成decimal，失败返回错误的值
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue">自定义错误返回的值</param>
        /// <returns></returns>
        public static decimal TryToDecimal(this object thisValue, decimal errorValue)
        {
            decimal result = 0;
            if (thisValue != null && decimal.TryParse(thisValue.ToString(), out result))
            {
                return result;
            }
            return errorValue;
        }
        #endregion

        #region 强转成DateTime 如果失败返回 DateTime.MinValue
        /// <summary>
        /// 强转成DateTime 如果失败返回 DateTime.MinValue
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static DateTime TryToDate(this object thisValue)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }
        #endregion

        #region 强转成DateTime 如果失败返回自定义错误值
        /// <summary>
        /// 强转成DateTime 如果失败返回 errorValue
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static DateTime TryToDate(this object thisValue, DateTime errorValue)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }
        #endregion

   
        #region  DataTable List 转换
        /// <summary>
        /// 将集合类转换成DataTable
        /// </summary>
        /// <param name="list">集合</param>
        /// <returns></returns>
        public static DataTable TryToDataTable<T>(this List<T> list)
        {
            //创建一个table循环list集合
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                //通过反射类获取到当前类型的所有属性循环取到属性的名称存到table中
                PropertyInfo[] propertys = typeof(T).GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name);
                }
                //循环list集合中的所有值匹配到类型对应属性的位置并添加到table中
                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        if (obj != null && obj != DBNull.Value)
                            tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

        /// <summary>将datatable转为list
        /// 将datatable转为list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> TryToList<T>(this DataTable table) where T : class, new()
        {
            List<T> list = new List<T>();
            try
            {
                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch(Exception ex)
                        {
                            continue;
                        }
                    }
                    list.Add(obj);
                }
                return list;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        /// <summary>  
        /// table指定行转对象  
        /// </summary>  
        /// <typeparam name="T">实体</typeparam>  
        /// <param name="dt">传入的表格</param>  
        /// <param name="rowindex">table行索引，默认为第一行</param>  
        /// <returns>返回实体对象</returns>  
        public static T TableToEntity<T>(DataTable dt, int rowindex = 0, bool isStoreDB = true)
        {
            Type type = typeof(T);
            T entity = Activator.CreateInstance<T>(); //创建对象实例  
            if (dt == null)
            {
                return entity;
            }
            //if (dt != null)  
            //{  
            DataRow row = dt.Rows[rowindex]; //要查询的行索引  
            PropertyInfo[] pArray = type.GetProperties();
            foreach (PropertyInfo p in pArray)
            {
                if (!dt.Columns.Contains(p.Name) || row[p.Name] == null || row[p.Name] == DBNull.Value)
                {
                    continue;
                }

                if (isStoreDB && p.PropertyType == typeof(DateTime) && Convert.ToDateTime(row[p.Name]) < Convert.ToDateTime("1753-01-02"))
                {
                    continue;
                }
                try
                {
                    var obj = Convert.ChangeType(row[p.Name], p.PropertyType);//类型强转，将table字段类型转为对象字段类型  
                    p.SetValue(entity, obj, null);
                }
                catch (Exception)
                {
                    // throw;  
                }
                // p.SetValue(entity, row[p.Name], null);                     
            }
            //  }  
            return entity;
        }

        #region IO 转换
        /// <summary>
        /// 将流转成btye
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] TryToBytes(this Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// <summary>
        /// 将btye转成流
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Stream TryToStream(this byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
        #endregion

        #region 转换IList<T>为List<T> 
        //// <summary>
        ///  将IList接口泛型转为List泛型类型
        /// </summary>
        /// <typeparam name="T">指定的集合中泛型的类型</typeparam>
        /// <param name="gbList">需要转换的IList</param>
        /// <returns></returns>
        public static List<T> TryIListToList<T>(this IList<T> gbList) where T : class   //静态方法，泛型转换，
        {
            if (gbList != null && gbList.Count >= 1)
            {
                List<T> list = new List<T>();
                for (int i = 0; i < gbList.Count; i++)  //将IList中的元素复制到List中
                {
                    T temp = gbList[i] as T;
                    if (temp != null)
                        list.Add(temp);
                }
                return list;
            }
            return null;
        }
        #endregion

        #region 排序表的视图
        /// <summary>
        /// 排序表的视图
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sorts"></param>
        /// <returns></returns>
        public static DataTable SortedTable(DataTable dt, params string[] sorts)
        {
            if (dt.Rows.Count > 0)
            {
                string tmp = "";
                for (int i = 0; i < sorts.Length; i++)
                {
                    tmp += sorts[i] + ",";
                }
                dt.DefaultView.Sort = tmp.TrimEnd(',');
            }
            return dt;
        }
        #endregion

        #region  转全角的函数
       
        ///转全角的函数(SBC case)
        ///任意字符串
        ///全角字符串
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        public static string ToSBC(string input)
        {
            // 半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }
        #endregion

        #region 转半角的函数
        
        /// 转半角的函数(DBC case)
        ///任意字符串
        ///半角字符串
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        #endregion

    }

}
