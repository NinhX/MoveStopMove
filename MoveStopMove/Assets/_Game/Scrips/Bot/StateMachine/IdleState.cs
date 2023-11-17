using UnityEngine;

public class IdleState : IState<Bot>
{
    private float time;
    private float timeIdle;

    public void OnEnter(Bot bot)
    {
        bot.Stop();
        timeIdle = Random.Range(1f, 3f);
    }

    public void OnExecute(Bot bot)
    {
        time += Constant.TIME_NEW_UPDATE;
        if (time > timeIdle)
        {
            bot.ChangeState(BotState.Patrol);
        }
    }

    public void OnExit(Bot t)
    {
        time = 0;
    }
}
