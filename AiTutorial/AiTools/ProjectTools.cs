namespace AiTutorial.AiTools;

public class ProjectTools
{
    public async Task<string> GetProjectAsync(int projectId)
    {
        await Task.Delay(10);

        return projectId switch
        {
            1 => "Payment API - ASP.NET Core - Production",
            2 => "Employee Portal - React + ASP.NET Core - Development",
            3 => "Order Management - .NET + SQL - Production",
            _ => "Project not found"
        };
    }

    public async Task<List<string>> SearchProjectsAsync(string skill)
    {
        await Task.Delay(10);

        var projects = new List<string>
        {
            "Payment API - C#, ASP.NET Core, Redis",
            "Employee Portal - React, JavaScript, ASP.NET Core",
            "Order Management - C#, SQL, RabbitMQ",
            "Analytics Platform - Python, Databricks"
        };

        return projects
            .Where(x => x.Contains(skill, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}
