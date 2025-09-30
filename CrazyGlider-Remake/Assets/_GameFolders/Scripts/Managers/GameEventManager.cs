using System;
using _GameFolders.Scripts.Enums;

namespace _GameFolders.Scripts.Managers
{
    public static class GameEventManager
    {
        public static event Action OnLevelStart;
        public static event Action<float> OnLevelComplete;
        
        /// <summary>
        /// First float: max height, Second float: distance
        /// </summary>
        public static event Action<float,float> OnLevelFailed;
        public static event Action<PlayerState> OnPlayerStateChanged;
        public static event Action OnSkillActivated;
        public static event Action OnSpendMoney;

        public static void RaiseLevelStart()
        {
            OnLevelStart?.Invoke();
        }

        public static void RaiseLevelComplete(float maxHeight)
        {
            OnLevelComplete?.Invoke(maxHeight);
        }

        public static void RaiseLevelFailed(float maxHeight, float distance)
        {
            OnLevelFailed?.Invoke(maxHeight, distance);
        }

        public static void RaisePlayerStateChanged(PlayerState obj)
        {
            OnPlayerStateChanged?.Invoke(obj);
        }

        private static void RaiseSkillActivated()
        {
            OnSkillActivated?.Invoke();
        }

        public static void RaiseSpendMoney()
        {
            OnSpendMoney?.Invoke();
        }
    }
}