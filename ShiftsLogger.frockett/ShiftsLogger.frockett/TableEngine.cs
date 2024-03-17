﻿using ShiftsLogger.frockett.UI.Dtos;
using Spectre.Console;

namespace ShiftsLogger.frockett.UI;

public class TableEngine
{
    public void PrintShifts(List<ShiftDto> shifts)
    {
        Table table = new Table();
        table.Alignment(Justify.Center);
        table.Title("Shifts");
        table.AddColumns(new[] { "System ID", "Employee", "Start", "End", "Duration" });

        foreach(ShiftDto shift in shifts)
        {
            table.AddRow(shift.Id.ToString(), shift.EmployeeName, shift.StartTime.ToString(), shift.EndTime.ToString(), shift.Duration.ToString());
        }

        AnsiConsole.Write(table);
    }

    public EmployeeDto SelectEmployeeFromList(List<EmployeeDto> employees)
    {
        AnsiConsole.Clear();

        if(!employees.Any())
        {
            AnsiConsole.MarkupLine("[red]No Employees Found.[/]");
            return null;
        }

        var employeeSelection = new SelectionPrompt<EmployeeDto>();
        employeeSelection.AddChoices(employees);
        employeeSelection.Title("Select Employee");

        EmployeeDto selectedEmployee = AnsiConsole.Prompt(employeeSelection);

        return selectedEmployee;
    }
}
