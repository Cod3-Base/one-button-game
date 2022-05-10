using __Common.Extensions;
using UnityEngine;

namespace AgarioRipoff.CameraSystem
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private GameObject player;

        [SerializeField] private GameObject joystick;

        [SerializeField] private float cameraZoomIncreasePerPlayerScaleUnit;
        

        private Vector2 _defJoystickPos;
        private Vector2 _defJoystickScale;

        private float _defaultCameraSize;

        private Vector2 _defaultPlayerScale;

        private void Start()
        {
            if (Camera.main is { })
            {
                Camera main;
                _defJoystickPos = (main = Camera.main).WorldToScreenPoint(joystick.transform.position);
                _defJoystickScale = joystick.transform.localScale;

                float orthographicSize = main.orthographicSize;

                _defaultPlayerScale = player.transform.lossyScale;
                _defaultCameraSize = orthographicSize;
            }
        }

        private void Update()
        {
            Vector2 playerScale = player.transform.lossyScale;

            float scalePercentage = ((playerScale.x - _defaultPlayerScale.x) / 1);

            if (playerScale != _defaultPlayerScale && Camera.main is { })
            {
                float newZoomLevel = _defaultCameraSize + (scalePercentage * cameraZoomIncreasePerPlayerScaleUnit);
                
                Camera.main.orthographicSize = newZoomLevel;

                float zoomDifference = newZoomLevel / _defaultCameraSize;
                Vector2 joystickScale = _defJoystickScale * zoomDifference;

                Vector2 pos = Camera.main.ScreenToWorldPoint(_defJoystickPos);
                
                joystick.transform.position = new Vector3(pos.x, pos.y, 0);
                joystick.transform.SetGlobalScale(joystickScale);
            }
        }
    }
}
