using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource backgroundMusicAudioSource = null;
    [SerializeField] private AudioSource gameSoundEffectsAudioSource = null;
    [SerializeField] private AudioSource playerSoundEffectsAudioSource = null;

    //TODO: Create audioClip manager to store and retrieve all audio clips by string or method?
    [SerializeField] private AudioClip gameStartedClip = null;
    [SerializeField] private AudioClip gameEndedClip = null;
    [SerializeField] private AudioClip noiseMeterFullClip = null;
    [SerializeField] private AudioClip timeRanOutClip = null;

    [SerializeField] private AudioClip entrywayOpenedClip = null;
    [SerializeField] private AudioClip entrywayClosedClip = null;

    [SerializeField] private AudioClip itemPickedUpClip = null;
    [SerializeField] private AudioClip itemDroppedClip = null;

    private Dictionary<int, AudioSource> entrywayAudioSources = null;
    private List<AudioSource> pausedAudioSources = null; //Used to store what audio sources are paused when the game is paused.

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

        entrywayAudioSources = new Dictionary<int, AudioSource>();
        pausedAudioSources = new List<AudioSource>();
    }

    private void Start()
    {
        if(GameManager.instance != null)
        {
            //GameManager.instance.OnGameStarted += HandleGameStarted;
            GameManager.instance.OnHandleNoiseMeterFull += HandleNoiseMeterFull;
            GameManager.instance.OnTimeRanOut += HandleTimeRanOut;
            //GameManager.instance.OnGameEnded += HandleGameEnded;
        }
        if(EntrywayManager.instance != null)
        {
            foreach (GameObject entrywayObject in EntrywayManager.instance.GetItemList().Values)
            {
                Entryway entrywayScript = entrywayObject.GetComponentInChildren<Entryway>();
                entrywayAudioSources.Add(entrywayScript.GetEntrywayNum(), entrywayObject.GetComponentInChildren<AudioSource>());
                entrywayScript.OnEntrywayOpened += HandleEntrywayOpened;
                entrywayScript.OnEntrywayClosed += HandleEntrywayClosed;
            }
            
        }
        if(PlayerInventoryController.instance != null)
        {
            PlayerInventoryController.instance.OnItemPickedUp += HandleItemPickedUp;
            PlayerInventoryController.instance.OnItemDropped += HandleItemDropped;
        }
        if(PauseController.instance != null)
        {
            PauseController.instance.OnGamePaused += HandleGamePaused;
            PauseController.instance.OnGameResumed += HandleGameResumed;
        }
    }

    private void PlayAudioClip(AudioSource soundSource, AudioClip clipToPlay)
    {
        if (soundSource != null && clipToPlay != null)
        {
            if (soundSource.isPlaying)
            {
                soundSource.Stop(); //TODO: Consider not stopping the audio, if it seems fluid.
            }
            soundSource.clip = clipToPlay;
            soundSource.Play();
        }
    }

    private void HandleGamePaused()
    {
        //See what audio is playing. Pause it. Add to pausedAudioList.
        if(backgroundMusicAudioSource != null && backgroundMusicAudioSource.isPlaying)
        {
            backgroundMusicAudioSource.Pause();
            pausedAudioSources.Add(backgroundMusicAudioSource);
        }
        if(gameSoundEffectsAudioSource != null && gameSoundEffectsAudioSource.isPlaying)
        {
            gameSoundEffectsAudioSource.Pause();
            pausedAudioSources.Add(gameSoundEffectsAudioSource);
        }
        if(playerSoundEffectsAudioSource != null && playerSoundEffectsAudioSource.isPlaying)
        {
            playerSoundEffectsAudioSource.Pause();
            pausedAudioSources.Add(playerSoundEffectsAudioSource);
        }
        foreach (AudioSource source in entrywayAudioSources.Values)
        {
            if(source != null && source.isPlaying)
            {
                source.Pause();
                pausedAudioSources.Add(source);
            }
        }
    }
    private void HandleGameResumed()
    {
        //Go through paused audio list. Resume it.
        foreach (AudioSource source in pausedAudioSources)
        {
            if(source != null)
                source.Play();
        }
        pausedAudioSources.Clear();
    }

    private void HandleGameStarted()
    {
        PlayAudioClip(gameSoundEffectsAudioSource, gameStartedClip);
    }

    private void HandleNoiseMeterFull()
    {
        PlayAudioClip(gameSoundEffectsAudioSource, noiseMeterFullClip);
    }

    private void HandleTimeRanOut()
    {
        PlayAudioClip(gameSoundEffectsAudioSource, timeRanOutClip);
    }

    private void HandleGameEnded()
    {
        PlayAudioClip(gameSoundEffectsAudioSource, gameEndedClip);
    }

    private void HandleEntrywayOpened(int entrywayNum)
    {
        PlayAudioClip(entrywayAudioSources[entrywayNum], entrywayOpenedClip);
    }

    private void HandleEntrywayClosed(int entrywayNum)
    {
        PlayAudioClip(entrywayAudioSources[entrywayNum], entrywayClosedClip);
    }

    private void HandleItemPickedUp()
    {
        PlayAudioClip(gameSoundEffectsAudioSource, itemPickedUpClip);
    }
    private void HandleItemDropped()
    {
        PlayAudioClip(gameSoundEffectsAudioSource, itemDroppedClip);
    }
}
