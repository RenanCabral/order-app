using OrderApp.Service;
using OrderApp.Service.Commands;
using OrderApp.Service.Factories;
using System;
using System.Collections.Generic;
using System.Threading;

namespace OrderApp
{
    class Program
    {
        #region Fields

        private static ICommandService _commandService = null;

        #endregion

        #region Internal Methods
        
        static void Main(string[] args)
        {
            try
            {
                // Creates a command manager instance
                _commandService = CommandServiceFactory.CreateInstance();

                // Display welcome message
                ShowWelcomeMessage();

                while (true)
                {
                    // Gets the user's input
                    InputCommand inputCommand = new InputCommand(Console.ReadLine());

                    // Checks if input command is valid
                    if (!inputCommand.IsValid())
                    {
                        ShowErrorMessage("Command cannot be empty. Please type 'help' for instructions.");
                        continue;
                    }

                    // Check if the command exists
                    Command command = _commandService.GetCommand(inputCommand.CommandName, inputCommand.Parameters);

                    if (command == null)
                    {
                        ShowErrorMessage("Command is not valid. Please type 'help' for instructions.");
                        continue;
                    }

                    Console.WriteLine("\n");

                    // Validates the command's parameters
                    OperationReturn<string> response = command.ValidateParameters();
                    if (response.Success == false)
                    {
                        ShowErrorMessage(response.MessageList);
                        continue;
                    }

                    // Executes the command
                    response = command.Process();
                    if (response.Success == false)
                    {
                        ShowErrorMessage(response.MessageList);
                        continue;
                    }

                    Console.WriteLine(response.Data);
                }
            }
            catch (Exception ex)
            {
                //TODO: Log exception
                ShowErrorMessage($"Internal Server error: {ex.Message}");
                Thread.Sleep(10000);
            }
        }

        #endregion

        #region Private Methods

        private static void ShowWelcomeMessage()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("--- WELCOME TO ORDER APP 1.0 ---");

            Console.ResetColor();

            Console.WriteLine("\n");

            Console.WriteLine("Please type 'Help' for instructions.");

            Console.WriteLine("\n");
        }

        private static void ShowErrorMessage(string errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{errorMessage}\n");
            Console.ResetColor();
        }

        private static void ShowErrorMessage(List<OperationInfo> operationInfoList)
        {
            foreach(OperationInfo operationInfo in operationInfoList)
            {
                ShowErrorMessage($"{operationInfo.Message} - ErrorCode {operationInfo.Code}");
            }
        }

        #endregion
    }
}
