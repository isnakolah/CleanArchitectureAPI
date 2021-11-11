namespace Domain.ValueObjects;

public class CalculateAge
{
    public static int GetCurrentAge(DateTime DOB)
    {
        var today = DateTime.Today;
        var age = today.Year - DOB.Year;

        // Check if leap year
        return (DOB.Date > today.AddYears(-age)) ? age-- : age;
    }
}
