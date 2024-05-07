using System;
using UI;
using UnityEngine;

namespace DriftSystem
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TimerScreen _timerScreen;
        
        private float _totalTime = 120f;

        public event Action TimerExpired;

        private void Update()
        {
            _totalTime -= Time.deltaTime;
            float currentTime = _totalTime;

            if (currentTime > 0)
            {
                _timerScreen.UpdateTimerLabel(currentTime);
            }

            if (currentTime <= 0.00001f)
            {
                OnTimerExpired();
            }
        }

        private void OnTimerExpired()
        {
            TimerExpired?.Invoke();
        }
    }
}