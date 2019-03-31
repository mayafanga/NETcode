using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAR.TeamManager.Model;
using BAR.TeamManager.BLL;
using BAR.TeamManager.IBLL;
using Lucene.Net.Search;
using Lucene.Net.Analysis.PanGu;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BAR.TeamManager.Common;

namespace BAR.TeamManager.WebApp.Controllers 
{
    public class SearchController : Controller
    {
        IteamService TeamService { get; set; }
        IhonorService HonorService { get; set; }
        IhonorparticipantmemberService HonorparticipantmemberService { get; set; }
        IpersonalinformationService PersonalinformationService { get; set; }

        // GET: Search
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 完成搜索
        /// 表单中有多个submit，单击每个submit都会提交表单，
        /// 但是只会将用户所单机的表单元素的value值提交到服务端。
        /// </summary>
        /// <returns></returns>
        public ActionResult SearchContent()
        {
            #region
            string jsonTxt = null;
            string btn = Request["btn"];
            string msg = Request["search"];
            if (msg != null)
            {
                //用拿到的名字从数据库找到相应的内容，返回到前台
                List<Models.ViewModelContent> list = Models.SearchContent.ShowSearchContent(Request, msg);
                //前台还要再传一个参数  以此参数判断这个是活动、荣耀还是团队发送过来的搜索请求

                if(btn.ToUpper()== "HONORBTN") //查询荣耀内容
                {
                    jsonTxt = this.GetHonorSearchInfo(list);
                }
                else if(btn.ToUpper()== "TEAMBTN") //查询团队内容
                {

                }
                else if(btn.ToUpper()== "ACTIVEBTN")//查询活动内容
                {

                }
            }
            else  //输入的内容是空的
            {
                jsonTxt = "空空如也";
            }

            return Content(jsonTxt);
            #endregion
            #region
            //if (!String.IsNullOrEmpty(Request["btnSearch"]))
            //{

            //}
            //else
            //{
            //    CreateSerachContent();
            //}

            //return Content("ok");
            #endregion
        }
        #region 给lucenedir更新数据库内容
        //private void CreateSerachContent()
        //{
        //    string indexPath = @"C:\lucenedir";//注意和磁盘上文件夹的大小写一致，否则会报错。将创建的分词内容放在该目录下。//将路径写到配置文件中。
        //    FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NativeFSLockFactory());//指定索引文件(打开索引目录) FS指的是就是FileSystem
        //    bool isUpdate = IndexReader.IndexExists(directory);//IndexReader:对索引进行读取的类。该语句的作用：判断索引库文件夹是否存在以及索引特征文件是否存在。
        //    if (isUpdate)
        //    {
        //        //同时只能有一段代码对索引库进行写操作。当使用IndexWriter打开directory时会自动对索引库文件上锁。
        //       //如果索引目录被锁定（比如索引过程中程序异常退出），则首先解锁（提示一下：如果我现在正在写着已经加锁了，但是还没有写完，这时候又来一个请求，那么不就解锁了吗？这个问题后面会解决）
        //        if (IndexWriter.IsLocked(directory))
        //        {
        //            IndexWriter.Unlock(directory);
        //        }
        //    }
        //    IndexWriter writer = new IndexWriter(directory, new PanGuAnalyzer(), !isUpdate, Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED);//向索引库中写索引。这时在这里加锁。
        //    List<honor> list = HonorService.LoadEntities(h => true).ToList();
        //    foreach (var honorModel in list)
        //    {
        //        Document document = new Document();//表示一篇文档。
        //        //Field.Store.YES:表示是否存储原值。只有当Field.Store.YES在后面才能用doc.Get("number")取出值来.Field.Index. NOT_ANALYZED:不进行分词保存
        //        document.Add(new Field("Id", honorModel.ID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));

        //        //Field.Index. ANALYZED:进行分词保存:也就是要进行全文的字段要设置分词 保存（因为要进行模糊查询）

        //        //Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS:不仅保存分词还保存分词的距离。
        //        document.Add(new Field("Title", honorModel.vcHonorName, Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
        //        document.Add(new Field("Content", honorModel.vcGuideTeacher, Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
        //        writer.AddDocument(document);

        //    }
        //    writer.Close();//会自动解锁。
        //    directory.Close();//不要忘了C
        //}
        #endregion




        #region
        //定义三个方法，作用写 根据搜索内容 获取团队、活动和荣耀表里面的信息
        /// <summary>
        /// 获取搜索的荣耀信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string GetHonorSearchInfo(List<Models.ViewModelContent> list)
        {
            List<string> honorName = new List<string>();
            foreach(var honor in list)
            {
                honorName.Add(honor.Title);
            }
            var honorList = HonorService.GetHonorListByName(honorName).ToList();
            return HonorInfoController.GetHonorInfo(honorList, HonorService, HonorparticipantmemberService, TeamService, PersonalinformationService);
        }
        #endregion

    }

}