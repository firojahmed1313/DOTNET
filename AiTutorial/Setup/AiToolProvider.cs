using AiTutorial.AiTools;
using Microsoft.Extensions.AI;

namespace AiTutorial.Setup;

public class AiToolProvider
{
    public IReadOnlyList<AITool> GetTools(
        EmployeeTools employeeTools,
        ProjectTools projectTools,
        OrderTools orderTools)
    {
        return
        [
            AIFunctionFactory.Create(employeeTools.GetEmployeeAsync),
            AIFunctionFactory.Create(employeeTools.SearchEmployeesAsync),

            AIFunctionFactory.Create(projectTools.GetProjectAsync),
            AIFunctionFactory.Create(projectTools.SearchProjectsAsync),

            AIFunctionFactory.Create(orderTools.GetOrderAsync),
            AIFunctionFactory.Create(orderTools.SearchOrdersAsync)
        ];
    }
}
