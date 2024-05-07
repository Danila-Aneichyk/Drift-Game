using UnityEngine;
using Timer = DriftSystem.Timer;

namespace UI
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private Timer _timer;

        private void Start()
        {
            _timer.TimerExpired += OnTimerExpired;
        }

        private void OnTimerExpired()
        {
            _gameOverPanel.SetActive(true);
        }
    }
}