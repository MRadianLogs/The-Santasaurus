using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerUIController : MonoBehaviour
{
    [SerializeField] private Text timerUIText = null;

    private void Start()
    {
        GameManager.instance.OnTimeLeftValueChanged += HandleTimeLeftValueChanged;
    }

    private void HandleTimeLeftValueChanged(float newTimeValue)
    {
        timerUIText.text = newTimeValue.ToString();
    }
}
