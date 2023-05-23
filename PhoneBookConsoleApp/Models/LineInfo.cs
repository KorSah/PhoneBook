namespace PhoneBookConsoleApp.Models;

public class LineInfo
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Separator { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public override string ToString() => $"{FirstName} {LastName} {Separator} {Phone}";
}
