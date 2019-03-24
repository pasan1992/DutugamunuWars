using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class text3 : MonoBehaviour {

	public List<string> thirdchoice = new List<string>() {"Abhaya","PanduwasaDewa","Vijaya","Mutasiwa","Theraputtabhaya"};
	// Use this for initialization
	void Start () {
		//GetComponent<TextMesh> ().text = thirdchoice  [0];
	
	}
	
	// Update is called once per frame
	void Update () {
		if (textcontrol.randQuestion>-1) 
		{
			GetComponent<Text> ().text = thirdchoice [textcontrol.randQuestion];
		}
	
	}

    public void Click()
	{
		textcontrol.selectedAnswer = gameObject.name;
		textcontrol.choiceSelected = "y";

	}
}
