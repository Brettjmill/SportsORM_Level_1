using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsORM.Models;


namespace SportsORM.Controllers
{
    public class HomeController : Controller
    {

        private static Context _context;

        public HomeController(Context DBContext)
        {
            _context = DBContext;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.BaseballLeagues = _context.Leagues
                .Where(l => l.Sport.Contains("Baseball"))
                .ToList();
            return View();
        }

        [HttpGet("level_1")]
        public IActionResult Level1()
        {
            ViewBag.WomensLeagues = _context.Leagues
                .Where(l => l.Name.Contains("Women"))
                .ToList();

            ViewBag.HockeyLeagues = _context.Leagues
                .Where(l => l.Name.Contains("Hockey"))
                .ToList();

            ViewBag.NotFootball = _context.Leagues
                .Where(l => l.Sport != "Football")
                .ToList();

            ViewBag.ConferenceLeagues = _context.Leagues
                .Where(l => l.Name.Contains("Conference"))
                .ToList();

            ViewBag.AtlanticLeagues = _context.Leagues
                .Where(l => l.Name.Contains("Atlantic"))
                .ToList();

            ViewBag.DallasTeams = _context.Teams
                .Where(t => t.Location.Contains("Dallas"))
                .ToList();

            ViewBag.RaptorTeams = _context.Teams
                .Where(t => t.TeamName.Contains("Raptors"))
                .ToList();

            ViewBag.CityTeams = _context.Teams
                .Where(t => t.Location.Contains("City"))
                .ToList();

            ViewBag.TNamedTeams = _context.Teams
                .Where(t => t.TeamName.StartsWith("T"))
                .ToList();

            ViewBag.AlphabetizedTeams = _context.Teams
                .OrderBy(l => l.Location);
                
            ViewBag.ReverseAlphaTeamNames = _context.Teams
                .OrderByDescending(tn => tn.TeamName);

            ViewBag.CooperPlayers = _context.Players
                .Where(ln => ln.LastName.Contains("Cooper"))
                .ToList();

            ViewBag.JoshuaPlayers = _context.Players
                .Where(fn => fn.FirstName.Contains("Joshua"))
                .ToList();

            ViewBag.CooperNotJoshPlayers = _context.Players
                .Where(n => n.LastName.Contains("Cooper") && n.FirstName != "Joshua")
                .ToList();

            ViewBag.AlexanderWyattPlayers = _context.Players
                .Where(n => n.FirstName.Contains("Alexander") || n.FirstName.Contains("Wyatt"))
                .ToList();

            return View();
        }

        [HttpGet("level_2")]
        public IActionResult Level2()
        {
            ViewBag.ASCTeams = _context.Teams
                .Include(t => t.CurrLeague)
                .Where(t => t.CurrLeague.Name == "Atlantic Soccer Conference")
                .ToList();

            ViewBag.BostonPenguins = _context.Players
                .Include(p => p.CurrentTeam)
                .Where(p => p.CurrentTeam.TeamName == "Penguins" && p.CurrentTeam.Location == "Boston")
                .ToList();

            ViewBag.ICBConferencePlayers = _context.Players
                .Include(p => p.CurrentTeam)
                .Where(p => p.CurrentTeam.CurrLeague.Name == "International Collegiate Baseball Conference")
                .ToList();

            ViewBag.ACAFootballPlayersLopez = _context.Players
                .Include(p => p.CurrentTeam)
                .Where(p => p.CurrentTeam.CurrLeague.Name == "American Conference of Amateur Football"  && p.LastName == "Lopez")
                .ToList();

            ViewBag.AllFootballPlayers = _context.Players
                .Include(p => p.AllTeams)
                .Where(p => p.AllTeams
                .Any(t => t.TeamOfPlayer.CurrLeague.Sport == "Football"))
                .ToList();

            ViewBag.TeamsWithSophia = _context.Teams
                .Include(t => t.CurrentPlayers)
                .Where(t => t.CurrentPlayers
                .Any(p => p.FirstName == "Sophia"))
                .ToList();

            ViewBag.LeaguesWithSophia = _context.Leagues
                .Include(l => l.Teams)
                .Where(l => l.Teams
                .Any(t => t.CurrentPlayers
                .Any(p => p.FirstName == "Sophia")))
                .ToList();

            ViewBag.FloresNotARoughrider = _context.Players
                .Include(p => p.CurrentTeam)
                .Where(p => p.CurrentTeam.TeamName != "Roughriders" && p.CurrentTeam.Location != "Washington" && p.LastName == "Flores")
                .ToList();

            return View();
        }

        [HttpGet("level_3")]
        public IActionResult Level3()
        {
            return View();
        }

    }
}