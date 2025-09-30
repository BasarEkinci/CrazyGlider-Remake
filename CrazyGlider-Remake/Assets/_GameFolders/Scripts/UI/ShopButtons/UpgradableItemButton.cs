using System.Collections.Generic;
using _GameFolders.Scripts.Items;
using _GameFolders.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _GameFolders.Scripts.UI.ShopButtons
{
    public class UpgradableItemButton : MonoBehaviour
    {
        [SerializeField] private Button purchaseButton;
        [SerializeField] private Transform levelImageParent;
        [SerializeField] private Image levelImage;
        [SerializeField] private TMP_Text priceText;
        [SerializeField] private TMP_Text warningText;
        [SerializeField] private Color purchasedColor;
        [SerializeField] private Color defaultColor;

        private bool _canPurchasable;
        private int _currentPrice;
        private int _currentLevel;
        private List<Image> _levelImages;
        private List<int> _priceList;
        private List<PlanePart> _requiredPartsToUnlock;
        
        public void InitializeButtonValues(int totalLevel,List<int> priceList, bool hasAnyRequirement = false, List<PlanePart> requiredPartsToUnlock = null)
        {
            GameEventManager.OnSpendMoney += OnSpendMoney;
            _canPurchasable = hasAnyRequirement;
            _requiredPartsToUnlock = requiredPartsToUnlock;
            warningText.gameObject.SetActive(hasAnyRequirement);
            _priceList = priceList;
            _currentPrice = priceList[0];
            priceText.SetText($"{_currentPrice}");
            _levelImages = new List<Image>();
            _currentLevel = 0;
            for (int i = 0; i < totalLevel; i++)
            {
                Image image = Instantiate(levelImage, levelImageParent);
                _levelImages.Add(image);
            }
        }
        public void Upgrade()
        {
            if (!_canPurchasable) return;
            // Add spend money
            _currentLevel++;
            if (_currentLevel < _levelImages.Count)
            {
                _levelImages[_currentLevel - 1].color = purchasedColor;
                _currentPrice = _priceList[_currentLevel];
                priceText.SetText($"{_currentPrice}");
            }
            else if (_currentLevel == _levelImages.Count)
            {
                _levelImages[_currentLevel - 1].color = purchasedColor;
                purchaseButton.interactable = false;
                priceText.SetText("MAX");
            }
        }
        public void ResetButtonValues()
        {
            _currentLevel = 0;
            purchaseButton.interactable = true;
            foreach (var image in _levelImages)
            {
                Destroy(image.gameObject);
            }
            priceText.SetText(_currentPrice.ToString());
            GameEventManager.OnSpendMoney -= OnSpendMoney;
        }
        
        private void OnSpendMoney()
        {
            if (_requiredPartsToUnlock == null) return;
            foreach (var items in _requiredPartsToUnlock)
            {
                bool isPurchased = items.IsPurchased;
                if (!isPurchased)
                {
                    _canPurchasable = false;
                    break;
                }
            }
        }
    }
}