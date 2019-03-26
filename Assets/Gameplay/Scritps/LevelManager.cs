using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static int currentLevel = 0;
    public Text levelText;

    public void Start()
    {
        levelText.text = "Level " + currentLevel.ToString();
    }

    public static void LoadNextLevelSelectionMenu()
    {
        currentLevel++;
        SceneManager.LoadScene("levelScreen", LoadSceneMode.Single);
    }

    public void LoadCurrentLevel()
    {
        switch (currentLevel)
        {
            case 1:
            case 0:
                SceneManager.LoadScene("Quiz1", LoadSceneMode.Single);
                break;
            case 2:
                SceneManager.LoadScene("Quiz2", LoadSceneMode.Single);
                break;
            case 3:
                SceneManager.LoadScene("Selection", LoadSceneMode.Single);
                break;
            case 4:
                SceneManager.LoadScene("FinalWar", LoadSceneMode.Single);
                break;
        }
    }

    public static void resetLevels()
    {
        currentLevel = 0;
    }
}
