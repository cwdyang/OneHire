using System.Web;
using System.Web.Mvc;

namespace Datacom.CorporateSys.Hire
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}