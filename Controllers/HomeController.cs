using eBusiness.DataContext;
using eBusiness.Models;
using Microsoft.AspNetCore.Mvc;

namespace eBusiness.Controllers
{
    public class HomeController : Controller
    {
        private readonly EbuisnesDbContex _ebuisnesDb;
        public HomeController(EbuisnesDbContex ebuisnesDb)
        {
            _ebuisnesDb=ebuisnesDb;
        }
        
        public IActionResult Index()
        {
           List<Team> teams = _ebuisnesDb.Teams.ToList();
            return View(teams);
        }
    }
}
