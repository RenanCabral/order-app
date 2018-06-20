using System;

namespace OrderApp
{
    public class InputCommand
    {
        #region Constructors

        public InputCommand(string inputLine)
        {
            string[] arguments = GetArguments(inputLine);

            MapCommandParameters(arguments);

            this.CommandName = GetCommandName(arguments);
        }

        #endregion

        #region Fields

        private const char ARGUMENT_SEPARATOR = ' ';
        
        #endregion

        #region Properties
        
        public string CommandName { get; }

        public string[] Parameters { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Indicates if a command was typed.
        /// </summary>
        /// <returns>Indicates</returns>
        public bool IsValid()
        {
            return !string.IsNullOrEmpty(CommandName);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the command's name from the arguments.
        /// </summary>
        /// <param name="arguments">Arguments as array of strings.</param>
        /// <returns>Typed command's name.</returns>
        private string GetCommandName(string[] arguments)
        {
            return arguments[0];
        }

        /// <summary>
        /// Maps the command's parameters into 'Parameters' property. 
        /// Basically maps all the arguments except the typed command.
        /// </summary>
        /// <param name="arguments">Arguments as array of strings.</param>
        private void MapCommandParameters(string[] arguments)
        {
            this.Parameters = new string[arguments.Length - 1];
            Array.Copy(arguments, 1, this.Parameters, 0, arguments.Length - 1);
        }

        /// <summary>
        /// Gets the arguments input by user.
        /// </summary>
        /// <param name="inputLine">Raw text typed by user.</param>
        /// <returns>Arguments split into array of strings.</returns>
        private string[] GetArguments(string inputLine)
        {
            return inputLine.Split(ARGUMENT_SEPARATOR);
        }

        #endregion
    }
}
