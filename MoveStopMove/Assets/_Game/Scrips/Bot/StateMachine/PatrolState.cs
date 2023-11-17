public class PatrolState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {
        if (bot.CheckTargetTracked())
        {
            bot.MoveToTarget();
        }
        else
        {
            bot.Move();
        }
    }

    public void OnExecute(Bot bot)
    {
        if (!bot.IsMoving())
        {
            bot.ChangeState(BotState.Idle);
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
