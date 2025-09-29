using System.Collections.Generic;
using _GameFolders.Scripts.Data.ValueObjects;
using UnityEngine;

namespace _GameFolders.Scripts.Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData", order = 0)]
    public class LevelDataSO : ScriptableObject
    {
        [SerializeField] private List<LevelData> levels;
        public List<LevelData> Levels => levels;
        public int LevelCount => levels.Count;
    }
}