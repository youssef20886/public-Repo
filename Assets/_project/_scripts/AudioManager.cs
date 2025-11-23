using System;
using UnityEngine;

public class AudioManager : SingletonMonobehaviour<AudioManager>
{
    [SerializeField] private GameAudioSO _gameAudioSO;
    private AudioSource _audioSource;
    private float _lastPlayTime = 0f;
    private float _cooldown = 0.05f;

    protected override void Awake()
    {
        base.Awake();

        _audioSource = GetComponent<AudioSource>();
    }


    public void PlayAudio(GlobalVariables.AutioType autioType)
    {
        if (Time.time - _lastPlayTime < _cooldown) return;
        _lastPlayTime = Time.time;

        _audioSource.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
        _audioSource.PlayOneShot(_gameAudioSO.GetAudio(autioType));
    }

    public void ResetComboAudio()
    {
        _gameAudioSO.comboClipIndex = 0;
    }
}
