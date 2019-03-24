using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackSystem : MonoBehaviour {

	public delegate void updateDeligate();


	// Animation
    Animator m_animator;
	RagdollSystem m_RagdollSystem;
	MovableCharacter m_MovableCharacter;
	protected BaseCharacterController m_CharacterController;

    public CharacterState m_state = CharacterState.Idle;

	// weapon parameters
	private Weapon m_weapon;
	private Item m_leftHandItem;
	private updateDeligate m_WeaponUpdateFunction;
	private float m_handIKWeight;
    public SoundSystem soundSystem;

	// combat system related parameters

	private bool m_recivingDamage = false;
	private int m_attackType =0;
	private bool m_character_over = false;

	public int health =4;
	private CharacterUI m_healthBar;
	private static float SPECIAL_ATTACK_DISTANCE = 5;
	private int m_stamina = 1000;
	private int m_maxStamina =1000;

    public delegate void OnDestroy();
    private OnDestroy onDestory;

    public enum CharacterState
    {
		Idle,Attack,Block,Roll,Attacked,BlockHit,Interupt,SpecialAttack,Sheath,SheathIdle,KnockBack,Recover
    };
	protected virtual void Start ()
    {
		initaializeParameters();
    }

    protected virtual void Awake()
    {
        soundSystem = this.GetComponent<SoundSystem>();
    }

	private void initaializeParameters()
	{
		m_animator = GetComponent<Animator>();
		m_RagdollSystem = GetComponent<RagdollSystem> ();
		m_weapon = GetComponentInChildren<Weapon> ();
		m_leftHandItem = GetComponentInChildren<Item> ();
		m_MovableCharacter = GetComponent<MovableCharacter> ();
		m_CharacterController = this.GetComponent<BaseCharacterController> ();

		// Initalize Character UI;
        if(PrefabManager.getInstance() !=null)
        {
            GameObject characterUI = GameObject.Instantiate(PrefabManager.getInstance().CharacterUI);
            m_healthBar = characterUI.GetComponent<CharacterUI>();
            m_healthBar.setMaxValue(health);
        }

		if (m_weapon != null) 
		{
			m_weapon.equipItem(this);
			LeftHand WeaponLeftHand = m_weapon.GetComponentInChildren<LeftHand> ();
		}
	}

    public void equipWeapon()
    {
        m_weapon = GetComponentInChildren<Weapon>();
        if (m_weapon != null)
        {
            m_weapon.equipItem(this);
            LeftHand WeaponLeftHand = m_weapon.GetComponentInChildren<LeftHand>();
        }
    }
    
    public void attackCommand()
    {
		if (!isInterupted()) 
		{
			m_animator.SetFloat("attackSpeed",1);
			m_animator.SetBool ("block", false);
			m_animator.SetTrigger("attack");
			m_attackType++;
			if(m_attackType >6)
			{
				m_attackType = 1;
			}

            //soundSystem.playSlathSound();
		}

    }

	public void interuptCommand(Transform attacker)
    {
		//m_animator.SetTrigger("interupt");
		m_animator.SetFloat("attackSpeed",-0.6f);
		setState (CharacterState.Interupt);
		this.transform.LookAt(attacker);
        soundSystem.playBlockSound();
    }

    public void blockCommand(Transform attacker)
    {
		m_animator.SetBool("block",true);
        this.transform.LookAt(attacker);
    }

	public void blockCommand()
    {
		m_animator.SetBool("block",true);
    }

	public void unblock()
	{
		m_animator.SetBool ("block", false);
	}

    public void rollCommand()
    {
        m_animator.SetTrigger("roll");
    }

	public void specialAttack()
	{
		m_animator.SetTrigger ("specialAttack");
		//this.GetComponent<Rigidbody> ().AddForce (this.transform.forward*10, ForceMode.Impulse);
	}

    public void setState(CharacterState state)
    {
        this.m_state = state;
        updatStateTransision(state);
    }

    public CharacterState getState()
    {
        return m_state;
    }

    public bool ableToMove()
    {
		return m_state.Equals(CharacterState.Idle) ||m_state.Equals(CharacterState.SheathIdle);
    }

    private void updatStateTransision( CharacterState state)
    {
        switch (state)
        {
			case CharacterState.Attack:
				m_animator.ResetTrigger ("attack");
				m_animator.SetFloat ("attackType", m_attackType);
				lookAtTarget ();
				m_recivingDamage = false;
	            break;
	        case CharacterState.Block:
	            //m_animator.ResetTrigger("block");
				m_animator.ResetTrigger("blockReact");
	            lookAtTarget();
	            break;
	        case CharacterState.Roll:
	            m_animator.ResetTrigger("roll");
	            break;
			case CharacterState.Idle:
				m_animator.ResetTrigger ("attacked");
				m_animator.ResetTrigger ("blockReact");
				m_recivingDamage = false;
	            break;
	        case CharacterState.Attacked:
	            m_animator.ResetTrigger("attacked");
	            break;
		 	case CharacterState.BlockHit:
	            m_animator.ResetTrigger("blockReact");
	            break;
			case CharacterState.Interupt:
				//m_animator.ResetTrigger ("block");
				break;
			case CharacterState.SpecialAttack:
			Debug.Log ("kick2");
				break;
			case CharacterState.Sheath:
				break;
			case CharacterState.SheathIdle:
				break;
		    case CharacterState.KnockBack:
			this.GetComponent<Rigidbody> ().AddForce (-this.transform.forward*50, ForceMode.VelocityChange);
			     break;
		case CharacterState.Recover:
				break;
        }
    }

	public virtual void getAttacked(Transform attacker,int attakcType,Rigidbody attackedPart,Vector3 weaponVelocty)
    {
		if(!m_state.Equals(CharacterState.Attacked) && !m_recivingDamage)
        {
            m_animator.SetFloat("attackedType", attakcType);
            m_animator.SetTrigger("attacked");
			this.transform.LookAt(attacker);
			getDamaged (attackedPart,weaponVelocty);
			m_recivingDamage = true;
            
        }
    }

	public void GetSPecialAttack(Transform attacker)
	{
		m_animator.SetTrigger("knockBack");
		this.transform.LookAt(attacker);
	}



    public int getAttackType()
    {
        return m_attackType;
    }

    public void setBlockId(int id)
    {
        m_animator.SetFloat("blockType", id);
    }

	public virtual void blockHitCommand(Transform attacker)
    {
        m_animator.SetTrigger("blockReact");
        m_animator.SetFloat("attackSpeed",5);
		this.transform.LookAt(attacker);
        soundSystem.playBlockSound();
    }

    public bool isInterupted()
    {
		return CharacterState.Interupt.Equals (m_state);
    }

    public float getWeaponDamage()
    {
        return m_animator.GetFloat("damage");
    }

	protected virtual void Update()
	{
		if (m_healthBar != null) 
		{
			m_healthBar.setPosition (this.transform.position);	
		}

		if (m_WeaponUpdateFunction != null) 
		{
			m_WeaponUpdateFunction();			
		}

	}

	public void setEnableAttakSystem(bool enabled)
	{
		m_animator.enabled = enabled;
	}

	private void getDamaged(Rigidbody attackedPart,Vector3 weaponVelocity)
	{
		health = health - 1;
		m_healthBar.setValue (health);
		if (health <= 0) 
		{
			if (!m_character_over) 
			{
				getDestroyed (attackedPart, weaponVelocity);
				m_character_over = true;
				OnCharacterDestroy ();

                //Call eventHandler
                onDestory();
                soundSystem.playFallSound();
			} 
		}
        else
        {
            soundSystem.playHitSound();
        }
	}

	protected virtual void OnCharacterDestroy ()
	{
		
	}
		
	public void SetRecivingDamage(bool state)
	{
		m_recivingDamage = state;
	}

	private void getDestroyed(Rigidbody attackedPart,Vector3 weaponVelocity)
	{
		if (m_CharacterController != null) 
		{
			//m_CharacterController.stopMovment (true);
		}

		if (m_RagdollSystem != null) 
		{
		//	m_RagdollSystem.enableRagdoll (true);
		//	m_RagdollSystem.commandStrikeDown (new Item[]{ (Item)m_weapon, (Item)m_leftHandItem });
		//	m_RagdollSystem.applyForce (weaponVelocity);
		}

		if (m_MovableCharacter != null) 
		{
			//m_MovableCharacter.setCharacterMovable (false);
		}

		m_animator.SetTrigger("fall");
		//setEnableAttakSystem (false);
		//weaponVelocity = weaponVelocity.normalized;


		//attackedPart.AddForce (weaponVelocity*100, ForceMode.Impulse);
	}
	 		
	public Animator getAnimator()
	{
		return m_animator;
	}

	public void setWeaponUpdateFunction(updateDeligate updateFunction)
	{
		m_WeaponUpdateFunction = updateFunction;
	}

	public void removeWeaponUpdateFunction()
	{
		m_WeaponUpdateFunction = null;
	}

	public virtual void startAttack()
	{

		Transform opponentTransfrom = getCurrentTarget ();
		if (opponentTransfrom != null) 
		{
			AttackSystem opponentAttakSystem = opponentTransfrom.GetComponent<AttackSystem> ();
			float distance = Vector3.Distance (this.transform.position, opponentTransfrom.position);

			if (distance < m_weapon.m_weaponRange) 
			{
				if (CharacterState.BlockHit.Equals(opponentAttakSystem.getState ()) || CharacterState.Block.Equals (opponentAttakSystem.getState ())) 
				{
					interuptCommand (opponentTransfrom);
					opponentAttakSystem.blockHitCommand (this.transform);
				} 
				else if (!CharacterState.Attacked.Equals(opponentAttakSystem.getState()) && !isInterupted() && distance < m_weapon.m_weaponRange && !m_animator.IsInTransition(0))
				{
					if (CharacterState.Attack.Equals (opponentAttakSystem.getState ())) 
					{
						if (m_weapon.getVelocity ().magnitude > opponentAttakSystem.getWeaponSpeed ()) 
						{
							opponentAttakSystem.getAttacked (this.transform, m_attackType, null,m_weapon.getVelocity());
						}
					} 
					else if(CharacterState.Idle.Equals((opponentAttakSystem.getState ())))
					{
						opponentAttakSystem.blockHitCommand(this.transform);
							interuptCommand (opponentTransfrom);
					}

				}
			}
		}
	}

	public void onSPecialAttack()
	{
		Transform opponentTransfrom = getCurrentTarget ();
		this.transform.LookAt (opponentTransfrom);
		if (opponentTransfrom != null) 
		{
			AttackSystem opponentAttakSystem = opponentTransfrom.GetComponent<AttackSystem> ();
			float distance = Vector3.Distance (this.transform.position, opponentTransfrom.position);

			if (distance < SPECIAL_ATTACK_DISTANCE) 
			{
				//opponentTransfrom.GetComponent<Rigidbody> ().AddForce (this.transform.forward*10, ForceMode.Acceleration);
				opponentAttakSystem.GetSPecialAttack (this.transform);
			}
		}
	}

	protected virtual Transform getCurrentTarget ()
	{
		return FindObjectOfType<EnemyAttackSystem> ().transform;
	}

	protected virtual void lookAtTarget ()
	{
		this.transform.LookAt (getCurrentTarget ());
	}
	/*
	public Vector3 getLeftHandPostion()
	{
		return m_weapon.getLeftHandPosition ();
	}
	*/
	void OnAnimatorIK()
	{
		if (CharacterState.BlockHit.Equals(m_state) || CharacterState.Block.Equals(m_state) || CharacterState.Attack.Equals(m_state) || CharacterState.Idle.Equals(m_state)) 
		{
			

			if (m_handIKWeight < 1) 
			{
				m_handIKWeight += Time.deltaTime/10;
			}

			m_animator.SetIKPosition (AvatarIKGoal.LeftHand,m_weapon.getLeftHandPosition().position);
			m_animator.SetIKPositionWeight (AvatarIKGoal.LeftHand, m_handIKWeight);

			m_animator.SetIKRotation (AvatarIKGoal.LeftHand, m_weapon.getLeftHandPosition().rotation);
			m_animator.SetIKRotationWeight (AvatarIKGoal.LeftHand, m_handIKWeight);
		} 
		else 
		{
			if (m_handIKWeight > 0) 
			{
				m_handIKWeight -= Time.deltaTime;
			}
			m_animator.SetIKPositionWeight (AvatarIKGoal.LeftHand,m_handIKWeight);
			m_animator.SetIKRotationWeight (AvatarIKGoal.LeftHand,m_handIKWeight);
		}

		//m_animator.SetIKRotation (AvatarIKGoal.RightHand, tempRotationObject.rotation);
		//m_animator.SetIKRotationWeight (AvatarIKGoal.RightHand, 1);
	}

	public bool isBlocking()
	{
		return CharacterState.Block.Equals (m_state);
	}

	public float getWeaponSpeed()
	{
		return m_weapon.getVelocity ().magnitude;
	}

	public bool isFunctional()
	{
		return !m_character_over;
	}

	public void putAwayWeapon()
	{
		m_animator.SetTrigger ("sheath");
	}

	public void onSwordSheath()
	{
		m_weapon.gameObject.SetActive (false);
	}

	public Weapon getWeapon()
	{
		return m_weapon;
	}

	public int getStamina()
	{
		return m_stamina;
	}
	public virtual void setStamina(int value)
	{
		m_stamina = value;
	}

	public int getMaxStamina()
	{
		return m_maxStamina;
	}

    public void setHealth(int healthValue)
    {
        this.health = healthValue;
        GameObject characterUI = GameObject.Instantiate(PrefabManager.getInstance().CharacterUI);
        m_healthBar = characterUI.GetComponent<CharacterUI>();
        m_healthBar.setMaxValue(healthValue);
    }

    public void setOnDestroy(OnDestroy function)
    {
        onDestory += function;
    }
}
