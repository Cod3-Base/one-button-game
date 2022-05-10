using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace __Common.Extensions
{
    /// <summary>
    ///     This class contains some commonly used extensions for unity classes.
    /// </summary>
    public static class UnityExtensions
    {
        /// <summary>
        ///     Sets the global scale, do not know full formula, got it from internet, but it works.
        /// </summary>
        /// <param name="transform">The transform to use it from.</param>
        /// <param name="globalScale">The global scale to set.</param>
        public static void SetGlobalScale(this Transform transform, Vector3 globalScale)
        {
            transform.localScale = Vector3.one;
            var lossyScale = transform.lossyScale;
            transform.localScale = new Vector3(globalScale.x / lossyScale.x, globalScale.y / lossyScale.y, globalScale.z / lossyScale.z);
        }
        
        public static bool PointWithinTransform(this Transform self, Vector2 location) // Method doesn't work yet, but fuck it!
        {
            Vector3 position = self.position;
            Vector3 lossyScale = self.lossyScale;

            float xMin = (position.x - (lossyScale.x / 2));
            float xMax = (position.x + (lossyScale.x / 2));

            float yMin = (position.y - (lossyScale.y / 2));
            float yMax = (position.y + (lossyScale.y / 2));
            
            // Debug.Log($"Rect:\nMinX{xMin}; MaXX{xMax}\nMinY{yMin}; MaxY{yMax}");
            // Debug.Log($"Location:\nX{location.x}; Y{location.y}");

            return (location.x) > xMin &&
                   (location.x) < xMax &&
                   (location.y) > yMin &&
                   (location.y) < yMax;
        }

        #region Camera Extensions

        public enum Corner
        {
            TopRight,
            BottomLeft
        }

        /// <summary>
        ///     Calculates the screen-bounds based on the current camera.
        /// </summary>
        /// <param name="self">The camera to calculate it from.</param>
        /// <param name="cornerToReturn">The corner to return.</param>
        /// <returns></returns>
        public static Vector3 GetScreenBounds(this Camera self, Corner cornerToReturn)
        {
            return cornerToReturn switch
            {
                Corner.TopRight => self.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)),
                Corner.BottomLeft => self.ScreenToWorldPoint(Vector3.zero),
                _ => Vector3.zero
            };
        }

        /// <summary>
        ///     Calculates the screen center based on the screen bounds.
        /// </summary>
        /// <param name="self">The camera used to calculate it.</param>
        /// <returns>A vector3 representing the center of the screen.</returns>
        public static Vector3 GetScreenCenter(this Camera self)
        {
            float centerX = Screen.width * 0.5f;
            float centerY = Screen.height * 0.5f;

            return self.ScreenToWorldPoint(new Vector3(centerX, centerY, 0));
        }

        /// <summary>
        ///     Returns true if the object is outside of the screen.
        /// </summary>
        /// <param name="self">The camera used to perform the action.</param>
        /// <param name="location"> The location of the object to check.</param>
        /// <param name="objectWidth">The width of the object.</param>
        /// <param name="objectHeight">The height of the object.</param>
        /// <returns></returns>
        public static bool CheckLocationWithScreenBounds(this Camera self, Vector3 location, float objectWidth, float objectHeight)
        {
            float objectW = objectWidth / 2;
            float objectH = objectHeight / 2;
            
            // Returns true if outside of screen.
            var viewport = self.rect;
            var calculatedLocation = self.WorldToViewportPoint(location);

            return (calculatedLocation.x) > viewport.width 
                   || (calculatedLocation.x) < viewport.x
                   || (calculatedLocation.y) > viewport.height 
                   || (calculatedLocation.y) < viewport.y;
        }

        public static Vector2 GetRandomScreenLocation(this Camera self, [CanBeNull] List<Transform> notAllowed=null)
        {
            Vector3 topRight = self.GetScreenBounds(Corner.TopRight);
            Vector3 bottomLeft = self.GetScreenBounds(Corner.BottomLeft);
            
            float minX = bottomLeft.x;
            float maxX = topRight.x;

            float minY = bottomLeft.y;
            float maxY = topRight.y;
            
            float randomX = UnityEngine.Random.Range(minX, maxX);
            float randomY = UnityEngine.Random.Range(minY, maxY);
            
            if (notAllowed == null)
            {
                return new Vector2(randomX, randomY);
            }

            foreach (Transform transform in notAllowed)
            {
                while (transform.PointWithinTransform(new Vector2(randomX, randomY)))
                {
                    randomX = UnityEngine.Random.Range(minX, maxX);
                    randomY = UnityEngine.Random.Range(minY, maxY);
                }
            }

            return new Vector2(randomX, randomY);
        }

        #endregion
        
        #region GameObject Extensions
        
        /// <summary>
        /// This method checks if another object is within the borders of this object.
        /// </summary>
        /// <param name="self">Own GameObject</param>
        /// <param name="otherObject">The other GameObject</param>
        /// <returns>True if the other object is fully within borders.</returns>
        public static bool ObjectFullyWithinBorders(this GameObject self, GameObject otherObject)
        {
            if (self.GetComponent<SpriteRenderer>() == null)
                throw new Exception("Self object has no Sprite Renderer!");
            
            if (otherObject.GetComponent<SpriteRenderer>() == null)
                throw new Exception("Other object has no Sprite Renderer!");

            SpriteRenderer ownRenderer = self.GetComponent<SpriteRenderer>();
            SpriteRenderer otherRenderer = otherObject.GetComponent<SpriteRenderer>();

            Bounds ownBounds = ownRenderer.bounds;
            Bounds otherBounds = otherRenderer.bounds;
            
            bool inHorizontalAxis = (otherBounds.min.x > ownBounds.min.x && otherBounds.max.x < ownBounds.max.x);
            bool inVerticalAxis = (otherBounds.min.y > ownBounds.min.y && otherBounds.max.y < ownBounds.max.y);

            return inHorizontalAxis && inVerticalAxis;
        }
        
        #endregion
    }
}
