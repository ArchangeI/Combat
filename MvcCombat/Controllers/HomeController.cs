using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcCombat.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

        public IActionResult Waiting(string identity)
        {
            if (MvcGame.IsPlayerNotCreated(identity))
            {
                var player = Engine.CreateDefaultPlayer(identity);
                MvcGame.players.Add(player);
            }

            ViewData["identity"] = identity;
            ViewData["State"] = MvcGame.GetState(identity);
            return View();
        }

        public IActionResult Refresh(string identity)
        {
            var Model = MvcGame.GetState(identity);
            ViewData["identity"] = identity;

            if (Model.P1State.Health > 0 && Model.P2State.Health > 0)
            {
                return View("Fighting", Model);
            }

            return View("EndSession", Model);
        }

        public IActionResult Fighting(string identity, HitAndBlock hitAndBlock)
        {
            var Model = MvcGame.PlayerChoise(identity, hitAndBlock);

            ViewData["identity"] = identity;

            return View(Model);
        }
    }
}
