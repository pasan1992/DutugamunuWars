using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{

    #region armySelection Screen Parameters

    private GameObject helpImage;
    private GameObject[] showcaseCharacters;

    private int currentHeroIndex = 0;
    private HeroButton[] heroButtons;
    private GameObject confirmButton;
    private SoundSystem soundSystem;

    public RockPaperSissor[] heroModels;
    public RockPaperSissor[] enemyModels;
    public bool ArmySelectionScreen = false;

    public struct BattleData
    {
       public RockPaperSissor.WEAPONTYPE opponentWeapon;
       public GameData.GameCharacter hero;
    }

    private string[] ChacaterNames =
    {
        "Nandhimithra",
        "Suranimala",
        "Gotaimbara",
        "Theraputthabhya",
        "Mahabharana",
        "Velusumanna",
        "Khanjadeva",
        "Phussadeva",
        "Labhiyavasabha",
        "Mahasona"
    };
    #endregion


    #region war scene

    private int enemeisDefeted = 0;

    #endregion

    // Data static in all levels.
    private static GameData gameData = null;
    private static int[] enemyOrder = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    private System.Random _random = new System.Random();
    public static bool restartLevel = true;
    private static int level = 0;

    // Data Changed in every level.
    private static Dictionary<int, BattleData> challengersMap = null;
    private EndGameScreen endGameScreen;

    public bool developerMode = false;
    #region initalize
    public void Awake()
    {
        #region Army Selection Screen
        if (restartLevel)
        {
            restartLevel = false;
            resetAllStaticData();
        }

        if (ArmySelectionScreen)
        {
            initalizeArmySelectionScreen();
        }
        #endregion

        #region battle field



        #endregion

        endGameScreen = GameObject.FindObjectOfType<EndGameScreen>();
    }

    public void Start()
    {
        if(ArmySelectionScreen)
        {
            confirmButton.SetActive(false);
            removeUsedHeroButtons();
            EnableAllUnselectedButtons(true);
        }

        if (challengersMap != null && !ArmySelectionScreen)
        {
            prepareArmy();
        }

        if(developerMode)
        {
            Time.timeScale = 4;
        }

    }
    #endregion

    #region UI
    private void initalizeUI()
    {
        helpImage = GameObject.FindGameObjectWithTag("Help");
        showcaseCharacters = GameObject.FindGameObjectsWithTag("ShowCaseCharacters");
        helpImage.SetActive(false);
        confirmButton = GameObject.FindGameObjectWithTag("Confirm");
        heroButtons = GameObject.FindObjectsOfType<HeroButton>();
    }

    private void removeUsedHeroButtons()
    {
        foreach (string characterName in gameData.getAllHeros().Keys)
        {
            if (gameData.getCharacter(characterName).used)
            {
                foreach (HeroButton heroButton in heroButtons)
                {
                    if (heroButton.name.Equals(characterName))
                    {
                        heroButton.setHeroUsed(true);
                    }
                }
            }

        }
    }

    public void enableHelp()
    {
        helpImage.SetActive(true);
        enableShowCaseCharacters(false);
        soundSystem.playHitSound();
    }
    
    public void disableHelp()
    {
        helpImage.SetActive(false);
        enableShowCaseCharacters(true);
        soundSystem.playHitSound();
    }

    public void enableShowCaseCharacters(bool enable)
    {
        foreach (GameObject character in showcaseCharacters)
        {
            character.SetActive(enable);
        }
    }
    #endregion

    #region Select Heros

    private void initalizeArmySelectionScreen()
    {
        soundSystem = this.GetComponent<SoundSystem>();

        initalizeUI();

        genarateEnemyList();

        intializeSelectHeros();
    }

    private void intializeSelectHeros()
    {
        foreach (RockPaperSissor model in heroModels)
        {
            model.gameObject.SetActive(false);
            model.CharacterType = RockPaperSissor.CHARACTERTYPE.YODAYA;
        }

        int i = 0;
        foreach (RockPaperSissor model in enemyModels)
        {
            model.gameObject.SetActive(true);
            model.CharacterType = RockPaperSissor.CHARACTERTYPE.SEBALA;
            model.WeaponType = challengersMap[i].opponentWeapon;
            model.initalizeHero();
            i++;
        }



    }

    public void selectHero(string heroName,bool select)
    {
        if(select)
        {
            heroModels[currentHeroIndex].gameObject.SetActive(true);
            heroModels[currentHeroIndex].WeaponType = gameData.getSelectedCharacterWeaponType(heroName);
            heroModels[currentHeroIndex].initalizeHero();

            // Update seleted hero
            BattleData updatedBattleData = new BattleData { opponentWeapon = challengersMap[currentHeroIndex].opponentWeapon, hero = gameData.getCharacter(heroName) };
            challengersMap[currentHeroIndex] = updatedBattleData;

            currentHeroIndex++;
            soundSystem.playSlathSound();
        }
        else
        {
            currentHeroIndex--;
            heroModels[currentHeroIndex].gameObject.SetActive(false);
            soundSystem.playFallSound();
        }

        if(currentHeroIndex >=heroModels.Length)
        {
            EnableAllUnselectedButtons(false);

            confirmButton.SetActive(true);
        }

        if(currentHeroIndex <heroModels.Length)
        {
            EnableAllUnselectedButtons(true);
            confirmButton.SetActive(false);
        }
    }

    private void EnableAllUnselectedButtons(bool enable)
    {
        foreach(HeroButton button in heroButtons)
        {
            if(!button.isSelected())
            {
                if(enable && !button.getHeroUsed())
                {
                    button.enableButton();
                }
                else
                {
                    button.disableButton();
                }
            }
        }
    }

    public void finalizeSelectHeros()
    {
        //level++;
        loadWarScene();
        ArmySelectionScreen = false;
    }

    private void genarateEnemyList()
    {
        int currentIndex = level * 3;
        challengersMap = new Dictionary<int, BattleData>();

        for (int i = 0; i < 3; i++)
        {
            string characterName = ChacaterNames[enemyOrder[i + currentIndex]];
            BattleData battleData = new BattleData { opponentWeapon = getOpponentWeapon(gameData.getSelectedCharacterWeaponType(characterName)), hero = gameData.getCharacter(characterName) };
            challengersMap.Add(i,battleData);
        }
    }

    private RockPaperSissor.WEAPONTYPE getOpponentWeapon(RockPaperSissor.WEAPONTYPE heroWeapon)
    {
        switch (heroWeapon)
        {
            case RockPaperSissor.WEAPONTYPE.shield:
                return RockPaperSissor.WEAPONTYPE.sphere;
            case RockPaperSissor.WEAPONTYPE.sphere:
                return RockPaperSissor.WEAPONTYPE.sword;
            case RockPaperSissor.WEAPONTYPE.sword:
                return RockPaperSissor.WEAPONTYPE.shield;
        }
        return RockPaperSissor.WEAPONTYPE.sword;
    }



    #endregion

    #region Reset All Logic in Army Selection Screen

    private void resetAllStaticData()
    {
        gameData = new GameData();

        if(!developerMode)
        {
            Shuffle(enemyOrder);
        }

        level = 0;
    }

    private void Shuffle(int[] array)
    {
        int p = array.Length;
        for (int n = p - 1; n > 0; n--)
        {
            int r = _random.Next(1, n);
            int t = array[r];
            array[r] = array[n];
            array[n] = t;
        }
    }

    #endregion

    #region battleField

    private void prepareArmy()
    {
        for(int j= 0;j<3;j++)
        {
            heroModels[j].CharacterType = RockPaperSissor.CHARACTERTYPE.YODAYA;
            heroModels[j].WeaponType = challengersMap[j].hero.weapon;
            heroModels[j].setName(challengersMap[j].hero.name);
            enemyModels[j].CharacterType = RockPaperSissor.CHARACTERTYPE.SEBALA;
            enemyModels[j].WeaponType = challengersMap[j].opponentWeapon;

            heroModels[j].gameObject.SetActive(true);
            enemyModels[j].gameObject.SetActive(true);
        }
    }

    private void winWar()
    {
        level++;

        // Is Final level
        if(level >=3)
        {
            //loadFinalWar();
            LevelManager.LoadNextLevelSelectionMenu();
        }
        // Is Next war level
        else
        {
            // Set selected characters used so they cannot be used again.
            for (int i = 0; i < 3; i++)
            {
                gameData.setCharacterUsed(challengersMap[i].hero.name);
            }

            ArmySelectionScreen = true;

            // Load selection screen
            loadSelection();
        }


    }

    private void looseWar()
    {
        ArmySelectionScreen = true;
        loadSelection();
    }

    public void onEnemyFall()
    {
        enemeisDefeted++;

        if(enemeisDefeted >=3)
        {
            StartCoroutine(waitAndEnd(true));
        }
    }

    public void onHeroFall()
    {
        if(!endGameScreen.isActive())
        {
            StartCoroutine(waitAndEnd(false));
        }

    }

    public void endWar()
    {
          if(endGameScreen.isWin())
          {
            winWar();
          }
          else
          {
            looseWar();
          }
    }

    IEnumerator waitAndEnd(bool win)
    {
        yield return new WaitForSeconds(2);

        if(win)
        {
            endGameScreen.winEndGameScreen();
        }
        else
        {
            endGameScreen.looseEndGameScreen();
        }
    }

    #endregion

    #region common

    private void loadWarScene()
    {
        SceneManager.LoadScene("War", LoadSceneMode.Single);
    }

    private void loadSelection()
    {
        SceneManager.LoadScene("Selection", LoadSceneMode.Single);
    }

    private void loadFinalWar()
    {
        SceneManager.LoadScene("FinalWar", LoadSceneMode.Single);
    }

    #endregion
}
