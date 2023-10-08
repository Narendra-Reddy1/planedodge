using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    [RequireComponent(typeof(Button))]
    public class AudioButton : MonoBehaviour
    {
        [SerializeField] private AudioID m_audioID = AudioID.ButtonClickSFX;
        private Button m_button;
        private void Awake()
        {
            m_button = GetComponent<Button>();
            m_button.onClick.AddListener(_PlayAudio);
        }
        private void OnDestroy()
        {
            m_button.onClick.RemoveListener(_PlayAudio);
        }

        private void _PlayAudio()
        {
            AudioManager.instance.PlaySFX(m_audioID);
        }
    }
