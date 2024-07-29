using System.Collections.Generic;
using UnityEngine;

namespace Shopping
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _carsInShop;

        private int _currentCarIndex;

        private void Start()
        {
            EnableFirstCarInShop();
            DisableOtherCarsInShop();
        }

        public void SwitchCars(int index)
        {
            if (_currentCarIndex == 0 && index < 0)
            {
                _currentCarIndex = _carsInShop.Count - 1;
            }
            else if (_currentCarIndex == _carsInShop.Count - 1 && index > 0)
            {
                _currentCarIndex = 0;
            }
            else
            {
                _currentCarIndex += index;
            }

            for (int i = 0; i < _carsInShop.Count; i++)
            {
                _carsInShop[i].SetActive(i == _currentCarIndex);
            }
        }

        private void DisableOtherCarsInShop()
        {
            for (int i = 1; i < _carsInShop.Count; i++)
            {
                _carsInShop[i].SetActive(false);
            }
        }

        private void EnableFirstCarInShop()
        {
            _carsInShop[0].SetActive(true);
            _currentCarIndex = 0;
        }
    }
}