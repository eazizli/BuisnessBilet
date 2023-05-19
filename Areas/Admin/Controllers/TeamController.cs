using eBusiness.DataContext;
using eBusiness.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace eBusiness.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly EbuisnesDbContex _ebuisnesDb;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TeamController(EbuisnesDbContex ebuisnesDb,IWebHostEnvironment webHostEnvironment )
        {
            _ebuisnesDb = ebuisnesDb;
            _webHostEnvironment = webHostEnvironment;
        }
    
        public async Task< IActionResult> Index()
        {
            List<Team> teams = await _ebuisnesDb.Teams.ToListAsync();
            return View(teams);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Team team)
        {
            
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            string guid = Guid.NewGuid().ToString();
            string newfile=guid+team.Images.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", "team", newfile);
            using(FileStream filestream=new FileStream(path, FileMode.Create))
            {
                await team.Images.CopyToAsync(filestream);
            }
            Team newTeam = new Team();
            newTeam.ImageName=newfile;
            newTeam.Work = team.Work;
            newTeam.Name = team.Name;
            
          

            await _ebuisnesDb.AddAsync(newTeam);
            await _ebuisnesDb.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Delete(int id)
        {
            Team teams = await _ebuisnesDb.Teams.FindAsync(id);
            if(teams == null)
            {
                return NotFound();
            }

            _ebuisnesDb.Teams.Remove(teams);
            _ebuisnesDb.SaveChanges();
            return RedirectToAction("Index");


        }
        public async Task<IActionResult> Update(int id)
        {
            Team team = await _ebuisnesDb.Teams.FindAsync(id);
            if(team == null)return NotFound();
            return View(team);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,Team newteam)
        {
            Team teamDb = await _ebuisnesDb.Teams.FindAsync(id);
            if(teamDb == null)return NotFound();
            teamDb.Name = newteam.Name;
            teamDb.Work = newteam.Work;
            string guid = Guid.NewGuid().ToString();
            string newfile = guid + teamDb.Images.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", "team", newfile);
            using (FileStream filestream = new FileStream(path, FileMode.Create))
            {
                await teamDb.Images.CopyToAsync(filestream);
            }
            Team newTeam = new Team();
            await _ebuisnesDb.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
