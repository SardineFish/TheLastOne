using UnityEngine;
using System.Collections;
using MonsterLove.StateMachine;
using System.Linq;

public enum EnemyState
{
    Idle,
    Wander,
    Chase,
    Attack
}
public class EnemyAI : EntityBehavior<LifeBody>
{
    public float VisualRange = 10;
    public float VisualHight = 0.5f;
    public float AttackRange = 0.5f;
    public Entity AttackTarget;
    StateMachine<EnemyState> fsm;
    EntityController controller;
    Vector2 wanderDirection;
    // Use this for initialization
    void Start()
    {
        controller = Entity.GetComponent<EntityController>();
        fsm = StateMachine<EnemyState>.Initialize(this);
        fsm.ChangeState(EnemyState.Idle);
    }

    Coroutine startWanderCoroutine;
    Coroutine endWanderCoroutine;
    void Idle_Enter()
    {
        controller.Move(new Vector2(0, 0));
        if (startWanderCoroutine!=null)
            StopCoroutine(startWanderCoroutine);
        startWanderCoroutine = StartCoroutine(StartWander());
    }

    void Idle_Update()
    {
        AttackTarget = CheckVision();
        if(AttackTarget)
        {
            fsm.ChangeState(EnemyState.Chase);
            return;
        }
    }

    void Idle_Exit()
    {
        StopCoroutine(startWanderCoroutine);
    }
    IEnumerator StartWander()
    {
        yield return new WaitForSeconds(Mathf.Lerp(2, 5, Random.value));
        fsm.ChangeState(EnemyState.Wander);
    }
    void Wander_Enter()
    {
        wanderDirection = Random.insideUnitCircle;
        endWanderCoroutine = StartCoroutine(EndWander());
    }
    void Wander_Exit()
    {
        StopCoroutine(endWanderCoroutine);
    }
    void Wander_Update()
    {
        AttackTarget = CheckVision();
        if (AttackTarget)
        {
            fsm.ChangeState(EnemyState.Chase);
            return;
        }

        Entity.GetComponent<EntityController>().Move(wanderDirection);
    }
    IEnumerator EndWander()
    {
        yield return new WaitForSeconds(Mathf.Lerp(2, 5, Random.value));
        fsm.ChangeState(EnemyState.Idle);
    }

    void Chase_Update()
    {
        if(!CheckVision())
        {
            fsm.ChangeState(EnemyState.Idle);
            AttackTarget = null;
            return;
        }
        if ((AttackTarget.transform.position - Entity.transform.position).magnitude < AttackRange)
        {
            fsm.ChangeState(EnemyState.Attack);
            return;
        }
        Entity.GetComponent<EntityController>().Move((AttackTarget.transform.position - Entity.transform.position).ToVector2XZ());
    }

    void Attack_Update()
    {
        if((AttackTarget.transform.position - Entity.transform.position).magnitude > AttackRange)
        {
            fsm.ChangeState(EnemyState.Chase);
            return;
        }
        Entity.GetComponent<EntityController>().TurnTo((AttackTarget.transform.position - Entity.transform.position).ToVector2XZ());
        Entity.GetComponent<SkillController>().ActivateSkill(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Entity CheckVision()
    {
        return GameObject.FindGameObjectsWithTag("Player")
            .Where(player => (player.transform.position - Entity.transform.position).magnitude < VisualRange)
            .Where(player => !Physics.Raycast(new Ray(Entity.transform.position + new Vector3(0, VisualHight, 0), player.transform.position - Entity.transform.position), VisualRange, 1 << 12))
            .FirstOrDefault()?.GetComponent<Player>();
    }
}
