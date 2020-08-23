using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script represents a house in the game. It will have entryways into the house, 
/// the main christmas tree to place presents under, a stealth meter for how asleep the npcs are, 
/// and the npcs that will chase the player when awake.
/// </summary>
public class House : MonoBehaviour
{
    [SerializeField] private int houseNum = -1;
    private float noiseMeter = -1f; //This determines how asleep the npcs of the house are. If this reaches too high, they wake up!
    public event Action<int, float> OnNoiseMeterChanged = delegate { }; //The event to call whenever the noise meter is changed.
    public event Action OnNoiseMeterFull = delegate { };
    public event Action<int, float> OnShowHouseNoiseMeter = delegate { };
    public event Action<int> OnHideHouseNoiseMeter = delegate { };
    [SerializeField] private GameObject houseTree = null;
    [SerializeField] private GameObject houseNPC = null;
    [SerializeField] private SpriteRenderer wantedItemSprite = null;
    private List<GameObject> entryways = null;

    private bool playerIsNearHouse = false;

    private bool playerIsInHouse = false;

    private void Awake()
    {
        entryways = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            Entryway childType = child.GetComponentInChildren<Entryway>();
            if (childType != null)
            {
                entryways.Add(child);
            }
        }
    }

    private void Start()
    {
        StartCoroutine(UpdateNoiseMeter());
    }

    private IEnumerator UpdateNoiseMeter()
    {
        while (!GameManager.instance.GetGameHasEnded())
        {
            //If player is not in house, slowly lower noise meter.
            if (!playerIsInHouse)
            {
                yield return new WaitForSeconds(1f);
                if (noiseMeter > 0)
                    DecreaseNoise(1f);
            }
            //Else, increase noise if player is moving.
            else
            {
                if (noiseMeter < 100)
                    AddNoise(1 * PlayerMovementController.instance.GetPlayerCurrentMovementSpeed());
            }
            yield return 0;
        }
    }

    public void ShowHouseNoiseMeterUI()
    {
        OnShowHouseNoiseMeter(houseNum, noiseMeter);
    }
    public void HideHouseNoiseMeterUI()
    {
        OnHideHouseNoiseMeter(houseNum);
    }

    public int GetHouseNum()
    {
        return houseNum;
    }

    public float GetNoiseMeter()
    {
        return noiseMeter;
    }
    public void AddNoise(float numNoise)
    {
        float newNoiseValue = noiseMeter + numNoise;
        if (newNoiseValue >= 100)
        {
            noiseMeter = 100;
            OnNoiseMeterFull();
        }
        else
        {
            noiseMeter = newNoiseValue;
        }
        OnNoiseMeterChanged(houseNum, noiseMeter);
        //Debug.Log("Noise Meter: " + noiseMeter);
    }
    public void DecreaseNoise(float numNoise)
    {
        float newNoiseValue = noiseMeter - numNoise;
        if (newNoiseValue <= 0)
        {
            noiseMeter = 0;
        }
        else
        {
            noiseMeter = newNoiseValue;
        }
        OnNoiseMeterChanged(houseNum, noiseMeter);
    }

    public void SetWantedItemSprite(Sprite newSprite)
    {
        wantedItemSprite.sprite = newSprite;
    }

    public bool GetPlayerIsInHouse()
    {
        return playerIsInHouse;
    }
    public void SetPlayerIsInHouse(bool newValue)
    {
        playerIsInHouse = newValue;
        //Debug.Log("Player in house: " + playerIsInHouse);
    }

    public bool GetPlayerIsNearHouse()
    {
        return playerIsNearHouse;
    }
    public void SetPlayerIsNearHouse(bool newValue)
    {
        playerIsNearHouse = newValue;
        //Debug.Log("Player near house: " + playerIsNearHouse);
    }
}
