using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace MVCEventCalendar.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult Index()
        {



            return View();



        }

        [HttpPost]
        public JsonResult GetSearchValue(string search)
        {
            using(MyDatabaseEntities dc= new MyDatabaseEntities())
            {
                var allsearch = (from c in dc.Clients
                                 where c.FirstLastName.StartsWith(search)
                                 select new { c.FirstLastName, c.ClientsID });
                return Json(allsearch, JsonRequestBehavior.AllowGet);
            }
        }
        //public JsonResult GetSearchValue(string search)
        //    {
        //    using (MyDatabaseEntities dc = new MyDatabaseEntities())
        //    {
        //        //var allsearch = dc.Clients.Where(p => p.FirstLastName.Contains(search)).Select(p => p.FirstLastName).ToList();
        //        //return Json(allsearch, JsonRequestBehavior.AllowGet);
        //        var allsearch = (from x in dc.Clients
        //                         where x.FirstLastName.StartsWith(search)
        //                         select new { x.FirstLastName });
        //        return Json(allsearch, JsonRequestBehavior.AllowGet);

        //  var allsearch = dc.Clients.Where(p => p.FirstLastName.Contains(search)).Select(p => p.FirstLastName).ToList();
        //return Json(allsearch, JsonRequestBehavior.AllowGet);

        //  return Json(dc.Clients.Where(c => c.FirstLastName.StartsWith(search)).Select(a => new { label = a.FirstLastName, id = a.ClientsID }), JsonRequestBehavior.AllowGet);
        //  List<Client> clients = dc.Clients.Where(x => x.FirstLastName.Contains(search)).Select(x => new Client
        //{
        //    ClientsID = x.ClientsID,
        //    FirstLastName = x.FirstLastName
        //}).ToList();
        // return new JsonResult { Data = clients, JsonRequestBehavior = JsonRequestBehavior.AllowGet };



        //List<Client> clientsd;
        //if (string.IsNullOrEmpty(search))
        //{
        //    clientsd = dc.Clients.ToList();

        //}
        //else
        //{
        //    clientsd = dc.Clients.Where(x => x.FirstLastName.StartsWith(search)).ToList();

        //}
        //    return new JsonResult { Data = clientsd, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        //    }

        //}
        public ActionResult StatisticsDatePerDayTotal(string start, string worker)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {

                DateTime SelectedDate = Convert.ToDateTime(start);

                var statsmodel = new Event();
                statsmodel.Price = dc.Events.Where(o => o.Start == SelectedDate ).Sum(o => o.Price);
                return View(statsmodel);
            }
        }

        public ActionResult getstart()
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var events = dc.Events.Select(o => o.Start);



                return View(events);
            }

        }
        public JsonResult ifdate() {
           
                using (MyDatabaseEntities dc = new MyDatabaseEntities())
                {
                    var events = dc.Events.Select(o => o.Start).ToList();



                    return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }

            


        }
        public ActionResult StatisticsDatePerDayWorker(string start, string worker)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                
                DateTime SelectedDate = Convert.ToDateTime(start);
                DateTime SelectedDate1 = Convert.ToDateTime(start);

                var statsmodel = new Event();
                statsmodel.Price = dc.Events.Where(o => o.Start ==  SelectedDate && o.Worker == worker).Sum(o => o.Price);
                return View(statsmodel);
            }
        }
        public ActionResult StatisticsDatePerMonthWorker(string start, string end,string worker)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {

                DateTime SelectedDate = Convert.ToDateTime(start);
                DateTime SelectedDate1 = Convert.ToDateTime(end);

                var statsmodel = new Event();
                statsmodel.Price = dc.Events.Where(o => o.Start >= SelectedDate && o.Start <= SelectedDate1 && o.Worker == worker ).Sum(o => o.Price);
                return View(statsmodel);
            }
        }
        public ActionResult StatisticsDatePerMonthTotal(string start, string end)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {

                DateTime SelectedDate = Convert.ToDateTime(start);
                DateTime SelectedDate1 = Convert.ToDateTime(end);

                var statsmodel = new Event();
                statsmodel.Price = dc.Events.Where(o => o.Start >= SelectedDate && o.Start <= SelectedDate1 ).Sum(o => o.Price);
                return View(statsmodel);
            }
        }
        public ActionResult Statistics(string worker)
        {

            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var statsmodel = new Event();
                statsmodel.Price = dc.Events.Where(o => o.Worker == worker).Sum(o => o.Price);
                return View(statsmodel);
            }
            //using (MyDatabaseEntities dc = new MyDatabaseEntities())
            //{
            //    var statsmodel = new Event();
            //    statsmodel.Price = dc.Events.Where(o => o.Worker == "Stojanovic Borka").Sum(o => o.Price);
            //    return View(statsmodel);
            //  }  
        }

        public async  Task<ActionResult> Eanings(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                string query = "select Price from Events where Worker =@p2";
                Event client = await dc.Events.SqlQuery(query, id).SingleOrDefaultAsync();
                if (client == null)
                {
                    return HttpNotFound();

                }
                return View(client);

            }

        }
         public ActionResult getdate()
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var valdat = dc.Events.Include(y => y.Start);
                return View(valdat.ToList());

            }
        }

        public JsonResult GetEvents()
        {

            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var events = dc.Events.ToList();



                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }


        public JsonResult GetClients()
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var clients = dc.Clients.ToList();
                return new JsonResult { Data = clients, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }

        }

        public ActionResult GetClientsList()
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var item = dc.Clients.ToList();

                return View(item);

            }
        }

        public ActionResult GetEventlist()
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var item = dc.Events.ToList();

                return View(item);

            }
        }
        //public ActionResult Create()
        //{
        //    return View();
        //}
        
         
            public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        
        public ActionResult Create(Client item)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
                try
                {
                    if (ModelState.IsValid)
                    {
                        dc.Clients.Add(item);
                        dc.SaveChanges();
                        return View(item);


                    }
                    else
                    {
                        return RedirectToAction("Index");


                    }
                }
                catch 
                {

                    return RedirectToAction("Index");

                }
            


        }

        
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                string query = "select * from Clients where ClientsID =@p0";
                Client client = await dc.Clients.SqlQuery(query, id).SingleOrDefaultAsync();
                if (client == null)
                {
                    return HttpNotFound();

                }
                return View(client);

            }
        }

        public ActionResult Edit (int? num)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                Client client = dc.Clients.Find(num);
                if (client == null)

                {
                    return HttpNotFound();


                }
                return View(client);
            }
        }

       // public ActionResult Details(int id)
       // {
        //    using(MyDatabaseEntities dc = new MyDatabaseEntities())
        //    {
             //   return View(dc.Clients.Where(x => x.ClientsID == id).FirstOrDefault());
        //    }
     //   }
        //public ActionResult Details(int? ClientsID)
        //{
        //    using (MyDatabaseEntities dc = new MyDatabaseEntities())
        //    {
        //        var model = dc.Clients.Find(ClientsID);
        //        return View(model);
        //    }
        //}
        public ActionResult Delete(int id)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                return View(dc.Clients.Where(x => x.ClientsID == id).FirstOrDefault());
            }
        }

        

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id,FormCollection collection)
        {

            try
            {
                using (MyDatabaseEntities dc = new MyDatabaseEntities())
                {
                    Client item = dc.Clients.Where(x => x.ClientsID == id).FirstOrDefault();
                    dc.Clients.Remove(item);
                    dc.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch 
            {

                return View();
            }
            
           
        }


        [HttpPost]
      
        public JsonResult SaveEvent(Event e ,Client c)
        {
            var status = false;
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                if (e.EventID > 0 && c.ClientsID > 0)
                {
                    //Update the event
                    var v = dc.Events.Where(a => a.EventID == e.EventID).FirstOrDefault();
                    var clv = dc.Clients.Where(a => a.ClientsID == c.ClientsID).FirstOrDefault();

                    if (v != null && clv != null )
                    {
                        v.Subject = e.Subject;
                        v.Start = e.Start;
                        v.End = e.End;
                        v.Description = e.Description;
                        v.IsFullDay = e.IsFullDay;
                        v.ThemeColor = e.ThemeColor;
                        v.Price = e.Price;
                        v.Worker = e.Worker;
                        clv.FirstLastName = c.FirstLastName;
                        clv.number = c.number;
                        v.NameClient = e.NameClient;
                        v.NumberClient = e.NumberClient;

                        
                   
                  
                    }
                }
                else
                {
                    dc.Events.Add(e);
                    dc.Clients.Add(c);
                }

                dc.SaveChanges();
                status = true;

            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var v = dc.Events.Where(a => a.EventID == eventID).FirstOrDefault();
                if (v != null)
                {
                    dc.Events.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status} };
        }

    }
}