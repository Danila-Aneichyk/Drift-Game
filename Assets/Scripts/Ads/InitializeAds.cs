using System;
using UnityEngine;

namespace Ads
{
    public class InitializeAds : MonoBehaviour
    {
        [SerializeField] private string _appKey;
        public event Action OnRewardedVideoWatched;
        
    }
}