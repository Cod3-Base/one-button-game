using System;
using System.Collections.Generic;
using System.Linq;

namespace __Common.Extensions
{
    /// <summary>
    /// This class contains some commonly used extensions.
    /// </summary>
    public static class NormalExtensions
    {
        #region Container Extensions
        
        /// <summary>
        /// Concatenates an array into each other.
        /// </summary>
        /// <param name="first">The first array.</param>
        /// <param name="second">The second array.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>An array consisting of both array elements.</returns>
        public static T[] Concatenate<T>(this T[] first, T[] second)
        {
            if (first == null) {
                return second;
            }
            
            return second == null ? first : first.Concat(second).ToArray();
        }
        
        /// <summary>
        /// Removes items from a certain index.
        /// </summary>
        /// <param name="list">The collection to perform on.</param>
        /// <param name="indexFrom">The start index.</param>
        /// <param name="indexEnd">The end index. Leave empty if everything after.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>The edited collection.</returns>
        public static List<T> RemoveFrom<T>(this List<T> list, int indexFrom, int indexEnd=-1)
        {
            if (indexEnd != -1 && (indexEnd > list.Count || indexFrom < 0))
                throw new Exception("Invalid range given!");

            if (indexEnd == -1)
            {
                for (int i = indexFrom; i < list.Count; i++)
                {
                    list.RemoveAt(i);
                }
            }

            else if (indexFrom < indexEnd)
            {
                for (int i = indexFrom; i < indexEnd; i++)
                {
                    list.RemoveAt(i);
                }
            }
            
            else if (indexFrom > indexEnd)
            {
                for (int i = indexFrom; i >= indexEnd; i--)
                {
                    list.RemoveAt(i);
                }
            }
            
            return list;
        }
        
        #endregion

        /// <summary>
        /// Generates a random float between two values. If the minimum is larger than the max the numbers are automatically swapped.
        /// </summary>
        /// <param name="min">The minimum number.</param>
        /// <param name="max">The maximum number.</param>
        /// <returns>A random float value between min and max.</returns>
        public static float RandomFloat(float min, float max)
        {
            Random random = new Random();
            
            float range = max - min;

            if (min > max)
            {
                range = Math.Abs(range);
            }

            double sample = random.NextDouble();
            double scaled = (sample * range) + min;
            return (float)scaled;
        }
    }
}
