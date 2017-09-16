using Parcial2.Game;
using UnityEngine;

namespace Parcial2.Store
{
    public class StoreManager : MonoBehaviour
    {
        public delegate void OnStoreItemPurchased(int itemPrice);

        public event OnStoreItemPurchased onStoreItemPurchased;

        public void PurchaseItem(StoreItemBase storeItem)
        {
            if (Player.Instance.CurrentCurrency >= storeItem.Price)
            {


                if (onStoreItemPurchased != null)
                {
                    onStoreItemPurchased(storeItem.Price);
                }
            }
        }
    }
}