namespace __Common
{
    /// <summary>
    /// This class is responsible solely for formatting time.
    /// </summary>
    public static class TimeFormatter
    {
        /// <summary>
        /// This method formats two time units into a nicely formatted string.
        /// </summary>
        /// <param name="seconds">The amount of seconds.</param>
        /// <param name="minutes">The amount of minutes.</param>
        /// <returns>A formatted string representing the duration.</returns>
        public static string FormatTimeString(float seconds, float minutes)
        {
            if (seconds < 10)
            {
                return minutes < 10 ? $"0{minutes}:0{seconds}" : $"{minutes}:0{seconds}";
            }
            
            return minutes < 10 ? $"0{minutes}:{seconds}" : $"{minutes}:{seconds}";
        }
        
        /// <summary>
        /// This method formats two time units into a nicely formatted string.
        /// </summary>
        /// <param name="seconds">The amount of seconds.</param>
        /// <param name="minutes">The amount of minutes.</param>
        /// <param name="hours">The amount of hours.</param>
        /// <returns>A formatted string representing the duration.</returns>
        public static string FormatTimeString(float seconds, float minutes, float hours)
        {
            if (seconds < 10)
            {
                if (minutes < 10)
                {
                    return hours < 10 ? $"0{hours}:0{minutes}:0{seconds}" : $"{hours}:0{minutes}:0{seconds}";
                }
                return hours < 10 ? $"0{hours}:{minutes}:0{seconds}" : $"{hours}:{minutes}:0{seconds}";
            }
            
            if (minutes < 10)
            {
                return hours < 10 ? $"0{hours}:0{minutes}:{seconds}" : $"{hours}:0{minutes}:{seconds}";
            }
            return hours < 10 ? $"0{hours}:{minutes}:{seconds}" : $"{hours}:{minutes}:{seconds}";
        }
    }
}
