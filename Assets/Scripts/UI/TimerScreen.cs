using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TimerScreen : BaseScreen
    {
        [SerializeField] private TMP_Text _timerLabel;

        public void UpdateTimerLabel(float time)
        {
            _timerLabel.text = time.ToString("00.00");
        }
    }
}