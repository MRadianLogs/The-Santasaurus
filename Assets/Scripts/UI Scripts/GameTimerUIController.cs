using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerUIController : MonoBehaviour
{
    [SerializeField] private GameObject timer = null;
    [SerializeField] private Text timerUIText = null;
    private bool timerUIIsActive = false;

    private void Start()
    {
        GameManager.instance.OnTimeLeftValueChanged += HandleTimeLeftValueChanged;
        GameManager.instance.OnGameStarted += HandleOnGameStarted;
        GameManager.instance.OnGameEnded += HandleOnGameEnded;
    }

    private void HandleTimeLeftValueChanged(float newTimeValue)
    {
        timerUIText.text = newTimeValue.ToString();
    }

    private void HandleOnGameStarted()
    {
        if(!timerUIIsActive)
        {
            ShowTimerUI();
        }
    }

    private void HandleOnGameEnded()
    {
        if(timerUIIsActive)
        {
            HideTimerUI();
        }
    }

    public void ShowTimerUI()
    {
        timer.SetActive(true);
        timerUIIsActive = true;
    }
    public void HideTimerUI()
    {
        timer.SetActive(false);
        timerUIIsActive = false;
    }
}
