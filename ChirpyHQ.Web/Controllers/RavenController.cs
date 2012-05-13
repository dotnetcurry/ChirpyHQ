using System.Web.Mvc;
using Raven.Client;
namespace ChirpyHQ.Controllers
{
    public class RavenController : Controller
    {
        public IDocumentSession RavenSession { get; set; }
        protected IDocumentStore _documentStore;

        public RavenController(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            RavenSession = _documentStore.OpenSession();
            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            using (RavenSession)
            {
                if (Session != null && filterContext.Exception == null)
                {
                    RavenSession.SaveChanges();
                }
            }
            base.OnActionExecuted(filterContext);
        }
    }

}
