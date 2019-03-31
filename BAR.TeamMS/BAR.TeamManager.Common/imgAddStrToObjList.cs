/**
 * 此文件不用，留作以后或有他用
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAR.TeamManager.Common
{
    class imgAddStrToObjList
    {
        /// <summary>
        /// 将轮播图字符串转换为对象列表，用以json序列化
        /// </summary>
        /// <param name="addressStr">待转换字符串 格式：[{"image":"../img/index/slider_2.jpg"},{"image":"../img/index/slider_3.jpg"}]</param>
        /// <returns></returns>
        public static List<object> imgAddressStringToObjectList(string addressStr)
        {
            List<object> list = new List<object>();
            try
            {
                string TeamSliderAddStr = addressStr;
                TeamSliderAddStr = TeamSliderAddStr.Substring(1, TeamSliderAddStr.Length - 2);
                string[] TeamSliderAddStrArry = TeamSliderAddStr.Split(',');
                foreach (string item in TeamSliderAddStrArry)
                {
                    string value = item.Substring(10, item.Length - 12);
                    var img = new { image = value };
                    list.Add(img);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
