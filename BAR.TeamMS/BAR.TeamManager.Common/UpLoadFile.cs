using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BAR.TeamManager.Common
{
    public class UpLoadFile
    { 
        /// <summary>
        /// 保存文件方法
        /// </summary>
        /// <param name="files">保存的文件</param>
        /// <param name="filepath">上传文件所需的路径 /file/honorFile</param>
        /// <returns></returns>
        public static string UpFile(HttpFileCollection files,string filepath)
        {
            if (files.Count == 0) return "no";
            string fileName = files[0].FileName;//上传的文件名
            string uploadDate = DateTime.Now.ToString("yyyyMMddHHmmss"); 
            string fileNewName = uploadDate + fileName;
            string virtualPath = System.Web.HttpContext.Current.Server.MapPath(filepath);
            string filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(filepath), fileNewName);//保存文件的路径
            if (!System.IO.File.Exists(filePath))
            {
                Directory.CreateDirectory(virtualPath);
                files[0].SaveAs(filePath);
            }
            return filePath;
        }
        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static string GetFileSize(long bytes)
        {
            long kblength = 1024;
            long mbLength = 1024 * 1024;
            if (bytes < kblength)
                return bytes.ToString() + "B";
            if (bytes < mbLength)
                return decimal.Round(decimal.Divide(bytes, kblength), 2).ToString() + "KB";
            else
                return decimal.Round(decimal.Divide(bytes, mbLength), 2).ToString() + "MB";
        }
    }
}
