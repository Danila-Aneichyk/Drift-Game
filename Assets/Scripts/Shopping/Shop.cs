using System.Collections.Generic;
using UnityEngine;

namespace Shopping
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _carsInShop;

        private void Start()
        {
            EnableFirstCarInShop();
            DisableOtherCarsInShop();
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
        }
        
        
    }
}