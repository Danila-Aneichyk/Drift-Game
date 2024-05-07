using System.Collections;
using System.Threading.Tasks;
using UI;
using UnityEngine;

namespace DriftSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class DriftDetector : MonoBehaviour
    {
        [SerializeField] private DriftScoreScreen _driftScoreScreen;
        [SerializeField] private Timer _timer; 

        private float _minimumSpeed = 5;
        private float _minimumAngle = 10;
        private float _driftingDelay = 0.2f;

        private Rigidbody _rb;

        private float _speed;
        private float _driftAngle;
        private float _driftMultiplier = 1;
        private float _currentScore;
        private float _totalScore;

        private bool _isDrifting = false;

        private IEnumerator _stopDriftingCoroutine;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _timer.TimerExpired += OnTimerExpired;
        }

        private void Update()
        {
            TrackDrift();
            ActualizeUI();
        }

        private void TrackDrift()
        {
            CalculateSpeedAndDriftAngle();
            HandleDriftStart();
            HandleDriftStop();
            UpdateDriftScore();
        }

        private void OnTimerExpired()
        {
            enabled = false;
            _driftScoreScreen.DisableEnableScorePanel(false);
        }

        private void CalculateSpeedAndDriftAngle()
        {
            _speed = _rb.velocity.magnitude;
            _driftAngle = Vector3.Angle(_rb.transform.forward, (_rb.velocity + _rb.transform.forward).normalized);

            if (_driftAngle > 120)
            {
                _driftAngle = 0;
            }
        }

        private void HandleDriftStart()
        {
            if (IsMinimumDriftConditionsReached())
            {
                if (IsDriftingInCurrentMoment())
                {
                    StartDrift();
                }
            }
        }

        private bool IsDriftingInCurrentMoment()
        {
            return !_isDrifting || _stopDriftingCoroutine != null;
        }

        private bool IsMinimumDriftConditionsReached()
        {
            return _driftAngle >= _minimumAngle && _speed > _minimumSpeed;
        }

        private void HandleDriftStop()
        {
            if (_driftAngle < _minimumAngle || _speed <= _minimumSpeed)
            {
                if (_isDrifting && _stopDriftingCoroutine == null)
                {
                    StopDrift();
                }
            }
        }

        private void UpdateDriftScore()
        {
            if (_isDrifting)
            {
                _currentScore += Time.deltaTime * _driftAngle * _driftMultiplier;
                _driftMultiplier += Time.deltaTime;
                _driftScoreScreen.DisableEnableScorePanel(true);
            }
        }

        private async void StartDrift()
        {
            if (!_isDrifting)
            {
                await Task.Delay(Mathf.RoundToInt(1000 * _driftingDelay));
                _driftMultiplier = 1;
            }

            if (_stopDriftingCoroutine != null)
            {
                StopCoroutine(_stopDriftingCoroutine);
                _stopDriftingCoroutine = null;
            }

            _driftScoreScreen.SetCurrentScoreColor(_driftScoreScreen.NormalDriftColor);

            _isDrifting = true;
        }

        private void StopDrift()
        {
            _stopDriftingCoroutine = StoppingDrift();
            StartCoroutine(_stopDriftingCoroutine);
        }

        private IEnumerator StoppingDrift()
        {
            yield return new WaitForSeconds(0.1f);
            _driftScoreScreen.SetCurrentScoreColor(_driftScoreScreen.NearStopColor);
            yield return new WaitForSeconds(_driftingDelay * 4f);
            _totalScore += _currentScore;
            _isDrifting = false;
            _driftScoreScreen.SetCurrentScoreColor(_driftScoreScreen.DriftEndedColor);
            yield return new WaitForSeconds(0.5f);
            _currentScore = 0f;
            _driftScoreScreen.DisableEnableScorePanel(false);
        }

        private void ActualizeUI()
        {
            _driftScoreScreen.UpdateCurrentScore(_currentScore);
            _driftScoreScreen.UpdateDriftMultiplier(_driftMultiplier);
            _driftScoreScreen.UpdateTotalScore(_totalScore);
            _driftScoreScreen.UpdateDriftAngle(_driftAngle);
        }
    }
}