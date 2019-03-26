using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void PlayGame()
	{
        //SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
        LevelManager.LoadNextLevelSelectionMenu();
	}

	public void QuitGame ()
	{
		Debug.Log("QUIT");
		Application.Quit ();
	}
}
