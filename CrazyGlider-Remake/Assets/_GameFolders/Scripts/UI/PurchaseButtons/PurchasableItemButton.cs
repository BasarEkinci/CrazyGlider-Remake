using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _GameFolders.Scripts.UI.PurchaseButtons
{
    public class PurchasableItemButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text priceText;
        [SerializeField] private Button purchaseButton;
        private int _price;
        
        public void InitializeButton(int price)
        {
            _price = price;
        }

        public void Purchase()
        {
            // add spend logic
            priceText.SetText("Purchased");
            purchaseButton.interactable = false;
        }
        
        public void ResetButton()
        {
            priceText.SetText($"{_price}");
            purchaseButton.interactable = true;
        }
    }
}