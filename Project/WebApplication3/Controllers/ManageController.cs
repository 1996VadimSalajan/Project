using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CodeFirst;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebApplication3.App_Code;
using WebApplication3.Models;
using WebApplication3.Service;

namespace WebApplication3.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        IRepository<User> _repuser;
        private IAuthenticationManager _AuthenticationManager;


        public ManageController(ApplicationSignInManager signInManager, ApplicationUserManager userManager, IRepository<User> repuser, IAuthenticationManager AuthenticationManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _repuser = repuser;
            _AuthenticationManager = AuthenticationManager;
        }

        public ActionResult UserInformation()
        {
            return PartialView(_repuser.Read().ToList());
        }

        public async Task<ActionResult> Index(ManageService.ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageService.ManageMessageId.ChangePasswordSuccess ? Resources.ChangedPassword
                : message == ManageService.ManageMessageId.SetPasswordSuccess ? Resources.SetedPassword
                : message == ManageService.ManageMessageId.Error ? Resources.Error
                : "";

            var userId = User.Identity.GetUserId();
            var user = new User();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(user),
                BrowserRemembered = await _AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }

        public ActionResult ChangePassword()
        {
            return base.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _userManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageService.ManageMessageId.ChangePasswordSuccess });
            }

            AddErrors(result);
            return View(model);
        }

        public ActionResult SetPassword()
        {
            return base.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageService.ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }
            return View(model);
        }
       
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword(User user)
        {
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }
    }
}