using System;

namespace CombatLibrary
{
	public class Player
	{
		public readonly string Name;
		private int _damageToHead = 25;
		private int _damageToBody = 15;
		private int _damageToLeg = 5;

		public bool FightWithBot;

		public int Health { get; private set; }

		public string Identity { get; private set; }

		public Player(string playerName)
		{
			Name = playerName;
			Identity = Guid.NewGuid().ToString();
			Health = 100;
		}

		public int HowMuchDamageIDid(HitAndBlock attackTarget, HitAndBlock defenseTarget)
		{
			if (attackTarget == defenseTarget)
				return 0;

			switch (attackTarget)
			{
				case HitAndBlock.Head:
					return _damageToHead;
				case HitAndBlock.Body:
					return _damageToBody;
				case HitAndBlock.Leg:
					return _damageToLeg;

				case HitAndBlock.CheatForDev:
					return 100;
			}

			return 0;
		}

		public void Hit(Player otherPlayerState, HitAndBlock typeOfHit, HitAndBlock typeOfBlock)
		{
			if (otherPlayerState.Health <= 0)
			{
				otherPlayerState.Health = 0;
			}
			else
			{
				otherPlayerState.Health -= HowMuchDamageIDid(typeOfHit, typeOfBlock);
			}
		}
	}
}
