using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioAsset _audioAsset;
    [SerializeField] private AudioSource _bgmAudioSource;
    [SerializeField] private AudioSource _sfxSource;

    #region SINGLETON
    public static AudioManager instance { get; private set; }
    #endregion

    private void Awake()
    {
        instance = this;
        _Init();
    }
    private void _Init()
    {
        _bgmAudioSource.loop = true;
        _bgmAudioSource.volume = .2f;
    }
    public void PlaySFX(AudioID audioID)
    {
        _sfxSource.PlayOneShot(_audioAsset.GetAudioClipByID(audioID));
    }

    public void PlayBGM(AudioID audioID)
    {
        _bgmAudioSource.clip = _audioAsset.GetAudioClipByID(audioID);
        _bgmAudioSource.Play();
    }
}