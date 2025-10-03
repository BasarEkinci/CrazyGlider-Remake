using System.Collections.Generic;
using System.Linq;
using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _GameFolders.Scripts.UI.ShopButtons
{
    public class PurchasableItemButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text priceText;
        [SerializeField] private TMP_Text warningText;
        [SerializeField] private Image purchasableItemImage;
        [SerializeField] private Button purchaseButton;

        private int _price;
        private bool _canPurchasable;
        private List<ShopItemType> _requiredPartsToUnlock;
        private ShopItemType _type;
        public void Initialize(int price, Sprite icon,ShopItemType type, List<ShopItemType> requiredPartsToUnlock = null)
        {
            _type = type;
            _price = price;
            priceText.SetText(_price.ToString());
            purchasableItemImage.sprite = icon;
            if (warningText != null)
            {
                warningText.gameObject.SetActive(requiredPartsToUnlock != null);
            }
        }
        
        public void Purchase()
        {
            //if (CoinManager.Instance.GetTotalCoins() < _price) return;
            //if (!CanPurchase()) return;
            CoinManager.Instance.SpendCoins(_price);
            purchaseButton.interactable = false;
            GameEventManager.RaiseItemPurchased(_type);
            priceText.SetText("PURCHASED");
        }
        
        
        private bool CanPurchase()
        {
            return _requiredPartsToUnlock.All(requiredType =>
                ShopManager.Instance.PurchasableParts.Any(part => 
                    part.type == requiredType && part.IsPurchased));
        }
    }
}