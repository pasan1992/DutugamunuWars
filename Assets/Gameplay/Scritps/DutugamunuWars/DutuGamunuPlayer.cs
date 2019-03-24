using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DutuGamunuPlayer : MonoBehaviour, BaseCharacterController
{
    // Use this for initialization
    public AttackSystem enemyTarget;
    NavMeshAgent m_Agent;
    MovableCharacter m_MovableCharacter;
    EnemyAttackSystem m_AttackSystem;
    TargetFinder m_TargetFinder;

    float timeTillNextAttack = 0;
    bool tridedToBlock = false;
    bool isDead = false;

    private float attackWaitTIme;
    private float blockCounter;
    private bool isBlocking;
    private bool combatMode = false;

    // temp
    public float skill;
    public bool enableAI = true;

    public EndGameScreen endGameScreen;

    void Awake()
    {
        m_Agent = this.GetComponent<NavMeshAgent>();
        m_MovableCharacter = this.GetComponent<MovableCharacter>();
        m_AttackSystem = this.GetComponent<EnemyAttackSystem>();
        m_AttackSystem.setOnDestroy(OnSelfFall);
        m_Agent.updatePosition = true;
        if (enemyTarget != null)
        {
            m_Agent.SetDestination(enemyTarget.transform.position);
            enemyTarget.GetComponent<EnemyAttackSystem>().setOnDestroy(OnEnemeyFall);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (enableAI && !isDead && enemyTarget != null && enemyTarget.isFunctional())
        {
            m_MovableCharacter.Move(m_Agent.desiredVelocity);
            if (enemyTarget != null)
            {
                m_Agent.SetDestination(enemyTarget.transform.position);
            }


            if (Vector3.Distance(this.transform.position, enemyTarget.transform.position) < 2.5f)
            {
                m_Agent.isStopped = true;
                combatMode = true;
            }
            else if (m_Agent.remainingDistance >= 2.5 && !isDead && !AttackSystem.CharacterState.Recover.Equals(m_AttackSystem.getState()) && !AttackSystem.CharacterState.KnockBack.Equals(m_AttackSystem.getState()))
            {
                m_AttackSystem.unblock();
                m_Agent.isStopped = false;
                combatMode = false;
            }
        }

    }

    public void Attack()
    {
        m_AttackSystem.attackCommand();
    }

    public void Block()
    {
        m_AttackSystem.blockCommand();
    }

    public void AttackOver()
    {
        // no reason;
    }

    public void stopMovment(bool state)
    {
        m_Agent.isStopped = state;
        m_Agent.velocity = Vector3.zero;
        isDead = state;
        m_Agent.enabled = !state;
    }

    public AttackSystem getTarget()
    {
        return enemyTarget;
    }

    public void OnEnemeyFall()
    {
     StartCoroutine(waitAndEnd(true));
    }

    public void OnSelfFall()
    {
        Debug.Log("fall");
        StartCoroutine( waitAndEnd(false));
    }

    IEnumerator waitAndEnd(bool win)
    {
        yield return new WaitForSeconds(2);

        if (win)
        {
            endGameScreen.winEndGameScreen();
        }
        else
        {
            endGameScreen.looseEndGameScreen();
        }
    }
}
