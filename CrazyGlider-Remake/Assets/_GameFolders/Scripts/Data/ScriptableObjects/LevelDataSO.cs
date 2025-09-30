using System.Collections.Generic;
using _GameFolders.Scripts.Data.ValueObjects.DataHolders;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace _GameFolders.Scripts.Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData", order = 0)]
    public class LevelDataSO : ScriptableObject
    {
        [SerializeField] private List<LevelData> levels;
        public List<LevelData> Levels => levels;
        public int LevelCount => levels.Count;

#if UNITY_EDITOR
        private void OnValidate()
        {
            foreach (var level in levels)
            {
                ShopData data = level.shopData;
                CreateUpgradableItemList(ref data.speed.priceList, data.speed.maxLevel);
                CreateUpgradableItemList(ref data.cliff.priceList, data.cliff.maxLevel);
                CreateUpgradableItemList(ref data.fuel.priceList, data.fuel.maxLevel);
            }
            EditorUtility.SetDirty(this);
        }

        private void CreateUpgradableItemList(ref List<int> priceList,int maxLevel)
        {
            if (maxLevel == 0)
                return;
            
            if (priceList == null)
                priceList = new List<int>();

            if (priceList.Count != maxLevel)
            {
                if (priceList.Count < maxLevel)
                {
                    int diff = maxLevel - priceList.Count;
                    for (int i = 0; i < diff; i++)
                    {
                        priceList.Add(0);
                    }
                }
                else
                {
                    priceList.RemoveRange(maxLevel, priceList.Count - maxLevel);
                }
            }
        }
#endif
    }
}