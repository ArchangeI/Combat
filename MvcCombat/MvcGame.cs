using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcCombat.Models;
using CombatLibrary;

namespace MvcCombat
{
    public static class MvcGame
    {
        static MvcGame()
        {
            Engine.CombatSessionStarted += Engine_CombatSessionStarted;
        }

        private static List<CombatSession> sessions = new List<CombatSession>();

        public static List<PlayerState> players = new List<PlayerState>();

        private static void Engine_CombatSessionStarted(object sender, CombatSession session)
        {
            sessions.Add(session);

            sessions = sessions.Where(x => !x.IsSessionOver).ToList();

        }

        public static State state { get; private set; }

        public static bool IsSessionStarted(string id)
        {
            foreach (var session in sessions)
            {
                var state = session.GetState();

                if (state.P1State.playerName == id)
                {
                    return true;
                }
                if (state.P2State.playerName == id)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsPlayerNotCreated(string id)
        {
            foreach (var player in players)
            {
                if(player.playerName == id)
                {
                    return false;
                }
            }

            return true;
        }

        public static State GetState(string id)
        {
            foreach (var session in sessions)
            {
                state = session.GetState();

                if (state.P1State.playerName == id)
                {
                    return session.GetState();
                }

                if (state.P2State.playerName == id)
                {
                    return session.GetState();
                }
            }

            return null;
        }

        public static State PlayerChoise(string identity, HitAndBlock hitAndBlock)
        {
            foreach (var session in sessions)
            {
                state = session.GetState();
                
                if(state.P1State.playerName == identity)
                {
                    if (state.Player1Attacks)
                    {
                        session.AttackerAttackAt(hitAndBlock);
                        return session.GetState();
                    }
                    else
                    {
                        session.DefenderDefends(hitAndBlock);
                        return session.GetState();
                    }
                }

                if (state.P2State.playerName == identity)
                {
                    if (state.Player2Attacks)
                    {
                        session.AttackerAttackAt(hitAndBlock);
                        return session.GetState();
                    }
                    else
                    {
                        session.DefenderDefends(hitAndBlock);
                        return session.GetState();
                    }
                }
            }

            return null;
        }
    }
}
