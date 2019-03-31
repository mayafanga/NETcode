using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAR.TeamManager.Common
{
    public class EditSlider
    {
        /// <summary>
        /// 把base64编码通过特殊字符分割开，编译成地址，然后拼成json格式返回去
        /// </summary>
        /// <param name="slider"></param>
        ///<param name="sliderName">保存图片的地址</param>
        /// <returns></returns>
        public static string EncapCode(string slider, string path)
        {
            StringBuilder jsonTxt = new StringBuilder();
            string result = "";
            if (slider != null)
            {
                string[] webSliderBase = slider.Split(new char[] { '#' });//根据特殊字符把从前台获取的Base64编码分割开
                List<string> webAddressList = new List<string>();
                foreach (string str in webSliderBase)
                {
                    if (str.Contains("data:image/jpeg;base64,"))
                    {
                        string newPath = EditPhotos.Base64StringToImage(str,path);
                        string savePath = ".." + path + "/" + Path.GetFileName(newPath);
                        webAddressList.Add(savePath); //把Base64编码转化为地址
                    }
                    else
                    {
                        webAddressList.Add(str);
                    }
                }
                jsonTxt.Append("[");
                foreach (var str in webAddressList)
                {
                    jsonTxt.Append("{\"image\":" + "\"" + str + "\"},");
                }
                jsonTxt.Remove(jsonTxt.ToString().Length - 1, 1);
                jsonTxt.Append("]");
            }
            else
            {
                jsonTxt.Append("text");
            }
            return jsonTxt.ToString();
        }

        #region
        /// <summary>
        /// 判断一串字符是不是base64编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static bool IsBase64(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return false;
            }
            else
            {
                if (str.Length % 4 != 0)
                {
                    return false;
                }

                char[] strChars = str.ToCharArray();
                foreach (char c in strChars)
                {
                    if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9')
                       || c == '+' || c == '/' || c == '=')
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        #endregion
    }
}
