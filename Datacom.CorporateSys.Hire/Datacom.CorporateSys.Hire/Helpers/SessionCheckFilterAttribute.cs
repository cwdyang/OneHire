using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Datacom.CorporateSys.Hire.Constants;
using Datacom.CorporateSys.Hire.Controllers;
using Datacom.CorporateSys.Hire.ViewModels;

namespace Datacom.CorporateSys.Hire.Helpers
{
    /// <summary>
    /// you can add this to FilterConfig.RegisterGlobalFilters
    /// </summary>
    public class SessionCheckFilterAttribute:ActionFilterAttribute
    {
        private bool _checkExamNotNull = false;

        public SessionCheckFilterAttribute()
        {
            _checkExamNotNull = false;
        }

        public SessionCheckFilterAttribute(bool checkExamNotNull)
        {
            _checkExamNotNull = checkExamNotNull;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();


            if (filterContext.Controller is ExamController)
            {
                var viewModel = filterContext.HttpContext.Session.GetDataFromSession<ExamViewModel>(SessionConstants.ExamViewModel);

                var sessionCheckFail = (_checkExamNotNull)? (viewModel == null || viewModel.Candidate == null|| viewModel.Exam==null): (viewModel == null || viewModel.Candidate == null);

                if (sessionCheckFail || !filterContext.HttpContext.Request.IsAuthenticated)
                {
                    if(filterContext.HttpContext.Request.IsAjaxRequest())
                        filterContext.Result = new EmptyResult();
                    else
                    {
                        var url = new UrlHelper(filterContext.RequestContext);
                        var loginUrl = url.Content("~/Account/Login");
                        filterContext.HttpContext.Response.Redirect(loginUrl, true);
                        filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Redirect); //STOPS execution!!
                    }
                }
            }

        }
    }
}