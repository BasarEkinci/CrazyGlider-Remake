using System.Collections.Generic;
using System.Linq;
using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _GameFolders.Scripts.UI.ShopButtons
{
    public class UpgradableItemButton : MonoBehaviour
    {
        [Header("UI References")] 
        [SerializeField] private TMP_Text priceText;

        [SerializeField] private TMP_Text warningText;
        [SerializeField] private Image levelImage;

        [Header("Object References")] [SerializeField]
        private Transform levelImageParent;

        [Header("Preferences")] [SerializeField]
        private Color purchasedColor;

        [SerializeField] private Color defaultColor;

        private int _price;
        private int _maxLevel;
        private int _currentLevel;
        private List<Image> _levelImages;
        private List<ShopItemType> _requiredPartsToUnlock;
        private List<int> _priceList;

        public void Initialize(int price, int maxLevel, List<int> priceList,
            List<ShopItemType> requiredPartsToUnlock = null)
        {
            _priceList = priceList;
            _price = price;
            _requiredPartsToUnlock = requiredPartsToUnlock;
            _maxLevel = maxLevel;
            _currentLevel = 0;
            CreateLevelImage(_maxLevel);
            priceText.SetText(_price.ToString());
            if (warningText != null)
            {
                warningText.gameObject.SetActive(requiredPartsToUnlock != null);
            }
        }

        public void Purchase()
        {
            /*if (CoinManager.Instance.GetTotalCoins() < _price)
                return;*/
            if (!CanPurchase()) return;
            
            if (_currentLevel == _maxLevel - 1)
            {
                _levelImages[_currentLevel].color = purchasedColor;
                priceText.SetText("MAX");
            }
            else if (_currentLevel < _maxLevel)
            {
                _currentLevel++;
                _levelImages[_currentLevel - 1].color = purchasedColor;
                _price = _priceList[_currentLevel];
                priceText.SetText(_price.ToString());
            }
        }

        private void CreateLevelImage(int maxLevel)
        {
            _levelImages = new List<Image>();
            for (int i = 0; i < maxLevel; i++)
            {
                Image image = Instantiate(levelImage, levelImageParent);
                image.color = defaultColor;
                _levelImages.Add(image);
            }
        }

        private bool CanPurchase()
        {
            if (_requiredPartsToUnlock != null)
            {
                return _requiredPartsToUnlock.All(requiredType =>
                    ShopManager.Instance.PurchasableParts.Any(part => 
                        part.type == requiredType && part.IsPurchased));   
            }
            return true;
        }
    }
}