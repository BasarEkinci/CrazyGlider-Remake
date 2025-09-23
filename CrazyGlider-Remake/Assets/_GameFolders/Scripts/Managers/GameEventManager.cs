using System;
using _GameFolders.Scripts.Enums;

namespace _GameFolders.Scripts.Managers
{
    public static class GameEventManager
    {
        public static event Action OnLevelStart;
        public static event Action OnLevelComplete;
        public static event Action OnLevelFailed;
        public static event Action<PlayerState> OnPlayerStateChanged;

        public static void RaiseLevelStart()
        {
            OnLevelStart?.Invoke();
        }

        public static void RaiseLevelComplete()
        {
            OnLevelComplete?.Invoke();
        }

        public static void RaiseLevelFailed()
        {
            OnLevelFailed?.Invoke();
        }

        public static void RaisePlayerStateChanged(PlayerState obj)
        {
            OnPlayerStateChanged?.Invoke(obj);
        }
    }
}