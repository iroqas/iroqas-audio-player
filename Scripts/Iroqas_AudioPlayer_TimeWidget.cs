using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Iroqas.AudioPlayer
{
    public class Iroqas_AudioPlayer_TimeWidget : MonoBehaviour
    {

        public TextMeshProUGUI text;

        private void Awake()
        {
            SetTimeInSeconds(0);
        }


        public void SetTimeInSeconds(float timeInSeconds)
        {
            text.text = SecondsToString(timeInSeconds);
        }

        private string SecondsToString(float timeInSeconds)
        {
            int minutes = ((int)timeInSeconds) / 60;
            int seconds = ((int)timeInSeconds) % 60;
            return (minutes.ToString() + ":" + seconds.ToString("00"));
        }
    }
}

