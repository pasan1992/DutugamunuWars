using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class nextButton : MonoBehaviour {

    // Use this for initialization
    public Text result;

    public Text[] questionTexts;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void OnMouseDown()
	{
		textcontrol.randQuestion = -1;
        result.text = "";
        textcontrol.questionSelected = false;

        foreach (Text text in questionTexts)
        {
            text.color = Color.red;
        }
    }
}
