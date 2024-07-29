using System;
using Shopping;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ShopScreen : BaseScreen
    {
        [SerializeField] private Shop _shop;

        [SerializeField] private Button _rightButton;
        [SerializeField] private Button _leftButton;

        private void Awake()
        {
            _rightButton.onClick.AddListener(SwitchToRight);
            _leftButton.onClick.AddListener(SwitchToLeft);
        }

        private void SwitchToLeft()
        {
            _shop.SwitchCars(-1);
        }
        
        private void SwitchToRight()
        {
            _shop.SwitchCars(1);
        }
    }
}