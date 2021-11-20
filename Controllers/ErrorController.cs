using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace HelloMVCWorld.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if ((exceptionHandlerPathFeature != null) && (exceptionHandlerPathFeature.Error != null))
            {
                // In our example, the ExceptionHelper.LogException() method will take care of   
                // logging the exception to the database and perhaps even alerting the webmaster  
                // Make sure that this method doesn't throw any exceptions or you might end  
                // in an endless loop!  
                // ExceptionHelper.LogException(exceptionHandlerPathFeature.Error);  ,Exceptoptiohandler is a class defined by user to log the exception
            }
            return Content("We're so sorry, but an error just occurred! We'll try to get it fixed ASAP!");
        }

        public IActionResult Error404Handling (int statusCode)
        {
            if (statusCode == 404)
            {
                return Content("not found " + statusCode);
            }

            else
            {
                return Content(statusCode.ToString());
            }
        }
    }
}
