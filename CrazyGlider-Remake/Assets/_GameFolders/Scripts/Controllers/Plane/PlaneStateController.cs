using _GameFolders.Scripts.Enums;
using UnityEngine;
using static _GameFolders.Scripts.Managers.GameEventManager;

namespace _GameFolders.Scripts.Controllers.Plane
{
    public class PlaneStateController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("HillEntry"))
            {
                RaisePlayerStateChanged(PlayerState.Rolling);
            }
            else if (other.CompareTag("Sea"))
            {
                RaisePlayerStateChanged(PlayerState.Sink);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("HillExit"))
            {
                RaisePlayerStateChanged(PlayerState.Flying);
            }
        }
    }
}