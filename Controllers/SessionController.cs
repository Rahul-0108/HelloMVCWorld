using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace HelloMVCWorld.Controllers
{
    [Log] // Custom  Filter  https://www.tutorialsteacher.com/mvc/action-filters-in-mvc
    public class SessionController : Controller  // Maintain  Session  Data https://asp.mvc-tutorial.com/httpcontext/sessions/
    {
        public IActionResult Index()
        {
             HttpContext.Session.TryGetValue("Name" , out byte[] name);
             return View(model: name != null ? Encoding.ASCII.GetString(name) : null);
        }

        [HttpPost]
        public IActionResult Index(string name)  // Same Action Name is Required as  Form Posts  Data to  Same  Action   Name
        {
            HttpContext.Session.Set("Name", Encoding.ASCII.GetBytes(name));
            return RedirectToAction("Index");
        }
    }
}
