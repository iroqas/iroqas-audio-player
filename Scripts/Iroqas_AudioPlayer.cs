using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Iroqas.AudioPlayer
{
    /// <summary>
    /// Implements a simple Audio Player Controller.
    ///
    /// v1.0b
    /// Iroqas - 01/09/2022
    /// </summary>
    public class Iroqas_AudioPlayer : MonoBehaviour
    {
        private bool isPlaying;
        private bool userIsInteracting;



        [Header("Media")]
        public AudioClip audioClip;

        [Header("Settings")]
        public float volume = 1;
        public string titleText = "Title not set";

        [Header("Style")]

        public Sprite playButtonGraphic;
        public Sprite pauseButtonGraphic;

        [Header("UI Elements")]

        public TextMeshProUGUI title;
        public AudioSource audioSource;
        public Slider slider;
        public Iroqas_AudioPlayer_TimeWidget timeWidget;
        public Button playButton;





        private void Start()
        {
            InitAudioSource();
            InitPlayPauseButton();
            InitSlider();
            InitTimeWidget();
            InitTitle();
        }


        // Update is called once per frame
        void Update()
        {

            UpdateAudioSource();
            UpdatePlayPauseButton();
            UpdateSlider();
            UpdateTimeWidget();


        }




        #region Init
        private void InitAudioSource()
        {
            audioSource.time = 0;
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            audioSource.clip = audioClip;
            audioSource.volume = this.volume;
        }

        private void InitPlayPauseButton()
        {
            if (playButton == null) throw new NullReferenceException("Play/Pause button not setted");
            playButton.onClick.AddListener(TogglePlayPause);
        }

        private void InitSlider()
        {
            if (slider == null) throw new NullReferenceException("Slider not setted");

            slider.minValue = 0;
            slider.maxValue = GetAudioLength();
        }

        private void InitTimeWidget()
        {
            if (timeWidget == null) throw new NullReferenceException("TimeWidget button not setted");
            timeWidget.SetTimeInSeconds(GetAudioTime());
        }

        private void InitTitle()
        {
            title.text = titleText;
        }

        #endregion


        #region Update

        private void UpdateAudioSource()
        {
            if (audioSource.isPlaying)
            {
                this.isPlaying = true;
            }
            else
            {
                this.isPlaying = false;
            }

            if (userIsInteracting)
            {
                float audioTime = GetSliderTime();
                SetAudioTime(audioTime);
            }
        }

        private void UpdatePlayPauseButton()
        {
            if (isPlaying)
            {
                playButton.image.sprite = pauseButtonGraphic;
            }
            else
            {
                playButton.image.sprite = playButtonGraphic;
            }
        }

        private void UpdateSlider()
        {
            if (isPlaying || (!isPlaying && !userIsInteracting))
            {
                float audioTime = GetAudioTime();
                SetSliderTime(audioTime);
            }

        }

        private void UpdateTimeWidget()
        {
            float audioTime = GetAudioTime();
            timeWidget.SetTimeInSeconds(audioTime);
        }


        #endregion

        #region public methods

        public void Play(bool fromBegging = false)
        {
            if (fromBegging)
            {
                SetAudioTime(0);
            }

            audioSource.Play();
        }

        public void Pause()
        {
            audioSource.Pause();
        }

        public void TogglePlayPause()
        {
            if (audioSource.isPlaying)
            {
                Pause();
            }

            else
            {
                Play();
            }
        }

        public bool IsPlaying()
        {
            return this.isPlaying;
        }

        public void SetTitle(string title)
        {
            titleText = title;
            this.title.text = titleText;
        }

        #endregion



        #region Private getters/setters

        private float GetAudioTime()
        {
            return audioSource.time;
        }

        private float GetAudioLength()
        {
            return audioClip.length;
        }

        private void SetAudioTime(float timeInSeconds)
        {
            audioSource.time = timeInSeconds;
        }



        private float GetSliderTime()
        {
            return slider.value;
        }

        private void SetSliderTime(float timeInSeconds)
        {
            slider.value = timeInSeconds;
        }



        #endregion






        #region UI Event Handlers
        bool wasPlaying = false;

        public void OnSliderPointerDownHandler()
        {
            userIsInteracting = true;
            if (isPlaying) wasPlaying = true;
            Pause();

        }


        public void OnSliderPointerUp()
        {
            userIsInteracting = false;
            if (wasPlaying)
            {
                wasPlaying = false;
                Play();
            }
        }

        public void OnSliderDragHandler()
        {
            if (isPlaying) wasPlaying = true;
            Pause();
        }


        #endregion
    }

}

