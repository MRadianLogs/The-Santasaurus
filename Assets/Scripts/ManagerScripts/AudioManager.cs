using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource backgroundMusicAudioSource = null;
    [SerializeField] private AudioSource playerSoundEffectsAudioSource = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists! Destroying object!");
            Destroy(transform.root.gameObject);
        }
    }

    private void Start()
    {
        if(GameManager.instance != null)
        {
            GameManager.instance.OnGameStarted += HandleGameStarted;
            GameManager.instance.OnHandleNoiseMeterFull += HandleNoiseMeterFull;
            GameManager.instance.OnGameEnded += HandleGameEnded;
        }
        if(HouseManager.instance != null)
        {
            foreach (GameObject houseObject in HouseManager.instance.GetItemList().Values)
            {
                House houseScript = houseObject.GetComponentInChildren<House>();
                foreach (GameObject entrywayObject in houseScript.GetEntryways())
                {
                    Entryway entrywayScript = entrywayObject.GetComponentInChildren<Entryway>();
                    entrywayScript.OnEntrywayOpened += HandleEntrywayOpened;
                    entrywayScript.OnEntrywayClosed += HandleEntrywayClosed;
                }
            }
        }
        if(PlayerInventoryController.instance != null)
        {
            PlayerInventoryController.instance.OnItemPickedUp += HandleItemPickedUp;
            PlayerInventoryController.instance.OnItemDropped += HandleItemDropped;
        }
    }

    private void PlayAudioClip(AudioSource soundSource, AudioClip clipToPlay)
    {
        if(soundSource.isPlaying)
        {
            soundSource.Stop();
        }
        soundSource.clip = clipToPlay;
        soundSource.Play();
    }

    private void HandleGameStarted()
    {

    }

    private void HandleNoiseMeterFull()
    {

    }

    private void HandleGameEnded()
    {

    }

    private void HandleEntrywayOpened()//Need to know entryway audio source?
    {

    }

    private void HandleEntrywayClosed()
    {

    }

    private void HandleItemPickedUp()//Need to know item audio source?
    {

    }
    private void HandleItemDropped()
    {

    }
}
