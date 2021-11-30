using System.Collections.Generic;
using System.Linq;

namespace CombatLibrary
{
    public enum TypeOfGame
    {
        Bot = 1,
        Player = 2
    }

    public static class GameManager
    {
        static GameManager()
        {
            Engine.CombatSessionStarted += Engine_CombatSessionStarted;
        }

        private static List<CombatSession> sessions = new List<CombatSession>();

        public static List<Player> players = new List<Player>();

        private static void Engine_CombatSessionStarted(object sender, CombatSession session)
        {
            sessions.Add(session);
            sessions = sessions.Where(x => !x.IsSessionOver).ToList();
            players = players.Where(x => x.Health > 0).ToList();
        }

        private static CombatSession GetSession(string id)
        {
            var session = sessions.Where(x => x.GetState().P1State.Identity == id
                || x.GetState().P2State.Identity == id)
                .FirstOrDefault();

            return session;
        }

        private static bool IsFightWithBot(string id)
        {
            var player = GetPlayer(id);

            return CheckCondition(player.FightWithBot);
        }

        private static bool CheckCondition(bool condition) => condition ? true : false;

        public static State GetState(string id)
        {
            var state = sessions.Select(x => x.GetState())
                .Where(x => x.P1State.Identity == id || x.P2State.Identity == id)
                .FirstOrDefault();

            return state;
        }

        public static Player GetPlayer(string id)
        {
            var player = players.Where(x => x.Identity == id)
                         .FirstOrDefault();

            return player;
        }

        public static bool IsSessionStarted(string id)
        {
            var state = GetState(id);

            return CheckCondition(state != null);
        }

        public static State PlayerChoise(string identity, HitAndBlock hitAndBlock)
        {
            var state = GetState(identity);
            var session = GetSession(identity);

            void Fighting(bool attacks, CombatSession session, HitAndBlock hitAndBlock)
            {
                if (attacks)
                {
                    session.AttackerAttackAt(hitAndBlock);
                }
                else
                {
                    session.DefenderDefends(hitAndBlock);
                }
            }

            if (state.P1State.Identity == identity)
            {
                Fighting(state.Player1Attacks, session, hitAndBlock);

                if (IsFightWithBot(identity))
                    Fighting(state.Player2Attacks, session, Bot.BotChoise());

                return session.GetState();
            }

            if (state.P2State.Identity == identity)
            {
                Fighting(state.Player2Attacks, session, hitAndBlock);
                return session.GetState();
            }
           
            return null;
        }
    }
}
