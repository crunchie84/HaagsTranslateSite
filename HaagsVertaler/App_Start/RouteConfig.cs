using System.Web.Mvc;
using System.Web.Routing;

namespace HaagsVertaler
{
  public class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapMvcAttributeRoutes();
    }
  }
}
