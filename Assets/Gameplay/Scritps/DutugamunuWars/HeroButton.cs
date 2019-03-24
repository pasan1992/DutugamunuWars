using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroButton : MonoBehaviour
{
    private Toggle button;
    private Image buttonImage;
    private GameSystem gameSystem;

    public Sprite[] images;
    private bool heroUsed = false;

    void Awake()
    {
        button = this.GetComponent<Toggle>();
        buttonImage = this.GetComponent<Image>();
        gameSystem = GameObject.FindObjectOfType<GameSystem>();
        button.onValueChanged.AddListener(onButtonToggle);
    }
    
    public void disableButton()
    {
        button.interactable = false;
        buttonImage.sprite = images[2];
    }

    public void enableButton()
    {
        button.interactable = true;
        if (button.isOn)
        {
            buttonImage.sprite = images[1];
        }
        else
        {
            buttonImage.sprite = images[0];
        }
    }

    public void onButtonToggle(bool on)
    {
        gameSystem.selectHero(this.name, on);

        if(on)
        {
            buttonImage.sprite = images[1];
        }
        else
        {
            buttonImage.sprite = images[0];
        }
    }

    public bool isSelected()
    {
        return button.isOn;
    }

    public void setHeroUsed(bool used)
    {
        heroUsed = used;
    }

    public bool getHeroUsed()
    {
        return heroUsed;
    }

}
