using System;
using System.Collections.Generic;

namespace CombatLibrary
{
    public enum HitAndBlock
    {
        Nothing = 0,
        Head = 1,
        Body = 2,
        Leg = 3,
        CheatForDev = 4
    }

    public static class Engine
    {
        public static Queue<PlayerState> PlayersWaitingForGame = new Queue<PlayerState>();
        public static event EventHandler<CombatSession> CombatSessionStarted;

        public static CombatSession CreateSession(PlayerState player1, PlayerState player2)
        {
            return new CombatSession(player1, player2);
        }


        public static PlayerState CreateDefaultPlayer(string identity)
        {
            var player = new PlayerState(identity);
            PlayersWaitingForGame.Enqueue(player);

            if (PlayersWaitingForGame.Count >= 2)
            {
                var player1 = PlayersWaitingForGame.Dequeue();
                var player2 = PlayersWaitingForGame.Dequeue();
                var session = CreateSession(player1, player2);

                CombatSessionStarted?.Invoke(null, session);
            }

            return player;
        }
    }
}
