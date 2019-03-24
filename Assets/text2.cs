using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class text2 : MonoBehaviour {

	public List<string> secondchoice = new List<string>() {"Elara","Dutugemunu","Gotaimbara","Vijayabhahu","Wikramasinghe"};

	// Use this for initialization
	void Start () {
	
		//GetComponent<TextMesh> ().text = secondchoice  [0];
	}
	
	// Update is called once per frame
	void Update () {
		if (textcontrol.randQuestion > -1) 
		{
			GetComponent<Text> ().text = secondchoice [textcontrol.randQuestion];
		}
	
	}

    public void Click()
	{
		textcontrol.selectedAnswer = gameObject.name;
		textcontrol.choiceSelected = "y";

	}
}
