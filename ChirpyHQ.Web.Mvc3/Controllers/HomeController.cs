using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using ChirpyHQ.Domain.Service;
using Raven.Client;
using ChirpyHQ.Domain.Model;

namespace ChirpyHQ.Controllers
{
    public class HomeController : RavenController
    {
        public HomeController(IDocumentStore chirpDocumentStore):base(chirpDocumentStore)
        {
        }

        public ActionResult Index()
        {
            IEnumerable<Chirp> list = RavenSession.Query<Chirp>();
            return View(list);
        }

        public ActionResult Tags()
        {
            List<HashTagCount> tags = RavenSession.Query<HashTagCount>("Chirps/HashTagCount")
                                                  .ToList<HashTagCount>();
            return View(tags);
        }

        public ViewResult Search()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Search(FormCollection form)
        {
            string searchQuery = form["query"];
            IQueryable<Chirp> chirpsByTagQuery = from chirp in RavenSession.Query<Chirp>()
                                                 where chirp.Tags.Any(currentTag => currentTag == searchQuery)
                                                            select chirp;
            List<Chirp> chirpList = chirpsByTagQuery.ToList<Chirp>();
            return View("Search", chirpList);
        }
    }
}
