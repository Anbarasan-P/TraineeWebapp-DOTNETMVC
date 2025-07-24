using System.Web;
using System.Web.Mvc;

namespace TraineeWebapp_DOTNETMVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
