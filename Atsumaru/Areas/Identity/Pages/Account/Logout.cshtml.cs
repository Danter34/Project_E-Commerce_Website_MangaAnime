// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Atsumaru.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<IdentityUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            // Luôn chuyển hướng đến trang chủ hoặc trang được chỉ định
            returnUrl ??= Url.Page("/Index"); // hoặc returnUrl = "/"; nếu muốn chắc chắn về URL
            return LocalRedirect(returnUrl);
        }

        // Ngăn người dùng truy cập trang này bằng GET
        public IActionResult OnGet()
        {
            return RedirectToPage("/Index"); // Hoặc return NotFound();
        }
    }
}
