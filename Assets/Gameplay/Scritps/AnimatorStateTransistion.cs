using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStateTransistion : StateMachineBehaviour
{
	protected AttackSystem m_attackSystem;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (layerIndex ==0)
        {
            AttackSystem attackSystem = animator.GetComponent<AttackSystem>();

			if (stateInfo.IsTag ("Idle")) 
			{
				attackSystem.setState (AttackSystem.CharacterState.Idle);
			} 
			else if (stateInfo.IsTag ("Attack")) 
			{
				if (m_attackSystem == null) 
				{
					m_attackSystem = animator.GetComponent<AttackSystem> ();
				}
				attackSystem.setState (AttackSystem.CharacterState.Attack);
			} 
			else if (stateInfo.IsTag ("Block")) 
			{
				attackSystem.setState (AttackSystem.CharacterState.Block);
				//Debug.Log("block state");
			} 
			else if (stateInfo.IsTag ("Roll")) 
			{
				attackSystem.setState (AttackSystem.CharacterState.Roll);
			} else if (stateInfo.IsTag ("Attacked")) {
				attackSystem.setState (AttackSystem.CharacterState.Attacked);
			} 
			else if (stateInfo.IsTag ("BlockHit")) 
			{
				attackSystem.setState (AttackSystem.CharacterState.BlockHit);
			} 
			else if(stateInfo.IsTag ("Sheath") )
			{
				attackSystem.setState (AttackSystem.CharacterState.Sheath);
			}
			else if(stateInfo.IsTag ("SheathIdle") )
			{
				attackSystem.setState (AttackSystem.CharacterState.SheathIdle);
			}
			else if(stateInfo.IsTag ("SpecialAttack") )
			{
				attackSystem.setState (AttackSystem.CharacterState.SpecialAttack);
			}
			else if(stateInfo.IsTag ("KnockBack") )
			{
				attackSystem.setState (AttackSystem.CharacterState.KnockBack);
			}
			else if(stateInfo.IsTag ("Recover") )
			{
				attackSystem.setState (AttackSystem.CharacterState.Recover);
			}
        }
    }
		

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (layerIndex ==0)
		{
			if (stateInfo.IsTag ("Attack") && animator.GetFloat("attackSpeed")<0.1 && stateInfo.normalizedTime <0.01f) 
			{
				animator.SetTrigger("interupt");
				animator.SetFloat("attackSpeed", 0.6f);
			}
		}
	}
	/*
	override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (layerIndex ==0)
		{
			if (stateInfo.IsTag ("Attack") ) 
			{
				animator.SetIKPosition (AvatarIKGoal.LeftHand,Vector3.zero);
				animator.SetIKPositionWeight (AvatarIKGoal.LeftHand, 1f);
			} 
			else 
			{
				animator.SetIKPositionWeight (AvatarIKGoal.LeftHand, 0f);
			}
		}
	}
	*/

}
