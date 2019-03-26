using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class textcontrol : MonoBehaviour {

	List<string> questions = new List <string>() {"Who is the first giant warrior?","Who was the chief warrior of King KavanTissa?"," Who's father was Mahanaga? ","Who uprooted all the lmbara trees?","Who is the warrier buddhist monk joined king dutugemunu's army?"};

	List<string> correctAnswer = new List <string>() { "4","1","2","4","3"};

	public Transform resultObj;

	public static string selectedAnswer;

	public static string choiceSelected="n";

	public static int randQuestion=-1;

    public static bool questionSelected = false;

    // Use this for initialization
    private int questionCount = 0;
	void Start () {
		//GetComponent<TextMesh> ().text = questions [0];
	
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(randQuestion == -1)
		{
		randQuestion = Random.Range (0, 5);
		}
		if (randQuestion>-1) 
		{
			GetComponent<Text> ().text = questions [randQuestion];
		}
		//GetComponent<TextMesh> ().text = questions [randQuestion];

		if(choiceSelected == "y")
        {

			choiceSelected = "n";
            questionSelected = true;


            if (correctAnswer [randQuestion] == selectedAnswer)
            {

				resultObj.GetComponent<Text> ().text = "CORRECT! Click Next To Continue";
                questionCount++;
                if (questionCount > 4)
                {
                    //SceneManager.LoadScene("Selection", LoadSceneMode.Single);
                    LevelManager.LoadNextLevelSelectionMenu();
                }
            } 
			else
			{
				resultObj.GetComponent<Text> ().text = "INCORRECT! Click Next To Continue" + "Correct Answer : "+ correctAnswer[randQuestion];

            }

           



        }

	}
}