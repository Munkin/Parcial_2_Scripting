using UnityEngine;

namespace Parcial2.Store
{
    public abstract class StoreItemBase : MonoBehaviour, IStoreItem
    {
        private StoreCategory category;

        [SerializeField]
        private int price;

        public StoreCategory Category
        {
            get { return category; }
            protected set { category = value; }
        }

        public int Price
        {
            get { return price; }
        }

        public abstract void Equip();

        public abstract void Unequip();

        protected virtual void Reset()
        {
            Category = StoreCategory.None;
        }
    }
}