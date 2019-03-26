using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class text1 : MonoBehaviour {

	public List<string> firstchoice = new List<string>() {"Mahasona","Velusumana","Vasabha","Pharakrama","Bharana"};

	// Use this for initialization
	void Start () {
		//GetComponent<TextMesh> ().text = firstchoice [0];
	
	}
	
	// Update is called once per frame
	void Update () {
		if (textcontrol.randQuestion>-1) 
		{
			GetComponent<Text> ().text = firstchoice [textcontrol.randQuestion];
		}
	
	}

    public void Click()
	{
        if(!textcontrol.questionSelected)
        {
            textcontrol.selectedAnswer = gameObject.name;
            textcontrol.choiceSelected = "y";
            this.GetComponent<Text>().color = Color.blue;
        }
	}
}
