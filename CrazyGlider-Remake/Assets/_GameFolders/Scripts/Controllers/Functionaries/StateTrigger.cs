using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Managers;
using UnityEngine;

namespace _GameFolders.Scripts.Controllers.Functionaries
{
    public class StateTrigger : MonoBehaviour
    {
        [SerializeField] private PlayerState playerState;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameEventManager.RaisePlayerStateChanged(playerState);
            }
        }
    }
}