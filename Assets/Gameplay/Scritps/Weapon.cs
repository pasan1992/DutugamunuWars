using UnityEngine;

public class Weapon :Item
{
	private Vector3 m_Position;
	private Vector3 m_Velocity;
	protected AttackSystem m_OwnerAttackSystem;
	public float m_weaponRange;
	private LeftHand m_leftHand;


	void Awake()
	{
		m_leftHand = this.GetComponentInChildren<LeftHand> ();
		m_OwnerAttackSystem = this.GetComponentInParent<AttackSystem>();
	}

	public void equipItem(AttackSystem attackSystem)
	{
		m_OwnerAttackSystem = attackSystem;
		m_OwnerAttackSystem.setWeaponUpdateFunction (weaponUpdate);
		SetPhyicsObjectState (false);
	}

	public override void unEquipItem()
	{
		m_OwnerAttackSystem.removeWeaponUpdateFunction ();
		m_OwnerAttackSystem = null;
		SetPhyicsObjectState (true);
	}
		
	public void weaponUpdate()
	{
		m_Velocity = this.transform.position - m_Position;
		m_Position = this.transform.position;
	}

	public Vector3 getVelocity()
	{
		return m_Velocity;
	}

	public float getWeaponRange()
	{
		return m_weaponRange;
	}

	public Transform getLeftHandPosition()
	{
        //return m_leftHand.transform;
        return this.transform;

	}
}
