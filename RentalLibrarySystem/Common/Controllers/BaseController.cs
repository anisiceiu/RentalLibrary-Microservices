﻿using Common.SharedModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Common.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        public CurrentUser CurrentUser { get { return this.currentUser; } }
        private CurrentUser currentUser = null;
        public readonly IHttpContextAccessor _httpContextAccessor;
        public string? BaseUrl = null;

        public BaseController()
        {
            _httpContextAccessor = HttpContextHelper._httpContextAccessor;
            BaseUrl = HttpContextHelper.BaseUrl(_httpContextAccessor.HttpContext.Request);
            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null && identity.Claims != null && identity.Claims.Any())
            {
                currentUser = new CurrentUser();

                currentUser.UserId = Convert.ToInt32(identity.FindFirst("UserId").Value);
                currentUser.UserName = identity.FindFirst("UserName").Value;
                currentUser.Email = identity.FindFirst("Email").Value;
                currentUser.MemberId = Convert.ToInt32(identity.FindFirst("MemberId").Value);
            }
        }

    }
}
