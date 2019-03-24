using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollSystem : MonoBehaviour 
{
	private List<Rigidbody> m_ragdollRigidBodies =  new List<Rigidbody>();
	private List<Collider> m_ragdollColliders = new List<Collider>();
	private Rigidbody chest;
	private Rigidbody m_selfMainRigidBody;
	private Collider m_selfManCollider;
	//private bool ragdollEnabled = false;

	public void Awake()
	{
		initalizeRagdoll ();
		enableRagdoll (false);
	}

	private void initalizeRagdoll()
	{
		Rigidbody[] allRigidBodies = this.GetComponentsInChildren<Rigidbody>();
		m_selfMainRigidBody = this.GetComponent<Rigidbody> ();
		m_selfManCollider = this.GetComponent<Collider> ();

		foreach(Rigidbody rb in allRigidBodies)
		{
			if (rb.gameObject.layer == 9) 
			{
				m_ragdollRigidBodies.Add (rb);
				m_ragdollColliders.Add (rb.GetComponent<Collider> ());

				if (rb.tag == "chest") 
				{
					chest = rb;
				}
			}
		}


	}
		
	public void enableRagdoll(bool enabled)
	{
		// Disable/Enable ragdoll colliders
		//ragdollEnabled = enabled;

		m_selfManCollider.enabled = !enabled;
		m_selfMainRigidBody.isKinematic = enabled;

		foreach (Collider col in m_ragdollColliders)
		{
			col.isTrigger = !enabled;
		}

		foreach (Rigidbody rb in m_ragdollRigidBodies)
		{
			if (enabled) 
			{
				rb.interpolation = RigidbodyInterpolation.Interpolate;
				rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
				rb.velocity = Vector3.zero;
			} else 
			{
				rb.interpolation = RigidbodyInterpolation.None;
				rb.collisionDetectionMode = CollisionDetectionMode.Discrete;	
			}
			rb.isKinematic = !enabled;
		}
	}

	public void commandStrikeDown(Item[] items)
	{
		StartCoroutine (strikeDown (items));
	}

	private IEnumerator strikeDown(Item[] items)
	{
		float ragdollDragValue = 0;

		while(ragdollDragValue <=10)
		{
			ragdollDragValue +=2;
			foreach (Rigidbody rb in m_ragdollRigidBodies) 
			{
				rb.drag = ragdollDragValue;
				rb.angularDrag = ragdollDragValue;
			}
			yield return null;	
		}

		while(ragdollDragValue >=2)
		{
			ragdollDragValue -=2;
			foreach (Rigidbody rb in m_ragdollRigidBodies) 
			{
				rb.drag = ragdollDragValue;
				rb.angularDrag = ragdollDragValue;
			}
			yield return null;	
		}
			
		foreach (Item weapon in items) 
		{
			weapon.unEquipItem ();
		}
	}

	public void applyForce(Vector3 forceDirection)
	{
		chest.AddForce (forceDirection*300, ForceMode.Impulse);
	}
}
