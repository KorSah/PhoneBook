namespace PhoneBookConsoleApp.Helpers
{
    public static class FileHelper
    {
        public static List<string>? ReadLines(string filePath)
        {
            try
            {
                return new List<string>(File.ReadAllLines(filePath));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading the file: {ex.Message}");
                return null;
            }
        }

        public static void WriteLines(string filePath, List<string> lines)
        {
            try
            {
                File.WriteAllLines(filePath, lines);
                Console.WriteLine("File written successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to the file: {ex.Message}");
            }
        }
    }
}
