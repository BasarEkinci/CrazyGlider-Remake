using _GameFolders.Scripts.Extensions;
using UnityEngine;
using static _GameFolders.Scripts.Managers.GameEventManager;

namespace _GameFolders.Scripts.Managers
{
    public class CoinManager : MonoSingleton<CoinManager>
    {
        private float _distance;
        private float _maxHeight;
        private int _earnedCoins;
        private int _totalCoins;
        private const float COIN_FACTOR = 3.2f;
        private void OnEnable()
        {
            OnLevelFailed += HandleLevelFailed;
        }
        
        private void OnDisable()
        {
            OnLevelFailed -= HandleLevelFailed;
        }

        private void HandleLevelFailed(float maxHeight, float distance)
        {
            _maxHeight = maxHeight;
            _distance = distance;
            CalculateEarnedCoins();
        }
        
        public void AddCoins(int amount)
        {
            _earnedCoins += amount;
            _totalCoins += amount; 
        }

        private void CalculateEarnedCoins()
        {
            _earnedCoins = Mathf.RoundToInt((_maxHeight + _distance) * COIN_FACTOR);
        }
        
        public void SpendCoins(int amount) { _totalCoins -= amount; }
        
        public int GetTotalCoins() { return _totalCoins; }
        public int GetEarnedCoins() { return _earnedCoins; }
    }
}