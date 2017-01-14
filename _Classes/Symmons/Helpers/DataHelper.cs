
namespace symmons.com._Classes.Symmons.Helpers
{
    public static class DataHelper
    {
        /// <summary>
        /// Get teh parsed string value based on maximum characters allowed.
        /// </summary>
        /// <param name="fieldValue">string value</param>
        /// <param name="validateCount">maximum characters count</param>
        /// <returns>Parsed string value</returns>
        public static string GetParsedString(string fieldValue, int validateCount)
        {
            return fieldValue.Length <= validateCount ? fieldValue : fieldValue.Substring(0, validateCount);
        }
    }
}