using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAR.TeamManager.Common
{
    public class MD5Secret
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="strPwd"></param>
        /// <returns></returns>
        public static string GetMD5Str(string strPwd)
        {
            byte[] b = System.Text.Encoding.Default.GetBytes(strPwd);
            b = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(b);
            string ret = " ";
            for (int i = 0; i < b.Length; i++)
            {
                ret += b[i].ToString("x").PadLeft(2, '0');
            }
            return ret;
        }
    }
}
