using Ads;
using UnityEngine;
using UnityEngine.UI;
using Timer = DriftSystem.Timer;

namespace UI
{
    public class GameOverScreen : BaseScreen
    {
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private Button _adButton;
        [SerializeField] private Timer _timer;
        [SerializeField] private InitializeAds _initializeAds;

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