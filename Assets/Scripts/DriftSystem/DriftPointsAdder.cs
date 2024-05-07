using UnityEngine;

namespace Points
{
    public class DriftPointsAdder : MonoBehaviour
    {
        private float _currentPoints;
        
        public void AddPoints(float time)
        {
            _currentPoints += time * 0.1f;
        }
    }
}