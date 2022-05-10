using System;
using System.Collections.Generic;
using System.Linq;
using AgarioRipoff.MovementSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace AgarioRipoff.InputSystem
{
    /// <summary>
    /// This class is responsible for handling the joystick. (Will be refactored later if I have time)
    /// </summary>
    public class Directionchecker : MonoBehaviour
    {
        [SerializeField] private GameObject boundsGameObject;
        [FormerlySerializedAs("player")] [SerializeField] private PlayerBehaviour playerBehaviour;
        [SerializeField] private float maxSpeed;
        

        private Vector3 _startLocation;

        private Vector3 _mouseLastLocation;

        private Vector3 _moveAmount;

        private Camera _mainCam;

        public void Awake()
        {
            _moveAmount = Vector3.zero;
            
            _startLocation = gameObject.transform.localPosition;
            _mainCam = Camera.main;
        }

        public void OnMouseDown()
        {
            if (_mainCam != null)
                _mouseLastLocation = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        }

        public void OnMouseDrag()
        {
            Vector3 newMousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mouseDifference = newMousePos - _mouseLastLocation;

            Vector3 newPos = transform.position + mouseDifference;

            if (AbleToMove(newPos))
            {
                transform.position = newPos;
                
                _moveAmount = CalculateMoveAmount(newPos);
            }

            _mouseLastLocation = newMousePos;
        }

        public void OnMouseUp()
        {
            transform.localPosition = _startLocation;
            _moveAmount = Vector3.zero;
        }

        private Vector2 CalculateMoveAmount(Vector2 joystickBallPosition)
        {
            CircleCollider2D collider2DC = boundsGameObject.GetComponent<CircleCollider2D>();

            float maxScale = new List<float> { collider2DC.bounds.size.x, collider2DC.bounds.size.y }.Max();
            
            float radius = collider2DC.radius * maxScale;

            Vector2 center = boundsGameObject.transform.position;

            float distance = Vector2.Distance(joystickBallPosition, center);
            
            float speedPercentage = CalculatePercentage(0, radius, distance);

            Vector2 difference = joystickBallPosition - center;
            
            Vector2 normalizedPos = difference.normalized;

            float speed = (maxSpeed * (speedPercentage / 100)) * Time.deltaTime;

            return normalizedPos * speed;
        }

        private static float CalculatePercentage(float min, float max, float current)
        {
            return ((current - min) / (max - min)) * 100;
        }

        private bool AbleToMove(Vector3 newMovePos)
        {
            CircleCollider2D collider2DC = boundsGameObject.GetComponent<CircleCollider2D>();

            float maxScale = new List<float> { collider2DC.bounds.size.x, collider2DC.bounds.size.y }.Max();
            
            float radius = collider2DC.radius * maxScale;

            Vector3 center = boundsGameObject.transform.position;

            float distance = Vector3.Distance(newMovePos, center);

            return (distance < radius || distance == 0);
        }

        public void LateUpdate()
        {
            playerBehaviour.Move(_moveAmount, 1);
        }
    }
}
