using TMPro;
using UnityEngine;

namespace UI
{
    public class DriftScoreScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _scorePanel;
        [SerializeField] private TMP_Text _totalScoreText;
        [SerializeField] private TMP_Text _currentScoreText;
        [SerializeField] private TMP_Text _multiplierText;
        [SerializeField] private TMP_Text _driftAngleText;

        public Color NormalDriftColor;
        public Color NearStopColor;
        public Color DriftEndedColor;

        private void Start()
        {
            _scorePanel.SetActive(false);
        }

        public void UpdateTotalScore(float totalScore)
        {
            _totalScoreText.text = "Total: " + totalScore.ToString("###,###,000");
        }

        public void UpdateCurrentScore(float currentScore)
        {
            _currentScoreText.text = currentScore.ToString("###,###,000");
        }

        public void UpdateDriftMultiplier(float driftFactor)
        {
            _multiplierText.text = driftFactor.ToString("###,###,##0.0") + "X";
        }

        public void UpdateDriftAngle(float driftAngle)
        {
            _driftAngleText.text = driftAngle.ToString("###,##0") + "°";
        }

        public void SetCurrentScoreColor(Color color)
        {
            _currentScoreText.color = color;
        }

        public void DisableEnableScorePanel(bool activate)
        {
            _scorePanel.SetActive(activate);
        }
    }
}