using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloMVCWorld.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace HelloMVCWorld.Controllers
{
  
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        private IMemoryCache _memoryCache;

        public HomeController(IConfiguration configuration , IOptions<IOptionsValues> websiteOptions , IMemoryCache memoryCache)
        {
            _configuration = configuration;
            _memoryCache = memoryCache;
        }
        public IActionResult Index(int i) // LocalHost1234/Home and LocalHost:1234 will Map here with i=0 , LocalHost:1234/1 will not map here ,  LocalHost:1234?i=1 will  Map  Here with i=1
        {
            string iConfigurationValue = _configuration.GetValue<string>("Type"); // Demos How to Fetch values from AppSettings https://asp.mvc-tutorial.com/core-concepts/configuration/
            string iConfigurationValue2 = _configuration.GetSection("Website").GetValue<string>("Title"); // Demos How to Fetch values from AppSettings Json Object

            // HttpContext.Request - all members related to the current request, e.g.the QueryString, Forms and so on.
            // HttpContext.Response - all members related to the Response about to be delivered, e.g.Cookies and response headers
            // HttpContext.Session - all members related to dealing with Session(generally used to persist data between requests)
            // HttpContext.User - all members related to dealing with a(potentially) authenticated user
            HttpContext httpContext = HttpContext;

            HttpRequest request = Request; // All members related to the current request, e.g.the QueryString, Forms and so on
            Models.Movie movie = new Models.Movie()
            {
                Title = "The Godfather",
                ReleaseDate = new DateTime(1972, 3, 24)
            };
            ViewData["Data1"] = "ViewData Value"; //  Dynamic Data Passed tot the  View
            ViewBag.Data2 = 2; //  Dynamic Data Passed tot the  View
            return View(movie);
        }

        [HttpGet]
        [Route("/SimpleBinding/{i}")] // This is the only Route to get to this  Action , Default Routing won't work  After  Applying  Attribute  Routing 

        // LocalHost:1234/SimpleBinding won't map here , SimpleBinding?i=1 won't map here , /SimpleBinding/1 will map Here with i=1 , /SimpleBinding/1?i=2 will map Here with i=1
        public IActionResult SimpleBinding(int i) 
        {
            return View(new WebUser() { FirstName = "John", LastName = "Doe" });
        }

        [HttpGet]
        [Route("/QueryParametersTesting/{i?}")]
        // /QueryParametersTesting will map Here with i=0 ,/QueryParametersTesting?i=1 will map Here with i=1 , /QueryParametersTesting/1 will map Here with i =0 , /QueryParametersTesting/1?i=2 will map Here with i=2
        public IActionResult QueryParametersTesting([FromQuery] int i) // FromQuery tells That Take Value FromQuery Passed in  URL , if FromQuery is not defined , then give Priority to  Route Template  Value
        {
            return Content("QueryParameters Testing");
        }

        [HttpGet]
        [Route("/QueryParametersTesting2/{i?}")]
        // /QueryParametersTesting2 will map Here with i=0 ,/QueryParametersTesting2?i=1 will map Here with i=1 , /QueryParametersTesting2/1 will  map Here with i=1, /QueryParametersTesting2/1?i=2 will  map Here with i=1
        public IActionResult QueryParametersTesting2(int i) 
        {
            return Content("QueryParameters2 Testing");
        }

        [HttpGet]
        [Route("/QueryParametersTesting4")]
        // /QueryParametersTesting4 will map Here with i=0 ,/QueryParametersTesting4?i=1 will map Here with i=1 , /QueryParametersTesting2/1  will not map  Here, /QueryParametersTesting4/1?i=2 will not map  Here
        public IActionResult QueryParametersTesting4(int i)
        {
            return Content("QueryParameters4 Testing");
        }


        [HttpPost]
        public IActionResult SimpleBinding(WebUser webUser) // Same Action Name is Required as  Form Posts  Data to  Same  Action   Name
        {
            //TODO: Update in DB here...
            //return Content($"User {webUser.FirstName} updated!");
            string formData = HttpContext.Request.Form["FirstName"]; // Demos how to get Data Posted to Server , Value here is John https://asp.mvc-tutorial.com/httpcontext/forms-post-data/

            if (!HttpContext.Request.Cookies.ContainsKey("first_request")) // Demos Cookies in Asp.net Core https://asp.mvc-tutorial.com/httpcontext/cookies/
            {
                HttpContext.Response.Cookies.Append("first_request", DateTime.Now.ToString());
            }
            else
            {
                DateTime firstRequest = DateTime.Parse(HttpContext.Request.Cookies["first_request"]);
                string cookieData = "Welcome back, user! You first visited us on: " + firstRequest.ToString();
            }

            return View(webUser);
        }

        public IActionResult GetOrCreateWithAbsoluteExpiration() // https://asp.mvc-tutorial.com/caching/in-memory-caching-with-imemorycache/
        {
            string cacheKey = DateTime.Now.ToString("yyyyMMdd");

            string cachedMessage = this._memoryCache.GetOrCreate(cacheKey, entry =>
            {
                // Create a fake delay of 3 seconds to simulate heavy processing...
                System.Threading.Thread.Sleep(3000);

                entry.SetAbsoluteExpiration(TimeSpan.FromSeconds(120));

                return "Cache was last refreshed @ " + DateTime.Now.ToLongTimeString();
            });

            return Content(cachedMessage);
        }

        [HttpGet]
        public IActionResult throwException()
        {
            throw new Exception("This is an Exception to demo Exception Handling in Asp.net Core");
        }
    }
}
