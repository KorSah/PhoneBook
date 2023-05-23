using PhoneBookConsoleApp.Constants;
using PhoneBookConsoleApp.Models;

namespace PhoneBookConsoleApp.Helpers
{
    public static class Extensions
    {
        /// <summary>
        /// Validates the lineInfo and returns the error messages
        /// </summary>
        /// <param name="lineInfo"></param>
        /// <returns>messages</returns>
        public static string[] Validate(this LineInfo lineInfo)
        {
            if (lineInfo.Phone.Length != 9 && lineInfo.Separator != Separators.Dash.ToString() && lineInfo.Separator != Separators.Colon.ToString())
                return new string[] { ErrorMessage.PhoneDigits, ErrorMessage.Separator };
            else if (lineInfo.Phone.Length != 9)
                return new string[] { ErrorMessage.PhoneDigits };
            else if (lineInfo.Separator != Separators.Dash.ToString() && lineInfo.Separator != Separators.Colon.ToString())
                return new string[] { ErrorMessage.Separator };
            else
                return Array.Empty<string>();
        }
    }
}
