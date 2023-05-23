using PhoneBookConsoleApp.Constants;
using PhoneBookConsoleApp.Models;
using PhoneBookConsoleApp.Services;


string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
ConsoleKeyInfo keyInfo;
do
{

    string absolutePath = string.Empty;

    while (true)
    {
        Console.Write("Enter the file path { FolderName/fileName.txt }: ");
        Console.ForegroundColor = ConsoleColor.Green;
        string? filePath = Console.ReadLine();
        filePath = filePath?.Trim();

        if (string.IsNullOrWhiteSpace(filePath))
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Please input a valid file name.");
            continue;
        }

        absolutePath = Path.Combine(baseDirectory, filePath);

        if (File.Exists(absolutePath))
            break;
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Specified file does not exist");
        }
    }
    Console.ResetColor();
    Console.WriteLine("Absolute file path: " + absolutePath);

    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine("Please choose the sort order.");
    Console.WriteLine("1. Ascending");
    Console.WriteLine("2. Descending");

    SortOrder sortOrder = default;
    string? input;

    bool isInputValid = true;
    do
    {
        Console.ForegroundColor = ConsoleColor.Green;
        input = Console.ReadLine();
        Console.ResetColor();
        switch (input)
        {
            case "1":
            case "Ascending" or "ascending":
                sortOrder = SortOrder.Ascending;
                isInputValid = true;
                break;
            case "2":
            case "Descending" or "descending":
                sortOrder = SortOrder.Descending;
                isInputValid = true;
                break;
            default:
                Console.WriteLine("Invalid input. Please type again.");
                isInputValid = false;
                break;
        }
    } while (!isInputValid);

    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine("Please choose the criteria.");
    Console.WriteLine("1. Name");
    Console.WriteLine("2. Surname");
    Console.WriteLine("3. Phone");

    Func<LineInfo, string>? orderBy = default;
    do
    {
        Console.ForegroundColor = ConsoleColor.Green;
        input = Console.ReadLine();
        Console.ResetColor();
        switch (input)
        {
            case "1":
            case "Name" or "name":
                orderBy = x => x.FirstName;
                isInputValid = true;
                break;
            case "2":
            case "Surname" or "surname":
                orderBy = x => x.LastName;
                isInputValid = true;
                break;
            case "3":
            case "Phone" or "phone":
                orderBy = x => x.Phone;
                isInputValid = true;
                break;
            default:
                Console.WriteLine("Invalid input. Please type again.");
                isInputValid = false;
                break;
        }
    } while (!isInputValid);

    PhoneBookService service = new(absolutePath);

    List<LineInfo> records = service.GetOrderedRecords(orderBy!, sortOrder).ToList();

    Console.WriteLine();
    Console.WriteLine("Valid records are:");

    foreach (var item in records)
    {
        Console.WriteLine(item.ToString());
    }

    var validations = service.GetValidations();
    Console.WriteLine();
    Console.WriteLine("Validations:");

    foreach (var data in validations)
    {
        if (data.Value.Length > 1)
            Console.WriteLine($"Line {data.Key}: {data.Value[0]}, {data.Value[1]} ");
        else
            Console.WriteLine($"Line {data.Key}: {data.Value[0]}");
    }


    Console.WriteLine();
    Console.WriteLine("Press 'e' or 'esc' to exit, or any other key to continue...");

    keyInfo = Console.ReadKey(true);
} while (keyInfo.Key != ConsoleKey.Escape && keyInfo.KeyChar != 'e');