using Ecommerce.Data;
using Ecommerce.Data.Migrations;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.Controllers
{
    [Authorize]
    public class MangerUserController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _environment;
        public MangerUserController(UserManager<ApplicationUser> userManager, IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _environment = environment;
        }
        public async Task< IActionResult> Index()
        {
            var user =await _userManager.FindByEmailAsync(User.Identity?.Name);
            var userProfile = new USerProfile
            {
                FullName = user.FullName,
                PicPath = user.PicPath,
            };
            return View(userProfile);
        }
        [HttpGet]
        public IActionResult CreateProfile()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> CreateProfile(USerProfile uSerprofile)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (uSerprofile.ProfilePic is null  )
            {
                ModelState.AddModelError("", "No Image files were uploaded");
                return View();
            }
            string imagePath = Path.Combine(_environment.WebRootPath, "ProfilePic");
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }
            
                string fileName = Path.GetFileName(uSerprofile.ProfilePic.FileName);
                string fullPath = Path.Combine(imagePath, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    uSerprofile.ProfilePic.CopyTo(stream);
                }
          var loggedUser= await _userManager.FindByEmailAsync(   User.Identity.Name);
            loggedUser.PicPath = "/ProfilePic/" + fileName;
            loggedUser.FullName = uSerprofile.FullName;
        var result=    await  _userManager.UpdateAsync(loggedUser);
            if(result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
           if(result.Errors.Count()>0)
            {

            }

            return View();
        }
    }
}
