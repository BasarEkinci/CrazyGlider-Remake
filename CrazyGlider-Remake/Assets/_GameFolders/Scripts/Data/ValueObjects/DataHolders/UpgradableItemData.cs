using System.Collections.Generic;
using _GameFolders.Scripts.Enums;


namespace _GameFolders.Scripts.Data.ValueObjects.DataHolders
{
    [System.Serializable]
    public struct UpgradableItemData
    {
        public int maxLevel; 
        public List<int> priceList;
        public List<ShopItemType> requiredPartsToUnlock;
    }
}