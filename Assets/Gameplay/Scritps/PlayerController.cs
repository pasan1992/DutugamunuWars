using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour,BaseCharacterController
{
    private Transform m_cam;
    private Vector3 m_camFoward;
    private Vector3 m_moveDiretion;
    private MovableCharacter m_movableCharacter;
    private AttackSystem m_attackSystem;
	private bool movmentControlStopped = false;

    private string horizontalAxis = "Horizontal";
    private string verticalAxis = "Vertical";




    void Awake()
    {
        m_cam = Camera.main.transform;
        m_movableCharacter = this.GetComponent<MovableCharacter>();
        m_attackSystem = this.GetComponent<AttackSystem>();
    }

    private void FixedUpdate()
    {
		if(m_attackSystem.ableToMove() && !movmentControlStopped)
        {
            //float h = CrossPlatformInputManager.GetAxis("Horizontal");
            //float v = CrossPlatformInputManager.GetAxis("Vertical");

            float h = SimpleInput.GetAxis(horizontalAxis);
            float v = SimpleInput.GetAxis(verticalAxis);

            // Remove the y component of m_Cam.forward vector.
            m_camFoward = Vector3.Scale(m_cam.forward, new Vector3(1, 0, 1)).normalized;

            // m_Move direction towards m_CamForward
            m_moveDiretion = v * m_camFoward + h * m_cam.right;
            m_movableCharacter.Move(m_moveDiretion);
        }
        else
		{
            m_movableCharacter.Move(Vector3.zero);
        }

        if (SimpleInput.GetButtonDown("Attack"))
        {
            m_attackSystem.attackCommand();
        }

        if (SimpleInput.GetButtonDown("Block"))
        {
            m_attackSystem.blockCommand();
        }

		if (SimpleInput.GetButtonUp ("Block")) 
		{
			m_attackSystem.unblock ();
		}

		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			m_attackSystem.rollCommand ();
		}

		if (Input.GetKeyDown (KeyCode.LeftShift)) 
		{
			m_attackSystem.specialAttack();
		}

    }



    public void AttackOver()
    {

    }
		
	public void stopMovment(bool state)
	{
		movmentControlStopped = state;
	}

    public AttackSystem getTarget()
    {
        return null;
    }

}
