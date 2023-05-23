using PhoneBookConsoleApp.Constants;
using PhoneBookConsoleApp.Helpers;
using PhoneBookConsoleApp.Models;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace PhoneBookConsoleApp.Services
{
    public class PhoneBookService 
    {
        private readonly string _filePath;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filePath"></param>
        public PhoneBookService(string filePath) => _filePath = filePath;
        
        /// <summary>
        /// Get the validations
        /// </summary>
        /// <returns>
        /// Dictionary with line number key and errors value
        /// </returns>
        public Dictionary<int, string[]> GetValidations()
        {
            var phoneBookLines = new Dictionary<int, string[]>();
            try
            {
                using StreamReader reader = new(_filePath);
                string? line;
                int lineNumber = 1;

                while ((line = reader?.ReadLine()) is not null)
                {
                    var record = Mapper.MapLine(line);

                    var errors = record is not null ? record.Validate() : new string[] { ErrorMessage.WrongStructure };

                    if(errors.Length > 0)
                        phoneBookLines.Add(lineNumber, errors);

                    lineNumber++;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error in reading file {ex.Message}");
            }

            return phoneBookLines;
        }

        /// <summary>
        /// Get all lines in phone book
        /// </summary>
        /// <returns>Phone book collection</returns>
        public IEnumerable<LineInfo> GetValidRecords()
        {
            var records = new List<LineInfo>();
            try
            {
                using StreamReader reader = new(_filePath);
                string? line;
                int lineNumber = 1;

                while ((line = reader?.ReadLine()) is not null)
                {
                    var record = Mapper.MapLine(line);
                    var errors = record?.Validate();

                    if (record is not null && (errors is null || errors?.Length == 0))
                        records.Add(record);

                    lineNumber++;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error in reading file {ex.Message}");
            }

            return records;
        }

        /// <summary>
        /// Get ordered lines in phone book by criteria
        /// </summary>
        /// <param name="orderBy">Order by field</param>
        /// <param name="sortOrder">Ascending or descending</param>
        /// <returns>Ordered collection</returns>
        /// <exception cref="ArgumentException"></exception>
        public IEnumerable<LineInfo> GetOrderedRecords(Func<LineInfo, string> orderBy, SortOrder sortOrder)
        {
            var records = GetValidRecords();

            return sortOrder switch
            {
                SortOrder.Ascending => records.OrderBy(x => string.IsNullOrEmpty(orderBy(x))).ThenBy(orderBy),
                SortOrder.Descending => records.OrderBy(x => string.IsNullOrEmpty(orderBy(x))).ThenByDescending(orderBy),
                _ => throw new ArgumentException("Invalid sort order specified.")
            };
        }
    }
}
