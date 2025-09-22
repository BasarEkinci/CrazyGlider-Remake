using _GameFolders.Scripts.Managers;
using Sirenix.OdinInspector;
using UnityEngine;
using _GameFolders.Scripts.Controllers; 

namespace _GameFolders.Scripts
{
    public class Test : MonoBehaviour
    {
        [Button("Invoke Game Start", ButtonSizes.Large)]
        private void InvokeGameStart()
        {
            GameEventManager.RaiseLevelStart();
        }

        [Button("Invoke Level Complete", ButtonSizes.Large)]
        private void InvokeLevelComplete()
        {
            GameEventManager.RaiseLevelComplete();
        }

        [Button("Invoke Level Failed", ButtonSizes.Large)]
        private void InvokeLevelFailed()
        {
            GameEventManager.RaiseLevelFailed();
        }

        [EnumToggleButtons]
        [SerializeField] private PlayerState playerStateToRaise = PlayerState.Idle;

        [Button("Invoke Player State Changed", ButtonSizes.Large)]
        private void InvokePlayerStateChanged()
        {
            GameEventManager.RaisePlayerStateChanged(playerStateToRaise);
        }
    }
}