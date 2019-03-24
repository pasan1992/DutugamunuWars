using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPaperSissor : MonoBehaviour
{
    public enum WEAPONTYPE { shield, sword, sphere }
    public enum CHARACTERTYPE { DUTUGAMUNU, ELARA, YODAYA, SEBALA}
    public WEAPONTYPE WeaponType;
    public CHARACTERTYPE CharacterType;
    public RockPaperSissor enemy;
    public RuntimeAnimatorController[] characterAnimators;
    public GameObject longSword;
    public GameObject shortSword;
    public GameObject shied;
    public GameObject sphere;
    public GameObject[] characterModels;

    private EnemyAttackSystem attackSystem;
    private EnemyAIController AIController;
    private GameSystem gameSystem;
    private string Name = null;
    private TextMesh nameText;



    void Awake()
    {
        attackSystem = this.GetComponent<EnemyAttackSystem>();
        attackSystem.setOnDestroy(onCharacterDestory);
        AIController = this.GetComponentInChildren<EnemyAIController>();
        gameSystem = GameSystem.FindObjectOfType<GameSystem>();
        initalizeHero();
        nameText = this.GetComponentInChildren<TextMesh>();
        if(nameText !=null && name !=null && CharacterType.Equals(CHARACTERTYPE.YODAYA))
        {
            nameText.text = name;
        }
    }

    public void initalizeHero()
    {
        // Set hero parameters.
        if (enemy != null)
        {
            if(AIController != null)
            {
                AIController.enemyTarget = enemy.GetComponent<EnemyAttackSystem>();
            }


            switch (enemy.WeaponType)
            {
                case WEAPONTYPE.shield:

                    if (WeaponType.Equals(WEAPONTYPE.shield))
                    {
                        attackSystem.setHealth(5);
                        AIController.skill = 0.1f;
                    }
                    else if (WeaponType.Equals(WEAPONTYPE.sword))
                    {
                        attackSystem.setHealth(15);
                        AIController.skill = 1f;
                    }
                    else if (WeaponType.Equals(WEAPONTYPE.sphere))
                    {
                        attackSystem.setHealth(5);
                        AIController.skill = 0.1f;
                    }

                    break;
                case WEAPONTYPE.sphere:

                    if (WeaponType.Equals(WEAPONTYPE.shield))
                    {
                        attackSystem.setHealth(15);
                        AIController.skill = 1f;
                    }
                    else if (WeaponType.Equals(WEAPONTYPE.sword))
                    {
                        attackSystem.setHealth(5);
                        AIController.skill = 0.1f;
                    }
                    else if (WeaponType.Equals(WEAPONTYPE.sphere))
                    {
                        attackSystem.setHealth(5);
                        AIController.skill = 0.1f;
                    }

                    break;
                case WEAPONTYPE.sword:


                    if (WeaponType.Equals(WEAPONTYPE.shield))
                    {
                        attackSystem.setHealth(5);
                        AIController.skill = 0.1f;
                    }
                    else if (WeaponType.Equals(WEAPONTYPE.sword))
                    {
                        attackSystem.setHealth(5);
                        AIController.skill = 0.1f;
                    }
                    else if (WeaponType.Equals(WEAPONTYPE.sphere))
                    {
                        attackSystem.setHealth(15);
                        AIController.skill = 1f;
                    }

                    break;
            }
        }

        // Select correct animator.
        setAnimators();
    }

    public void setAnimators()
    {
        this.GetComponent<Animator>().runtimeAnimatorController = characterAnimators[(int)WeaponType];

        // Disable all weapons
        shortSword.SetActive(false);
        longSword.SetActive(false);
        shied.SetActive(false);
        sphere.SetActive(false);

        // Equip selected weapon. (enable selected weapon)
        switch (WeaponType)
        {
            case WEAPONTYPE.shield:
                shortSword.SetActive(true);
                shied.SetActive(true);
                break;
            case WEAPONTYPE.sword:
                longSword.SetActive(true);
                break;
            case WEAPONTYPE.sphere:
                sphere.SetActive(true);
                break;
        }

        // Disable all character models.
        foreach (GameObject character in characterModels)
        {
            character.SetActive(false);
        }

        // Enable selected character.
        characterModels[(int)CharacterType].SetActive(true);
    }

    public void onCharacterDestory()
    {
        if(CharacterType.Equals(CHARACTERTYPE.SEBALA) || CharacterType.Equals(CHARACTERTYPE.ELARA))
        {
            if(gameSystem != null)
            {
                gameSystem.onEnemyFall();
            }
        }
        else
        {
            if(gameSystem != null)
            {
                gameSystem.onHeroFall();
            }
        }
    }

    public void setName(string name)
    {
        this.name = name;
    }
}
