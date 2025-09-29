using _GameFolders.Scripts.Data.ScriptableObjects;
using _GameFolders.Scripts.Extensions;
using UnityEngine;

namespace _GameFolders.Scripts.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private LevelDataSO levelDataSo;
        private int _currentLevelIndex;
        private void Start()
        {
            GameEventManager.RaiseLevelStart();
        }

        public void NexLevel()
        {
            if (_currentLevelIndex < levelDataSo.LevelCount)
            {
                _currentLevelIndex++;
                GameEventManager.RaiseLevelStart();
            }
        }
    }
}