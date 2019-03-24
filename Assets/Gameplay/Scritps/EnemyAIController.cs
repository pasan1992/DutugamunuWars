using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIController : MonoBehaviour ,BaseCharacterController {

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

	// temp
	public float skill;
    public bool enableAI  = true;

	void Awake ()
    {
        m_Agent = this.GetComponent<NavMeshAgent>();
        m_MovableCharacter = this.GetComponent<MovableCharacter>();
        m_AttackSystem = this.GetComponent<EnemyAttackSystem>();
		m_Agent.updatePosition = true;
		if (enemyTarget != null) 
		{
			m_Agent.SetDestination(enemyTarget.transform.position);
		}

    }
	
	// Update is called once per frame
	void Update ()
    {
		if ( enableAI && !isDead  && enemyTarget != null && enemyTarget.isFunctional()) 
		{
			m_MovableCharacter.Move (m_Agent.desiredVelocity);
            if(enemyTarget !=null)
            {
                m_Agent.SetDestination(enemyTarget.transform.position);
            }


			if (Vector3.Distance (this.transform.position, enemyTarget.transform.position) < 2.5f) 
			{
				m_Agent.isStopped = true;
				CombatMode ();
			} 
			else if (m_Agent.remainingDistance >= 2.5 && !isDead && !AttackSystem.CharacterState.Recover.Equals(m_AttackSystem.getState()) && !AttackSystem.CharacterState.KnockBack.Equals(m_AttackSystem.getState())) 
			{
				m_AttackSystem.unblock ();
				m_Agent.isStopped = false;
			}
		} 

	}

    public void CombatMode()
    {
		if( (m_AttackSystem.getState().Equals(AttackSystem.CharacterState.Block) || m_AttackSystem.getState().Equals(AttackSystem.CharacterState.Idle)) && Random.value >0.6 && timeTillNextAttack >attackWaitTIme)
        {
			float distance = Vector3.Distance (enemyTarget.transform.position, this.transform.position);
			if ( (enemyTarget.isBlocking () && distance <= 2.5f) || distance < 1.5f && skill > Random.value) 
			{
				//m_AttackSystem.kickCommand ();
				m_AttackSystem.attackCommand();
				m_AttackSystem.unblock ();
				attackWaitTIme = Random.value*3;
			} 
			else 
			{
				m_AttackSystem.attackCommand();
				m_AttackSystem.unblock ();
				attackWaitTIme = Random.value*3;
			}

            timeTillNextAttack = 0;
        }

        if(enemyTarget.getState().Equals(AttackSystem.CharacterState.Attack))
        {
			
			if (Random.value < skill && !tridedToBlock) 
			{          
				m_AttackSystem.blockCommand ();
                isBlocking = true;
			} else 
			{
				//m_AttackSystem.unblock ();
			}
            tridedToBlock = true;
        }
        else
        {
            tridedToBlock = false;
            //m_AttackSystem.resetConcecativeHits();
        }

        timeTillNextAttack += Time.deltaTime;
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
}
