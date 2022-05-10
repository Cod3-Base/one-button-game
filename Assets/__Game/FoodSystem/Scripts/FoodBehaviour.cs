using System;
using __Common.Extensions;
using __Common.ObjectPooling;
using AgarioRipoff.MovementSystem;
using UnityEngine;

namespace AgarioRipoff.FoodSystem
{
    public class FoodBehaviour : PoolItem
    {
        private float _growAmount;

        public void Initialize(float growAmount)
        {
            _growAmount = growAmount;
        }
        
        public void OnTriggerStay2D(Collider2D other)
        {
            GameObject otherObject = other.gameObject;

            if (!otherObject.CompareTag("Player"))
                return;

            if (!otherObject.ObjectFullyWithinBorders(gameObject))
                return;

            PlayerBehaviour playerScript = other.GetComponent<PlayerBehaviour>();

            if (playerScript == null)
                throw new Exception("Player has no player behaviour script!");
            
            playerScript.Grow(_growAmount);
            
            ReturnToPool();
        }

        protected override void Activate()
        {
            
        }

        protected override void Deactivate()
        {
            
        }
    }
}
