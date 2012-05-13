using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChirpyHQ.Domain.Model;
using ChirpyHQ.Models;
//using ChirpyHQ.Domain.Repository;
//using ChirpyHQ.Domain.Service;
using Raven.Client;

namespace ChirpyHQ.Controllers
{
    public class AdministrationController : RavenController
    {
        public AdministrationController(IDocumentStore documentStore)
            : base(documentStore)
        {
        
        }

        //
        // GET: /Administration/

        public ActionResult Index()
        {
            IEnumerable<Chirp> list = RavenSession.Query<Chirp>();
            return View(list);
        }

        //
        // GET: /Administration/Details/5

        public ActionResult Details(int id = 0)
        {
            Chirp chirp = RavenSession.Load<Chirp>(id);
            if (chirp == null)
            {
                return HttpNotFound();
            }
            return View(chirp);
        }

        //
        // GET: /Administration/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Administration/Create

        [HttpPost]
        public ActionResult Create(Chirp newChirp)
        {
            if (ModelState.IsValid)
            {
                string[] tokens = newChirp.Value.Split(new char[] { ' ' });
                newChirp.Tags = tokens.Where(s => s.StartsWith("#")).ToArray<string>();
                  RavenSession.Store(newChirp);
                return RedirectToAction("Index", "Home");
            }
            return View(newChirp);
        }

        //
        // GET: /Administration/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Chirp chirp = RavenSession.Load<Chirp>(id);
            if (chirp == null)
            {
                return HttpNotFound();
            }
            return View(chirp);
        }

        //
        // POST: /Administration/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Chirp chirp = RavenSession.Load<Chirp>(id);
            if (chirp != null)
            {
                RavenSession.Delete<Chirp>(chirp);
            }
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing); 
        }
    }
}