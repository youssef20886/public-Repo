using UnityEngine;

[CreateAssetMenu(fileName = "GameAudioSO", menuName = "ScriptableObjects/GameAudioSO", order = 1)]
public class GameAudioSO : ScriptableObject
{
    public AudioClip[] buttonClicks;
    public AudioClip[] cardClicks;
    public AudioClip[] cardDistributeClips;
    public AudioClip[] cardComparisonSuccess;
    public AudioClip[] cardComparisonFail;

    public int comboClipIndex = 0;

    public AudioClip GetAudio(GlobalVariables.AutioType autioType)
    {
        AudioClip clipToPlay = null;
        switch (autioType)
        {
            default:
            case GlobalVariables.AutioType.ButtonClick:
                clipToPlay = GetRandomClip(buttonClicks);
                break;
            case GlobalVariables.AutioType.CardClick:
                clipToPlay = GetRandomClip(cardClicks);
                break;
            case GlobalVariables.AutioType.CardDistribute:
                clipToPlay = GetRandomClip(cardDistributeClips);
                break;
            case GlobalVariables.AutioType.CardComparisonSuccess:
                clipToPlay = GetNextClip(cardComparisonSuccess);
                break;
            case GlobalVariables.AutioType.CardComparisonFail:
                clipToPlay = GetRandomClip(cardComparisonFail);
                break;
        }
        return clipToPlay;
    }

    private AudioClip GetNextClip(AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0)
            return null;

        AudioClip clip = clips[comboClipIndex];

        if (comboClipIndex < clips.Length - 1)
            comboClipIndex++;

        return clip;
    }

    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0)
            return null;

        int index = Random.Range(0, clips.Length);
        return clips[index];
    }
}
