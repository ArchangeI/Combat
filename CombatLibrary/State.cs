namespace CombatLibrary
{
	public class State
	{
		public Player P1State { get; set; }

		public Player P2State { get; set; }

		public bool Player1Attacks { get; set; }

		public bool Player2Attacks => !Player1Attacks;

		public HitAndBlock AttackerChoise { get; set; }

		public HitAndBlock DefenderChoise { get; set; }
	}
}
