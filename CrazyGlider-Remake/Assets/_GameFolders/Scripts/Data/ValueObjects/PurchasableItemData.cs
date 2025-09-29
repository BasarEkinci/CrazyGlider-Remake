using UnityEngine;

namespace _GameFolders.Scripts.Data.ValueObjects
{
    [System.Serializable]
    public struct PurchasableItemData
    {
        public string name;
        public Sprite icon;
        public int price;
    }
}