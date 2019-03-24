using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class MovableCharacter : MonoBehaviour {


    [SerializeField]
    float m_MovingTurnSpeed = 360;
    [SerializeField]
    float m_StationaryTurnSpeed = 180;


    // Movment Parameters
    float m_TurnAmount;
    float m_ForwardAmount;
    bool m_characterMovable;

    Animator m_Animator;
	Rigidbody m_Rigidbody;

    void Awake ()
    {
       	m_Rigidbody = this.GetComponent<Rigidbody>();
	    m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        m_Animator = this.GetComponent<Animator>();
        setCharacterMovable(true);
    }

    public void Move(Vector3 moveDirection)
    {
        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }
        moveDirection = transform.InverseTransformDirection(moveDirection);

        if(m_characterMovable)
        {
            m_TurnAmount = Mathf.Atan2(moveDirection.x, moveDirection.z);
            m_ForwardAmount = moveDirection.z * 0.7f;
            ApplyExtraTurnRotation();
        }
        UpdateAnimator();
    }



    void UpdateAnimator()
    {
        // update the animator parameters
        m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
        m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
    }


    public void OnAnimatorMove()
    {
        // we implement this function to override the default root motion.
        // this allows us to modify the positional speed before it's applied.
        if (Time.deltaTime > 0)
        {
            Vector3 v = (m_Animator.deltaPosition) / Time.deltaTime;
            v.y = m_Rigidbody.velocity.y;
			m_Rigidbody.velocity = v*1.2f;
			this.transform.rotation = Quaternion.Euler(new Vector3(0,this.transform.rotation.eulerAngles.y,0));
        }
    }


    void ApplyExtraTurnRotation()
    {
        // help the character turn faster (this is in addition to root rotation in the animation)
        float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
        transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
    }

    public void setCharacterMovable(bool movable)
    {
        m_characterMovable = movable;
        if(!movable)
        {
            m_ForwardAmount = Mathf.Lerp(m_ForwardAmount, 0, 0.1f);
        }
    }
}
