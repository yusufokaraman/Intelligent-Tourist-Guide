﻿using ITG.Entities.Concrete;
using ITG.Mvc.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITG.Mvc.Areas.Admin.ViewComponents
{
    public class UserMenuViewComponent:ViewComponent
    {
        private readonly UserManager<User> _userManager;

        public UserMenuViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public ViewViewComponentResult Invoke()
        {
            var user =  _userManager.GetUserAsync(HttpContext.User).Result;
            return View(new UserViewModel
            {
                User = user
            });


        }
    }
}
