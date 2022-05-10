using System;
using __Common.Extensions;
using UnityEngine;

namespace AgarioRipoff.MovementSystem
{
    public class PlayerBehaviour : MonoBehaviour
    {
        [SerializeField] private Camera mainCam;

        public void Move(Vector3 amount, float speed)
        {
            Vector3 newMovePos = transform.position + (amount * speed);

            if (mainCam.CheckLocationWithScreenBounds(newMovePos, transform.localScale.x, transform.localScale.y))
                return;
            
            transform.position = newMovePos;
        }

        public void Grow(float growAmount)
        {
            Vector2 growRate = new Vector2(growAmount, growAmount);
            
            transform.SetGlobalScale(transform.localScale + (Vector3)growRate); 
        }
    }
}
