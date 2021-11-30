namespace CombatLibrary
{
    public class CombatSession
    {
        private readonly State _state;

        public CombatSession(Player player1, Player player2)
        {
            _state = new State
            {
                P1State = player1,
                P2State = player2,
                Player1Attacks = true
            };
        }

        public State GetState() => _state;

        public bool IsSessionOver => _state.P1State.Health <= 0 || _state.P2State.Health <= 0;

        private bool IsRoundReady => _state.AttackerChoise != HitAndBlock.Nothing &&
                                     _state.DefenderChoise != HitAndBlock.Nothing &&
                                     !IsSessionOver;

        private void CheckForRoundReady()
        {
            if (IsRoundReady)
            {
                FightOneRound();
            }
        }

        public void AttackerAttackAt(HitAndBlock target)
        {
            _state.AttackerChoise = target;

            CheckForRoundReady();
        }

        public void DefenderDefends(HitAndBlock target)
        {
            _state.DefenderChoise = target;

            CheckForRoundReady();
        }

        private void FightOneRound()
        {

            var attacker = _state.Player1Attacks ? _state.P1State : _state.P2State;
            var defender = _state.Player1Attacks ? _state.P2State : _state.P1State;

            attacker.Hit(defender, _state.AttackerChoise, _state.DefenderChoise);

            _state.AttackerChoise = HitAndBlock.Nothing;
            _state.DefenderChoise = HitAndBlock.Nothing;
            _state.Player1Attacks = !_state.Player1Attacks;
        }
    }
}
