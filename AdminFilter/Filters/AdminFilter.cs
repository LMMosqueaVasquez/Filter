using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


public class AdminOnly : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext actionContext)
    {
         var admin = actionContext.HttpContext.Session.GetInt32("Adm");

         if(admin!= 1)
         {
          Console.WriteLine(admin);
          actionContext.Result = new UnauthorizedResult();
         }

       
        
    }

}