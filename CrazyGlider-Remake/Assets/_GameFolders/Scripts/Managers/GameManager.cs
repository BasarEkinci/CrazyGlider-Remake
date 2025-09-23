using System;
using UnityEngine;

namespace _GameFolders.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            GameEventManager.RaiseLevelStart();
        }
    }
}