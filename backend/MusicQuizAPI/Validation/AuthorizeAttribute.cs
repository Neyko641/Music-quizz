using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MusicQuizAPI.Models;
using MusicQuizAPI.Models.Database;
using System;
using MusicQuizAPI.Exceptions;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        User user = (User)context.HttpContext.Items["User"];

        if (user == null)
        {
            // not logged in
            throw new UnauthorizedException("Authorization token isn't provided or it's not valid!");
        }
    }
}