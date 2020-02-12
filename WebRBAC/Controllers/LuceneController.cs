using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lucene;
using Lucene.Net;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using WebRBAC.Models;
using PanGu.HighLight;

namespace WebRBAC.Controllers
{
    public class LuceneController : Controller
    {
        // GET: Lucene
        public ActionResult Index(string keyWords)
        {
            return View();
        }

        [HttpGet]
        public ActionResult SearchPage(string keyWords)
        {
            return Json(Search(keyWords), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(News news)
        {
            CreateIndex(news);
            return RedirectToAction("Create");
        }

        public void CreateIndex(News news)
        {
            Directory dir = FSDirectory.Open(new System.IO.DirectoryInfo(Server.MapPath("/Index")),new NativeFSLockFactory());
            bool isUpdate = IndexReader.IndexExists(dir);
            if(isUpdate)
            {
                if(IndexWriter.IsLocked(dir))
                {
                    IndexWriter.Unlock(dir);
                }
            }

            using (IndexWriter iw = new IndexWriter(dir,new PanGuAnalyzer(),!isUpdate,IndexWriter.MaxFieldLength.LIMITED))
            {
                for(int i = 1; i < 100; i++)
                {
                    Random random = new Random();
                    DateTime dt = news.AddTime.AddHours(random.Next(100, 1000));
                    Document doc = new Document();
                    doc.Add(new NumericField("NewsId", Field.Store.YES, true).SetIntValue(i));
                    doc.Add(new Field("Title", news.Title, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_OFFSETS));
                    doc.Add(new Field("Content", news.Content, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_OFFSETS));
                    doc.Add(new Field("AddTime", dt.ToString("yyyy-MM-dd HH:mm:ss"), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_OFFSETS));
                    doc.Add(new NumericField("OrderId", Field.Store.YES, true).SetLongValue(Convert.ToInt64(DateTools.DateToString(dt, DateTools.Resolution.SECOND))));
                    iw.AddDocument(doc);
                    iw.Optimize();
                }
            }
        }

        public List<News> Search(string Keywords)
        {
            List<News> newsList = new List<News>();
            Directory dir = FSDirectory.Open(new System.IO.DirectoryInfo(Server.MapPath("/Index")), new SimpleFSLockFactory());
            IndexSearcher search = new IndexSearcher(dir);

            PhraseQuery query = new PhraseQuery();
            query.Add(new Term("Content", Keywords));
            //query.Slop = 8;

            NumericRangeFilter<int> range = NumericRangeFilter.NewIntRange("NewsId", 1, 10, true, true);

            Sort sort = new Sort(new SortField("OrderId", SortField.LONG, true));


            TopFieldDocs fileds = search.Search(query, range, 1000, sort);

            ScoreDoc[] docs = fileds.ScoreDocs;
            for(int i = 0; i < docs.Length; i++)
            {
                News news = new News();
                int docid = docs[i].Doc;
                Document doc = search.Doc(docid);
                news.NewsId = Convert.ToInt32(doc.Get("NewsId"));
                news.Title = doc.Get("Title");

                SimpleHTMLFormatter html = new SimpleHTMLFormatter("<span style=\"color:red\" >", "</span>");
                Highlighter high = new Highlighter(html, new PanGu.Segment());
                high.FragmentSize = 120;

                news.Content = high.GetBestFragment(Keywords, doc.Get("Content"));

                news.AddTime = Convert.ToDateTime(doc.Get("AddTime"));

                news.OrderId = Convert.ToInt64(doc.Get("OrderId"));

                newsList.Add(news);
            }
            return newsList;
        }
    }
}