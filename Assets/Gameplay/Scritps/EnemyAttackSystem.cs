using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseCharacterController))]
public class EnemyAttackSystem : AttackSystem 
{
	Transform target;
	public bool m_ShieldEquiped;

	
	private int concecativeHits = 0;


	protected override void Start()
	{
		base.Start ();
        BaseCharacterController enemyAi = (BaseCharacterController)m_CharacterController;

        if(enemyAi !=null && enemyAi.getTarget()!= null)
        {
            target = enemyAi.getTarget().transform;
        }

        if(target != null)
        {
            PlayerAttackSystem palyerAttackSystem = target.GetComponent<PlayerAttackSystem>();

            if (palyerAttackSystem != null)
            {
                palyerAttackSystem.getTaretFinder().addToEnemyList(this);
            }
        }

        soundSystem = this.GetComponent<SoundSystem>();
	}

	protected override Transform getCurrentTarget()
	{
		return target;
	}

	public override void startAttack()
	{

		Transform opponentTransfrom = getCurrentTarget ();
		if (opponentTransfrom != null) 
		{
			AttackSystem opponentAttakSystem = opponentTransfrom.GetComponent<AttackSystem> ();
			float distance = Vector3.Distance (this.transform.position, opponentTransfrom.position);

			if (distance < getWeapon().m_weaponRange) 
			{
				if (CharacterState.BlockHit.Equals(opponentAttakSystem.getState ()) || CharacterState.Block.Equals (opponentAttakSystem.getState ())) 
				{
					interuptCommand (opponentTransfrom);
					opponentAttakSystem.blockHitCommand (this.transform);
                    
				} 
				else if (!CharacterState.Attacked.Equals(opponentAttakSystem.getState()) && !isInterupted() && distance < getWeapon().m_weaponRange && !getAnimator().IsInTransition(0))
				{
					if (CharacterState.Attack.Equals (opponentAttakSystem.getState ())) 
					{
                        Debug.Log("here");
                        //if (getWeapon ().getVelocity ().magnitude >= opponentAttakSystem.getWeaponSpeed ())
                        //                  {
                        //	opponentAttakSystem.getAttacked (this.transform, getAttackType (), null, getWeapon ().getVelocity ());
                        //}
                        //                  else
                        //                  {

                        //                  }
                        interuptCommand(opponentTransfrom);
                       
                    }
                    else 
					{
						opponentAttakSystem.getAttacked (this.transform, getAttackType (), null, getWeapon ().getVelocity ());
                        
					}

				}
                else if(distance < getWeapon().m_weaponRange)
                {
                    opponentAttakSystem.getAttacked(this.transform, getAttackType(), null, getWeapon().getVelocity());
                    //soundSystem.playHitSound();
                }
			}
		}
	}

	protected override void OnCharacterDestroy()
	{
		PlayerAttackSystem palyerAttackSystem =  target.GetComponent<PlayerAttackSystem> ();
		if (palyerAttackSystem != null) 
		{
			palyerAttackSystem.getTaretFinder ().removeFromEnemyList (this);
		}
        soundSystem.playFallSound();
		StartCoroutine (waitAndDestroy ());
	}

	
	public override void getAttacked(Transform attacker,int attakcType,Rigidbody attackedPart,Vector3 weaponVelocty)
    {
		base.getAttacked(attacker,attakcType,attackedPart,weaponVelocty);
		addConcecativeHits();
    }

	public override void blockHitCommand(Transform attacker)
    {
		base.blockHitCommand(attacker);
		if(Random.value >0.667f)
		{
			reduceConcetaciteHits();
			if(concecativeHits <=0)
			{
				concecativeHits =0;
				unblock();
			}
		}
		else if(Random.value > 0.931f)
		{
			resetConcecativeHits();
			unblock();
		}
    }

	IEnumerator waitAndDestroy()
	{
		yield return new WaitForSeconds (10);
		Destroy (this.gameObject);
	}

	public bool isShieldEquiped()
	{
		return m_ShieldEquiped;
	}

	public int getConcecetiveHits()
	{
		return concecativeHits;
	}

	private void addConcecativeHits()
	{
		concecativeHits++;
	}

	private void reduceConcetaciteHits()
	{
		concecativeHits--;
	}

	private void resetConcecativeHits()
	{
		concecativeHits = 0;
	}
}
