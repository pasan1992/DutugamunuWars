using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour {

	// Use this for initialization
	public Image healthBar;

	private float maxHelath;

	public void setValue(float value)
	{
		healthBar.fillAmount = value/maxHelath;
	}

	public void setMaxValue(float value)
	{
		maxHelath = value;
	}

	public void setPosition(Vector3 position)
	{
		this.transform.position = position + Vector3.up*2;		
		this.transform.rotation = Quaternion.Euler (0, 90, 0);
	}

	public void setColor(Color color)
	{
		healthBar.color = color;
	}
}
