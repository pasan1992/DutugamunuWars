using UnityEngine;

public class Item : MonoBehaviour 
{
	public GameObject m_itemPhysicsObject;

	public void SetPhyicsObjectState(bool state)
	{
		m_itemPhysicsObject.SetActive (state);

		if (state) 
		{
			m_itemPhysicsObject.transform.parent = null;
			this.transform.parent = m_itemPhysicsObject.transform;
		} 
		else 
		{
			m_itemPhysicsObject.transform.parent = this.transform;
			m_itemPhysicsObject.transform.localPosition = Vector3.zero;
		}
	}

	public virtual void equipItem()
	{
		SetPhyicsObjectState (false);
	}

	public virtual void unEquipItem()
	{
		SetPhyicsObjectState (true);
	}

}
