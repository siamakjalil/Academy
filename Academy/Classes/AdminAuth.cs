using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataLayer.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Academy.Classes
{

    public class ClaimRequirementAttribute : TypeFilterAttribute
    {
        public ClaimRequirementAttribute() : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { };
        }
    }

    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        private IAdmin _admin;

        public ClaimRequirementFilter(IAdmin admin)
        {
            _admin = admin;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var identity = context.HttpContext.User.Identity;
            if (identity == null)
            {
                context.Result = new RedirectResult("/Account/Login");
                return;
            }
            if (!identity.IsAuthenticated)
            {
                context.Result = new RedirectResult("/Account/Login");
                return;
            }
        }
    }

}
