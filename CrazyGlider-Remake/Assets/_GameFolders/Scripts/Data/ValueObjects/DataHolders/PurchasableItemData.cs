using System.Collections.Generic;
using _GameFolders.Scripts.Enums;
using UnityEngine;

namespace _GameFolders.Scripts.Data.ValueObjects.DataHolders
{
    [System.Serializable]
    public struct PurchasableItemData
    {
        public int price;
        public Sprite icon;
        public List<ItemType> requiredPartsToUnlock;
    }
}