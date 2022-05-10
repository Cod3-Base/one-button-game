using UnityEngine;

namespace __Common.ObjectPooling
{
    public abstract class PoolItem : MonoBehaviour
    {
        // The associated object pool
        private ObjectPool _associatedObjectPool;
        
        /// <summary>
        /// Set the object we belong to.
        /// </summary>
        public ObjectPool AssociatedPool
        {
            set => _associatedObjectPool = value;
        }
        
        #region activation
        
        /// <summary>
        /// Called just before the item is activated.
        /// </summary>
        protected abstract void Activate();
        
        /// <summary>
        /// Called just before the item is deactivated.
        /// </summary>
        protected abstract void Deactivate();
        
        #endregion

        #region initialization and return
        
        /// <summary>
        /// The initialization of the Item. Called whenever the object gets asked for in the Object pool.
        /// </summary>
        /// <param name="position"> The required position of the object.</param>
        /// <param name="rotation"> The required rotation of the object.</param>
        /// <param name="parent"> The required parent of the object.</param>
        public void Init(Vector3 position, Quaternion rotation, Transform parent)
        {
            Transform ownTransform = transform;
            
            ownTransform.position = position;
            ownTransform.rotation = rotation;
            ownTransform.parent = parent;
            
            Activate();
        }
        
        /// <summary>
        /// Method to call when the object needs to return.
        /// </summary>
        protected void ReturnToPool()
        {
            Deactivate();
            _associatedObjectPool.ReturnObjectToPool(this);
        }
        
        #endregion
    }
}
