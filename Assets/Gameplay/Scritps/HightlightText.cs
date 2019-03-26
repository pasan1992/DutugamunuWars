using UnityEngine;
using System.Collections;

public class HightlightText : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseOver()
	{
		GetComponent<TextMesh> ().color = new Color (0, 1, 0);
		GetComponent<TextMesh> ().fontSize = 50;
	}

	void OnMouseExit()
	{
		GetComponent<TextMesh> ().color = new Color (1, 1, 1);
		GetComponent<TextMesh> ().fontSize = 40;
	}
	}


