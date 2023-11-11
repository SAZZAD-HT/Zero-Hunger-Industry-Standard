using System.Web;
using System.Web.Mvc;

namespace Zero_Hunger_Industry_Standard
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
