using System;
using UnityEngine;

public class AudioManager : SingletonMonobehaviour<AudioManager>
{
    [SerializeField] private GameAudioSO gameAudioSO;
    private AudioSource _audioSource;

    protected override void Awake()
    {
        base.Awake();

        _audioSource = GetComponent<AudioSource>();
    }


    public void PlayAudio(GlobalVariables.AutioType autioType)
    {
        _audioSource.PlayOneShot(gameAudioSO.GetAudio(autioType));

    }
}
