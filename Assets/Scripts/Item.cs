using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable, RequireComponent(typeof(Collider2D))]
    public class Item : MonoBehaviour
    {
        public enum ItemType
        {
            Bomb,
            Wall,
            Gun,
            Mine
        }

        [SerializeField]
        private GameObject _pickupSprite;
        [SerializeField]
        private GameObject _itemSprite;
        [SerializeField]
        private ItemType _itemType;

        protected Collider2D _collider;
        protected PlayerController _player;

        public bool IsPickedUp
        {
            get
            {
                return _isPickedUp;
            }
            set
            {
                _isPickedUp = value;
                _pickupSprite.SetActive(!value);
                _itemSprite.SetActive(value);
            }
        }

        public ItemType Type
        {
            get
            {
                return _itemType;
            }
        }

        private bool _isPickedUp;

        private void Start()
        {
            Initialize();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.PickUpItem(this);
                _player = player;
            }
        }

        private void Initialize()
        {
            _isPickedUp = false;
            _collider = GetComponent<Collider2D>();
            _collider.isTrigger = true;
        }

        public virtual void Use()
        {
            
        }
    }
}
