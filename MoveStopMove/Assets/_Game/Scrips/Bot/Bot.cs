using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float rangeFollow;

    private Dictionary<BotState, IState<Bot>> botStates = new()
    {
        { BotState.Idle, new IdleState() },
        { BotState.Attack, new AttackState() },
        { BotState.Patrol, new PatrolState() }
    };

    private MapManager mapManager => MapManager.Instance;
    private Vector3 destination;
    private IState<Bot> currentState;
    private Vector3 nextPoint;
    private Character targetTracked;

    public void NewUpdate()
    {
        if (Alive)
        {
            currentState?.OnExecute(this);
        }
    }

    public override void InitModel()
    {
        base.InitModel();

        SetRandomSkin<WeaponType>(ChangeWeapon);
        SetRandomSkin<HatType>(ChangeHat);
        SetRandomSkin<PantType>(ChangePant);
        SetRandomSkin<SkinType>(ChangeSkin);
    }

    public override void OnInit()
    {
        Hidden();
        base.OnInit();
        characterName = Name.RandomName();
        agent.speed = characterScripable.Speed;
        ChangeState(BotState.Idle);
        targetTracked = null;
    }

    public override void Move()
    {
        if (Alive)
        {
            base.Move();
            nextPoint = RandomPoint(Tf.position, 5, 15);
            SetDestination(nextPoint);
        }
    }

    public override void Stop()
    {
        if (Alive)
        {
            base.Stop();
            SetDestination(Tf.position);
        }
    }

    public override void SetTarget(Character taret)
    {
        base.SetTarget(taret);
        ChangeState(BotState.Attack);
    }

    public override void OnDead()
    {
        SetDestination(Tf.position);
        base.OnDead();
    }

    public void ChangeState(BotState botState)
    {
        IState<Bot> state = botStates[botState];
        currentState?.OnExit(this);
        currentState = state;
        currentState.OnEnter(this);
    }

    public bool IsMoving()
    {
        Vector3 positon = Tf.position;
        float distanceX = positon.x - destination.x;
        float distanceZ = positon.z - destination.z;
        return distanceX * distanceX + distanceZ * distanceZ > 0.01f;
    }

    public void MoveToTarget()
    {
        Move();

        if (targetTracked != null && targetTracked.Alive)
        {
            nextPoint = targetTracked.Tf.position;
            SetDestination(nextPoint);
        }
    }

    public bool CheckTargetTracked()
    {
        bool result = false;
        if (!CheckTarget())
        {
            targetTracked = mapManager.GetNearbyCharacter(Tf, rangeFollow);
            result = targetTracked != null && targetTracked.Alive;
        }
        return result;
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        agent.SetDestination(destination);
    }

    private Vector3 RandomPoint(Vector3 center, float min, float max)
    {
        Vector3 result = Vector3.zero;
        float range = UnityEngine.Random.Range(min, max);
        Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, range, NavMesh.AllAreas))
        {
            result = hit.position;
        }
        return result;
    }

    private void SetRandomSkin<T>(Action<T> action) where T : Enum
    {
        T skinType = Extensions.RandomEnum<T>();
        action.Invoke(skinType);
    }
}
