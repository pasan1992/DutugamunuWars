using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class nextButton : MonoBehaviour {

    // Use this for initialization
    public Text result;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void OnMouseDown()
	{
		textcontrol.randQuestion = -1;
        result.text = "";
    }
}
