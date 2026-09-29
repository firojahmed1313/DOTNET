namespace AiTutorial.AiTools;

public class EmployeeTools
{
    public async Task<string> GetEmployeeAsync(int employeeId)
    {
        // Normally: query database

        await Task.Delay(10);

        return $"Employee {employeeId}: MD Firoj Ahmed, Backend Developer";
    }

    public async Task<List<string>> SearchEmployeesAsync(string skill)
    {
        await Task.Delay(10);

        var employees = new List<string>
        {
            "Firoj - C#, ASP.NET Core, SQL",
            "Rahul - C#, Redis, RabbitMQ",
            "Amit - Java, Spring Boot",
            "John - React, JavaScript"
        };

        return employees
            .Where(x => x.Contains(skill, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}
