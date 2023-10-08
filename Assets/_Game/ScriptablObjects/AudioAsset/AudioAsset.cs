using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "newAudioAsset", menuName ="ScriptableObjects/AudioAsset")]
    public class AudioAsset : ScriptableObject
    {
        [SerializeField] private SerializedDictionary<AudioID, AudioClip> m_audioDatabase;
        public AudioClip GetAudioClipByID(AudioID id) => m_audioDatabase[id];
    }
    public enum AudioID
    {
        MainBGM,
        JetMovementSFX,
        ButtonClickSFX,
        MissileBlastSFX,
        CoinCollectionSFX,
        PowerupCollectionSFX,

    }