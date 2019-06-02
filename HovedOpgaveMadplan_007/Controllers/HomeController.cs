using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HovedOpgaveMadplan_007.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetMadPlanEntities()
        {
            using (DB_MadplanEntities1 dbm = new DB_MadplanEntities1())
            {
                var events = dbm.Table.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        public JsonResult SaveMad(Table t)
        {
            var status = false;
            using (DB_MadplanEntities1 dbm = new DB_MadplanEntities1())
            {
                if (t.Id > 0)
                {
                    var x = dbm.Table.Where(a => a.Id == t.Id).FirstOrDefault();

                    if (x != null)
                    {
                        x.Titel = t.Titel;
                        x.Start = t.Start;
                        x.Info = t.Info;
                    }
                }
                else
                {
                    dbm.Table.Add(t);
                }
                dbm.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteMad(int TableId)
        {
            var status = false;
            using (DB_MadplanEntities1 dbm = new DB_MadplanEntities1())
            {
                var var = dbm.Table.Where(a => a.Id == TableId).FirstOrDefault();
                if (var != null)
                {
                    dbm.Table.Remove(var);
                    dbm.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}