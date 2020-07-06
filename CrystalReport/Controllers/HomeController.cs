using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using CrystalReport.Models;

namespace CrystalReport.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowProductList()
        {
            CrystalReport.Models.AdventureWorks2012Entities db = new CrystalReport.Models.AdventureWorks2012Entities();

            //CrMVCApp.Models.Customer c;
            var c = (from b in db.Products join pr in db.ProductReviews on b.ProductID equals pr.ProductID select new {b.Name, b.Color, b.Size, b.ListPrice, b.SellStartDate, pr.ReviewerName, pr.Rating, pr.Comments}).ToList();
            //var c = (from b in db.Products select new { b.Name, b.Color, b.Size, b.ListPrice, b.SellStartDate }).ToList();

            ProductList rpt = new ProductList();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }

        public ActionResult ShowProductReview()
        {
            CrystalReport.Models.AdventureWorks2012Entities2 db = new CrystalReport.Models.AdventureWorks2012Entities2();

            //CrMVCApp.Models.Customer c;
            //var c = (from b in db.ProductReviewViews join pr in db.ProductReviews on b.ProductID equals pr.ProductID select new { b.Name, b.Color, b.Size, b.ListPrice, b.SellStartDate, pr.ReviewerName, pr.Rating, pr.Comments }).ToList();
            //var c = (from b in db.Products select new { b.Name, b.Color, b.Size, b.ListPrice, b.SellStartDate }).ToList();
            var c = (from b in db.ProductReviewViews select new { b.Name, b.Color, b.Size, b.ReviewerName, b.Rating, b.Comments }).ToList();

            ProductReview rpt = new ProductReview();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
    }
}