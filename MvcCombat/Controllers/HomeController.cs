using Microsoft.AspNetCore.Mvc;
using CombatLibrary;

namespace MvcCombat.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Selecting(string name)
        {
            ViewData["name"] = name;

            return View();
        }

        [HttpPost]
        public IActionResult Selecting(TypeOfGame type, string name)
        {
            var player = Engine.CreateDefaultPlayer(name);

            GameManager.players.Add(player);

            ViewData["name"] = name;
            ViewData["identity"] = player.Identity;

            if (type == TypeOfGame.Bot)
            {
                SessionWithBot(player.Identity);
            }

            return View("Waiting");
        }

        public IActionResult SessionWithBot(string identity)
        {
            var player = GameManager.GetPlayer(identity);
            player.FightWithBot = true;
            Engine.CreateDefaultPlayer(new Bot().Name);
            ViewData["identity"] = identity;
            return View("Waiting");
        }

        public IActionResult Waiting(string identity)
        {
            ViewData["identity"] = identity;
            ViewData["State"] = GameManager.GetState(identity);

            return View();
        }

        public IActionResult Refresh(string identity)
        {
            var Model = GameManager.GetState(identity);
            ViewData["identity"] = identity;

            if (Model.P1State.Health > 0 && Model.P2State.Health > 0)
            {
                return View("Fighting", Model);
            }

            return View("EndSession", Model);
        }

        public IActionResult Fighting(string identity, HitAndBlock hitAndBlock)
        {
            var Model = GameManager.PlayerChoise(identity, hitAndBlock);

            ViewData["identity"] = identity;

            return View(Model);
        }
    }
}
