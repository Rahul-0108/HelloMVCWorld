﻿using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

public class LogAttribute : ActionFilterAttribute  // Custom  Filter  https://www.tutorialsteacher.com/mvc/action-filters-in-mvc
{
    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
        Log("OnActionExecuted", filterContext.RouteData);
    }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        Log("OnActionExecuting", filterContext.RouteData);
    }

    public override void OnResultExecuted(ResultExecutedContext filterContext)
    {
        Log("OnResultExecuted", filterContext.RouteData);
    }

    public override void OnResultExecuting(ResultExecutingContext filterContext)
    {
        Log("OnResultExecuting ", filterContext.RouteData);
    }

    private void Log(string methodName, RouteData routeData)
    {
        var controllerName = routeData.Values["controller"];
        var actionName = routeData.Values["action"];
        var message = String.Format("{0}- controller:{1} action:{2}", methodName,
                                                                    controllerName,
                                                                    actionName);
        Debug.WriteLine(message); //  Write   Message to  Debug  Window
    }
}