using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseNoiseMeterUIController : MonoBehaviour
{
    private bool UIIsActive = false;
    private int currentHouseNum = -1;

    [SerializeField] GameObject NoiseMeterUI = null;
    [SerializeField] Slider noiseMeterSlider = null;

    private void Start()
    {
        GameManager.instance.OnGameEnded += HandleOnGameEnded;
        foreach (GameObject house in HouseManager.instance.GetItemList().Values)
        {
            House houseScript = house.GetComponentInChildren<House>();
            houseScript.OnShowHouseNoiseMeter += HandleShowHouseNoiseMeterUI;
            houseScript.OnHideHouseNoiseMeter += HandleHideHouseNoiseMeterUI;
            houseScript.OnNoiseMeterChanged += HandleNoiseMeterChanged;
        }
    }

    private void HandleOnGameEnded()
    {
        if(UIIsActive)
        {
            HideUI();
        }
    }

    private void HandleShowHouseNoiseMeterUI(int newHouseNum, float houseCurrentNoiseValue)
    {
        //Change house num.
        currentHouseNum = newHouseNum;
        //Change UI.
        SetNoiseMeterValue(houseCurrentNoiseValue);
        //Show UI.
        ShowUI();
    }

    private void HandleHideHouseNoiseMeterUI(int houseNum)
    {
        if(currentHouseNum == houseNum)
        {
            //Hide UI.
            HideUI();
        }
    }

    private void ShowUI()
    {
        //Show UI.
        NoiseMeterUI.SetActive(true);
        UIIsActive = true;
    }

    private void HideUI()
    {
        //Hide UI.
        NoiseMeterUI.SetActive(false);
        UIIsActive = false;
    }

    private void HandleNoiseMeterChanged(int houseNum, float newNoiseValue)
    {
        if(currentHouseNum == houseNum && UIIsActive)
        {
            //Change UI.
            SetNoiseMeterValue(newNoiseValue);
        }
    }

    private void SetNoiseMeterValue(float newValue)
    {
        noiseMeterSlider.value = newValue;
    }
}
