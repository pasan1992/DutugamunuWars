using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class text4 : MonoBehaviour {

	public List<string> fourthchoice = new List<string>()  {"Nandhimitra","Uththiya","Mahasiwa","Saddhathiss","Bhahiya"};

	// Use this for initialization
	void Start () {
	
		//GetComponent<TextMesh> ().text = fourthchoice  [0];
	}
	
	// Update is called once per frame
	void Update () {
		if (textcontrol.randQuestion > -1) 
		{
			GetComponent<Text> ().text = fourthchoice [textcontrol.randQuestion];
		}
	
	}

    public void Click()
	{
		textcontrol.selectedAnswer = gameObject.name;
		textcontrol.choiceSelected = "y";

	}
}
