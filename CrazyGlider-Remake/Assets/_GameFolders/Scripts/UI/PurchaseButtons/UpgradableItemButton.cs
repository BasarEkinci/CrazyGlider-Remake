using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _GameFolders.Scripts.UI.PurchaseButtons
{
    public class UpgradableItemButton : MonoBehaviour
    {
        [SerializeField] private Button purchaseButton;
        [SerializeField] private Transform levelImageParent;
        [SerializeField] private Image levelImage;
        [SerializeField] private TMP_Text priceText;
        [SerializeField] private Color purchasedColor;
        [SerializeField] private Color defaultColor;
        
        private int _currentPrice;
        private int _currentLevel;
        private List<Image> _levelImages;
        private List<int> _priceList;
        
        public void InitializeButtonValues(int totalLevel,List<int> priceList)
        {
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
        }
    }
}