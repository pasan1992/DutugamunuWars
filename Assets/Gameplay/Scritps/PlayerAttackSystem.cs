using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TargetFinder))]
[RequireComponent(typeof(PlayerController))]
public class PlayerAttackSystem : AttackSystem {

	TargetFinder m_TargetFinder;
	private CharacterUI m_staminaBar;


	public void Awake()
	{
		m_TargetFinder = this.GetComponent<TargetFinder> ();
	}
	 
	protected override void Start()
	{
		base.Start ();
		GameObject characterUI = GameObject.Instantiate (PrefabManager.getInstance ().CharacterUI);
		m_staminaBar = characterUI.GetComponent<CharacterUI> (); 
		m_staminaBar.setMaxValue (getStamina());
		m_staminaBar.setColor(Color.cyan);
	}

	protected override void Update()
	{
		base.Update ();
		m_staminaBar.setPosition (this.transform.position - Vector3.up*0.2f);
	}

	public override void startAttack()
	{

		Transform opponentTransfrom = getCurrentTarget ();
		if (opponentTransfrom != null) 
		{
			EnemyAttackSystem opponentAttakSystem = opponentTransfrom.GetComponent<EnemyAttackSystem> ();
			float distance = Vector3.Distance (this.transform.position, opponentTransfrom.position);

			if (distance < getWeapon().m_weaponRange) 
			{
				if (CharacterState.BlockHit.Equals(opponentAttakSystem.getState ()) || CharacterState.Block.Equals (opponentAttakSystem.getState ())) 
				{
					interuptCommand (opponentTransfrom);
					opponentAttakSystem.blockHitCommand (this.transform);
				} 
				else if (!CharacterState.Roll.Equals(opponentAttakSystem.getState()) &&!CharacterState.Attacked.Equals(opponentAttakSystem.getState()) && !isInterupted() && distance < getWeapon().m_weaponRange && !getAnimator().IsInTransition(0))
				{
					if (CharacterState.Attack.Equals (opponentAttakSystem.getState ())) 
					{
						if (getWeapon().getVelocity ().magnitude > opponentAttakSystem.getWeaponSpeed ()) 
						{
							opponentAttakSystem.getAttacked (this.transform, getAttackType(), null,getWeapon().getVelocity());
							int stamina = getStamina ();
							if (stamina < getMaxStamina()) 
							{
								stamina++;
							}
							setStamina (stamina);
						}
					} 
					else 
					{
						opponentAttakSystem.getAttacked (this.transform, getAttackType(), null,getWeapon().getVelocity());

						int stamina = getStamina ();
						if (stamina < getMaxStamina()) 
						{
							stamina++;
						}
						setStamina (stamina);
					}

				}
			}
		}
	}

	protected  override Transform getCurrentTarget()
	{
		return m_TargetFinder.getCurrentTarget ();
	}


	public override void setStamina(int value)
	{
		base.setStamina (value);
		m_staminaBar.setValue (value);
	}

	protected override void OnCharacterDestroy()
	{
		foreach (EnemyAttackSystem enemey in  m_TargetFinder.getEnemyList())
		{
			enemey.putAwayWeapon ();
		}
		StartCoroutine (waitAndDestroy ());
	}

	public TargetFinder getTaretFinder()
	{
		return m_TargetFinder;
	}
		
	IEnumerator waitAndDestroy()
	{
		yield return new WaitForSeconds (4);
		SceneManager.LoadScene ("All");
	}
}
