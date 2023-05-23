using PhoneBookConsoleApp.Models;

namespace PhoneBookConsoleApp.Helpers
{
    public static class Mapper
    {
        public static LineInfo? MapLine(string line)
        {
            if (line is null)
                return null;

            string[] parts = line.Split(' ');

            if (parts.Length == 4)
            {
                string name = parts[0];
                string lastname = parts[1];
                string separator = parts[2];
                string phoneNumber = parts[3];

                return new LineInfo
                {
                    FirstName = name,
                    LastName = lastname,
                    Separator = separator,
                    Phone = phoneNumber
                };
            }
            else if (parts.Length == 3)
            {
                string name = parts[0];
                string separator = parts[1];
                string phoneNumber = parts[2];

                return new LineInfo
                {
                    FirstName = name,
                    Separator = separator,
                    Phone = phoneNumber
                };
            }
            else
                return null;
        }
    }
}
