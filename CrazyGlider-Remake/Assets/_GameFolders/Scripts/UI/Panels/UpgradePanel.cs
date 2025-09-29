using _GameFolders.Scripts.Data.ScriptableObjects;
using _GameFolders.Scripts.UI.PurchaseButtons;
using UnityEngine;

namespace _GameFolders.Scripts.UI.Panels
{
    public class UpgradePanel : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private LevelDataSO levelData;

        [Header("Upgradable Item Buttons")]
        [SerializeField] private UpgradableItemButton speedUpgrade;
        [SerializeField] private UpgradableItemButton cliffUpgrade;
        [SerializeField] private UpgradableItemButton fuelUpgrade;
    }
}