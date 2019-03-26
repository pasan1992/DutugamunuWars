using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public Question[]questions;
	private static List<Question> unansQuestions;

	private Question currentQuestion;

	[SerializeField]
	private Text factText;

	[SerializeField]
	private float timeBetweenQuestions = 1f;

	[SerializeField]
	private Text trueAnswerText;

	[SerializeField]
	private Text falseAnswerText;

	[SerializeField]
	private Animator animator;

    public static int correctAnswers = 0;

	void Start ()
	{
		if (unansQuestions == null || unansQuestions.Count == 0 ) 
		{
			unansQuestions = questions.ToList<Question>() ;
		}


		SetCurrentQuestion ();

	}

	void SetCurrentQuestion  ()
	{
		int randomQuestionIndex = Random.Range (0, unansQuestions.Count);
		currentQuestion = unansQuestions [randomQuestionIndex];

		factText.text = currentQuestion.fact;

		if (currentQuestion.isTrue) {
		
			trueAnswerText.text = "CORRECT!";
			falseAnswerText.text = "WRONG!";


        } else {
		
			trueAnswerText.text = "WRONG!";
			falseAnswerText.text = "CORRECT!";

		}

	}

	IEnumerator TransitionToNextQuestion ()
	{
		unansQuestions.Remove(currentQuestion);

		yield return new WaitForSeconds (timeBetweenQuestions); 

		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);


	}



	public void UserSelectTrue ()
	{
		animator.SetTrigger ("True");
		if (currentQuestion.isTrue) {
			Debug.Log ("CORRECT");

            correctAnswers++;

            if (correctAnswers > 4)
            {
                // SceneManager.LoadScene("Quiz2", LoadSceneMode.Single);
                LevelManager.LoadNextLevelSelectionMenu();
            }
        } else
		{
			Debug.Log ("WRONG");
		}
	
	  StartCoroutine(TransitionToNextQuestion());
	}

	public void UserSelectFalse()
	{
		animator.SetTrigger ("False");
		if (!currentQuestion.isTrue) {
			Debug.Log ("CORRECT");

            correctAnswers++;

            if (correctAnswers > 4)
            {
                // SceneManager.LoadScene("Quiz2", LoadSceneMode.Single);
                LevelManager.LoadNextLevelSelectionMenu();
            }
        } else
		{
			Debug.Log ("WRONG");
		}
		StartCoroutine(TransitionToNextQuestion());
	}




}
