public class AttackState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        bot.Stop();
    }

    public void OnExecute(Bot bot)
    {
        if (!bot.CheckTarget() || (!bot.CanAttack && !bot.Attacking))
        {
            bot.ChangeState(BotState.Patrol);
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
