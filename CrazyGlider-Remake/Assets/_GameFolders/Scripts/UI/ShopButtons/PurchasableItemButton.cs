using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _GameFolders.Scripts.UI.ShopButtons
{
    public class PurchasableItemButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text priceText;
        [SerializeField] private TMP_Text warningText;
        [SerializeField] private Button purchaseButton;

        private int _price;
        private bool _canPurchasable;
    }
}