using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void PlayGame()
	{
        GameManager.correctAnswers = 0;

        SceneManager.LoadScene ("Quiz1",LoadSceneMode.Single);
	}

	public void QuitGame ()
	{
		Debug.Log("QUIT");
		Application.Quit ();
	}
}
