using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword :Weapon
{
	/*
    void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.layer == 9) 
		{
			AttackSystem attacked = other.GetComponentInParent<AttackSystem>();

			if (attacked != null) 
			{
				bool enemyOnEnemy = attacked.GetType() == typeof(EnemyAttackSystem) && m_OwnerAttackSystem.GetType() == typeof(EnemyAttackSystem);

				if (!enemyOnEnemy) 
				{
					string attackedName = attacked.name;
					if(!attackedName.Equals(m_OwnerAttackSystem.gameObject.name))
					{
						AttackSystem.CharacterState attakerState = m_OwnerAttackSystem.getState();
						AttackSystem.CharacterState attackedState = attacked.getState();

						if(AttackSystem.CharacterState.Attack.Equals(attakerState) && !m_OwnerAttackSystem.isInterupted())
						{

							if (AttackSystem.CharacterState.Block.Equals(attackedState))
							{
								m_OwnerAttackSystem.interuptCommand(attacked.transform);
								attacked.blockHitCommand(m_OwnerAttackSystem.transform);
							}
							else if(!AttackSystem.CharacterState.Attacked.Equals(attackedState))
							{
								int attackNumber = m_OwnerAttackSystem.getAttackType();
								attacked.getAttacked(m_OwnerAttackSystem.transform, attackNumber,other.GetComponent<Rigidbody>(),getVelocity());
							}
						}
					}
				}

			}

		}

    }*/
}
