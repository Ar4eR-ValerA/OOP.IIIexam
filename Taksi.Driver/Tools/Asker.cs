using System;
using System.Collections.Generic;
using System.Linq;
using Spectre.Console;
using Taksi.Server.DAL.Exceptions;

namespace Taksi.Driver.Tools
{
    public class Asker
    {
        public T AskChoices<T>(string message, IEnumerable<T> choices)
        {
            IEnumerable<T> enumerable = choices.ToList();
            if (!enumerable.Any())
            {
                throw new DriverExceptions("There are no objects for choice.");
            }

            AnsiConsole.Write(message + "\n\n");
            return AnsiConsole.Prompt(new SelectionPrompt<T>()
                .AddChoices(enumerable));
        }

        public string AskChoices(string message, string choice)
        {
            if (!choice.Any())
            {
                throw new DriverExceptions("There are no objects for choice");
            }

            AnsiConsole.Write(message + "\n\n");
            return AnsiConsole.Prompt(new SelectionPrompt<string>()
                .AddChoices(choice));
        }

        public void AskExit(string message)
        {
            AnsiConsole.Write(message + "\n\n");
            AnsiConsole.Prompt(new SelectionPrompt<string>()
                .AddChoices("exit"));
        }

        public int AskInt(string message)
        {
            return AnsiConsole.Prompt(
                new TextPrompt<int>(message + "\n")
                    .Validate(value =>
                    {
                        return value switch
                        {
                            < 0 => ValidationResult.Error("[red]Value must be positive[/]"),
                            _ => ValidationResult.Success(),
                        };
                    }));
        }

        public string AskString(string message)
        {
            return AnsiConsole.Ask<string>(message + "\n");
        }

        public Guid AskGuid(string message)
        {
            return AnsiConsole.Ask<Guid>(message + "\n");
        }
    }
}