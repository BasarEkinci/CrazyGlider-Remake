using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _GameFolders.Scripts.UI.ShopButtons
{
    public class UpgradableItemButton : MonoBehaviour
    {
        public Transform LevelImageParent => levelImageParent;
        
        [SerializeField] private Transform levelImageParent;
        [SerializeField] private TMP_Text priceText;
        [SerializeField] private TMP_Text warningText;
        [SerializeField] private Color purchasedColor;
        [SerializeField] private Color defaultColor;

        private int _maxLevel;
        private int _price;
        private Image _levelImage;
        private ShopItemType _type;
        private void InitializeButton(int price, int maxLevel,Image levelImage,ShopItemType type,bool canPurchasable = true)
        {
            _type = type;
            _price = price;
            _maxLevel = maxLevel;
            _levelImage = levelImage;
            priceText.SetText($"{_price}");
            warningText.gameObject.SetActive(!canPurchasable);
            GameEventManager.OnSpendMoney += OnSpendMoney;
        }

        public void Purchase()
        {
            
        }
        
        private void OnSpendMoney(ShopItemType type)
        {
            
        }
    }
}