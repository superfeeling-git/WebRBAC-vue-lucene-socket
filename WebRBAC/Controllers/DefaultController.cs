using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lucene;
using Lucene.Net;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using PanGu.HighLight;
using io = System.IO;
using WebRBAC.Models;

namespace WebRBAC.Controllers
{
    public class LuceneHelper
    {
        public void CreateIndex(News news)
        {
            Directory dir = FSDirectory.Open(new System.IO.DirectoryInfo(HttpContext.Current.Server.MapPath("/Indexs/")), new NativeFSLockFactory());
            var isUpdate = IndexReader.IndexExists(dir);
            if(isUpdate)
            {
                if(IndexWriter.IsLocked(dir))
                {
                    IndexWriter.Unlock(dir);
                }
            }

            using (IndexWriter iw = new IndexWriter(dir,new PanGuAnalyzer(),!isUpdate, IndexWriter.MaxFieldLength.LIMITED))
            {
                Random random = new Random();
                for (int i = 0; i < 100; i++)
                {                    
                    DateTime dt = news.AddTime.AddHours(random.Next(100, 1000));
                    Document doc = new Document();
                    doc.Add(new NumericField("NewsId", Field.Store.YES, true).SetIntValue(i));
                    doc.Add(new Field("Title", news.Title, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_OFFSETS));
                    doc.Add(new Field("Content", news.Content, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_OFFSETS));
                    doc.Add(new Field("Date", dt.ToString("yyyy-MM-dd HH:mm:ss"), Field.Store.YES, Field.Index.NO));
                    doc.Add(new NumericField("OrderId", Field.Store.YES, true).SetLongValue(Convert.ToInt64(DateTools.DateToString(dt, DateTools.Resolution.SECOND))));
                    iw.AddDocument(doc);
                }
                iw.Optimize();
            }
        }

        public List<News> Search(string keywords)
        {
            Directory dir = FSDirectory.Open(new io.DirectoryInfo(HttpContext.Current.Server.MapPath("/Indexs/")), new SimpleFSLockFactory());
            IndexReader reader = IndexReader.Open(dir, true);
            IndexSearcher search = new IndexSearcher(reader);

            MultiFieldQueryParser multifield = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30, new string[]{"Title","Content" }, new PanGuAnalyzer());
            multifield.PhraseSlop = 3;
            multifield.DefaultOperator = QueryParser.Operator.AND;
            Query muqu = multifield.Parse(keywords);

            //MultiPhraseQuery multi = new MultiPhraseQuery();
            //multi.Add(new Term[] {new Term("Content","中国"), new Term("Content", "智慧"), new Term("Title", "中国"), new Term("Title", "智慧") });

            //PhraseQuery query = new PhraseQuery();
            //query.Add(new Term("Content", keywords));

            NumericRangeFilter<int> filter = NumericRangeFilter.NewIntRange("NewsId", 1, 10, true, true);

            Sort sort = new Sort();
            sort.SetSort(new SortField("OrderId", SortField.LONG, true));

            TopFieldDocs fields = search.Search(muqu, filter, 1000, sort);

            ScoreDoc[] docs = fields.ScoreDocs;

            List<News> newslist = new List<News>();
            for(int i = 0; i < docs.Length; i++)
            {
                News news = new News();
                Document doc = search.Doc(docs[i].Doc);
                news.NewsId = Convert.ToInt32(doc.Get("NewsId"));
                news.Title = doc.Get("Title");

                SimpleHTMLFormatter formatter = new SimpleHTMLFormatter("<span style=\"color:red\">", "</span>");
                Highlighter high = new Highlighter(formatter, new PanGu.Segment());
                high.FragmentSize = 120;
                news.Content = high.GetBestFragment(keywords, doc.Get("Content"));
                


                news.AddTime = Convert.ToDateTime(doc.Get("Date"));
                news.OrderId = Convert.ToInt64(doc.Get("OrderId"));

                newslist.Add(news);
            }

            return newslist;
        }
    }
}