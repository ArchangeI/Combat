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
        public static Queue<Player> PlayersWaitingForGame = new Queue<Player>();
        public static event EventHandler<CombatSession> CombatSessionStarted;

        public static CombatSession CreateSession(Player player1, Player player2)
        {
            return new CombatSession(player1, player2);
        }


        public static Player CreateDefaultPlayer(string name)
        {
            var player = new Player(name);
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
