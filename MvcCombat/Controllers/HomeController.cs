using Microsoft.AspNetCore.Mvc;
using CombatLibrary;
using AutoMapper;
using Combat.DAL.Entities;
using Combat.DAL.EF;

namespace MvcCombat.Controllers
{
    public class HomeController : Controller
    {
        private CombatContext _context;

        public HomeController(CombatContext context)
        {
            _context = context;
        }

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

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Player, PlayerDAL>());
            var mapper = new Mapper(config);
            var playerDAL = mapper.Map<Player, PlayerDAL>(player);

            _context.Add(playerDAL);
            _context.SaveChanges();

            ViewData["name"] = name;
            ViewData["identity"] = player.Id;

            if (type == TypeOfGame.Bot)
            {
                SessionWithBot(player.Id);
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

            PlayerDAL player = _context.Players.Find(identity);
            if (player != null)
                _context.Players.Remove(player);
            _context.SaveChanges();

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
