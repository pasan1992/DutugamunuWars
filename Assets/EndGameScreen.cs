using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameScreen : MonoBehaviour
{
    public GameObject endGameScreen;
    private Text endGameText;
    private bool win = false;

    public void Awake()
    {
        endGameText = endGameScreen.GetComponentInChildren<Text>();
        endGameScreen.SetActive(false);
    }

    public void winEndGameScreen()
    {
        endGameScreen.SetActive(true);
        endGameText.text = "Victory";
        win = true;
    }

    public void looseEndGameScreen()
    {
        endGameScreen.SetActive(true);
        endGameText.text = "Defeat";
        win = false;
    }

    public bool isWin()
    {
        return win;
    }

    public bool isActive()
    {
        return endGameScreen.activeSelf;
    }

    public void restartGame()
    {
        GameSystem.restartLevel = true;
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
