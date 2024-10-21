using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Cool_Co_Fridge_Management.Models;

namespace Cool_Co_Fridge_Management.Attributes
{
    public class RoleAuthorizeAttribute : ActionFilterAttribute
    {
        public readonly string[] _roles;

        public RoleAuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userRole = context.HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(userRole))
            {
                context.Result = new RedirectToActionResult("NewLogin", "Account", null);
                return;
            }

            if (!_roles.Contains(userRole)) 
            {
                context.Result = new RedirectToActionResult("AcessDenied", "Account", null);
            }
            base.OnActionExecuting(context);
        }
    }
}
