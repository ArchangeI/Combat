namespace CombatLibrary
{
	public class State
	{
		public PlayerState P1State { get; set; }

		public PlayerState P2State { get; set; }

		public bool Player1Attacks { get; set; }

		public bool Player2Attacks => !Player1Attacks;

		public HitAndBlock AttackerChoise { get; set; }

		public HitAndBlock DefenderChoise { get; set; }
	}
}
