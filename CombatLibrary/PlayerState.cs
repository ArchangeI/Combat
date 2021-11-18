namespace CombatLibrary
{
	public class PlayerState
	{
		public readonly string playerName;
		private int _damageToHead = 25;
		private int _damageToBody = 15;
		private int _damageToLeg = 5;

		public int Health { get; private set; }

		public PlayerState(string playerName)
		{
			this.playerName = playerName;
			Health = 100;
		}

		public override string ToString()
		{
			return $"{nameof(playerName)}: {playerName}, {nameof(Health)}: {Health}";
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

		public void Hit(PlayerState otherPlayerState, HitAndBlock typeOfHit, HitAndBlock typeOfBlock)
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
