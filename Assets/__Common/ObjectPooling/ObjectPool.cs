using System.Collections.Generic;
using UnityEngine;

namespace __Common.ObjectPooling
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] GameObject pooledObject;
        [SerializeField] int startPoolSize = 5;
        [SerializeField, Tooltip("Keep at zero if you don't want to expand if empty.")] int amountToExpandOnceEmpty;
        
        private Stack<PoolItem> _objectPool;

        #region Start and Update
        
        private void Awake()
        {
            _objectPool = new Stack<PoolItem>(startPoolSize);
            
            Expand(startPoolSize);
        }

        private void Update()
        {
            if (_objectPool.Count <= 0)
            {
                Expand(amountToExpandOnceEmpty);
            }
        }
        
        #endregion
        
        
        #region retrieval and returnal

        /// <summary>
        /// Returns the top most GameObject from the pool
        /// </summary>
        /// <param name="position"> The position where the object must be "instantiated".</param>
        /// <param name="rotation"> The rotation at which the object must be "instantiated".</param>
        /// <param name="parent"> The parent of which the object must be.</param>
        /// <returns> GameObject requested. </returns>
        public GameObject GetItemFromPool(Vector3 position, Quaternion rotation, Transform parent=null)
        {
            if (_objectPool.Count == 0)
            {
                Debug.Log($"{name}: Object pool empty, can't get item!");
                return null;
            }
            
            PoolItem poolItem = _objectPool.Pop();

            poolItem.Init(position, rotation, parent != null ? parent : transform);
            
            GameObject poolObject;
            (poolObject = poolItem.gameObject).SetActive(true);
            
            return poolObject;
        }
        
        /// <summary>
        /// Returns the GameObject to the pool.
        /// </summary>
        /// <param name="item"> The item to return.</param>
        public void ReturnObjectToPool(PoolItem item)
        {
            if (!item.gameObject.activeSelf)
                return;

            item.transform.parent = transform;
            item.gameObject.SetActive(false);
            
            _objectPool.Push(item);
        }
        
        #endregion
        
        /// <summary>
        /// Expands the object pool stack.
        /// </summary>
        /// <param name="expandAmount"> The amount of which the pool to expand by.</param>
        private void Expand(int expandAmount)
        {
            for (int i = 0; i < expandAmount; i++)
            {
                GameObject newObject = Instantiate(pooledObject);
                
                PoolItem item = newObject.GetComponent<PoolItem>();
                item.AssociatedPool = this;
                
                ReturnObjectToPool(item);
            }
        }
        
    }
}
