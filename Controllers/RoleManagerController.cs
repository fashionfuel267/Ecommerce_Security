using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class RoleManagerController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        public RoleManagerController(RoleManager<IdentityRole> roleManager)
        {
            this._roleManager=roleManager;
        }
        public IActionResult Index()
        {
            return View(_roleManager.Roles.OrderBy(r=>r.Name).ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create( string name)
        {
          var result= await _roleManager.CreateAsync(new IdentityRole { Name = name });
            if(result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            if (result.Errors.Count() > 0)
            {

            }
            return View();
        }
    }
}
