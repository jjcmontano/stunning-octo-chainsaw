using Microsoft.Extensions.Hosting;
using Robots.Model;
using Robots.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Robots.Services
{
    /// <summary>
    /// Console interface command handler
    /// </summary>
    public class CommandService : ICommandService
    {
        private readonly Regex _commandRegex = new Regex(@"(\w+) *\( *(?:(\d+) *, *(\d+) *, *(\w+))? *\) *", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private readonly ITableTopService _tableTopService;

        public CommandService(ITableTopService tableTopService)
        {
            this._tableTopService = tableTopService;
        }

        public void RunCommandLoop()
        {
            Command command;
            do
            {
                Console.Write("Enter command: ");
                var inputText = Console.ReadLine() ?? string.Empty;

                command = ProcessCommand(inputText);
            } while (command != Command.EXIT);
        }

        public Command ProcessCommand(string inputText)
        {
            var commandComponents = _commandRegex.Match(inputText);

            if (commandComponents.Success && commandComponents.Groups.Count >= 2)
            {
                var commandText = commandComponents.Groups[1].Value;
                var parameters = commandComponents.Groups.Values.Skip(2).Select(c => c.Value).ToList();

                if (Enum.TryParse<Command>(commandText, ignoreCase: true, out var command))
                {
                    switch (command)
                    {
                        case Command.NONE:
                            PrintHelp();
                            break;
                        case Command.PLACE:
                            Place(parameters);
                            break;
                        case Command.MOVE:
                            Move();
                            break;
                        case Command.LEFT:
                            Left();
                            break;
                        case Command.RIGHT:
                            Right();
                            break;
                        case Command.REPORT:
                            Report();
                            break;
                        case Command.EXIT:
                            Exit();
                            break;
                        default:
                            break;
                    }
                }
                return command;
            }
            else
            {
                Console.WriteLine($"{inputText}: Unrecognised command or invalid syntax, e.g. missing parentheses.");
                PrintHelp();
                return Command.NONE;
            }
        }

        private void PrintHelp()
        {
            Console.WriteLine("Command help:");
            Console.WriteLine("NOTE: Command validation is case-insensitive and whitespace is ignored, but expects valid parentheses and parameters otherwise.");
            Console.WriteLine("\tplace(<x coordinate>,<y coordinate>,<direction: north/south/east/west>): Place robot at (x,y) facing direction north/south/east/west");
            Console.WriteLine("\tmove(): Move robot 1 position in the direction it is facing");
            Console.WriteLine("\tleft(): Rotate robot 90° counterclockwise");
            Console.WriteLine("\tright(): Rotate robot 90° clockwise");
            Console.WriteLine("\treport(): Print current robot position and direction it is facing");
            Console.WriteLine("\texit(): End program");
        }

        private void Place(List<string> parameters)
        {
            if (parameters.Count == 3 &&
                int.TryParse(parameters[0], out var x) &&
                int.TryParse(parameters[1], out var y) &&
                Enum.TryParse<Direction>(parameters[2], ignoreCase: true, out var direction))
            {
                Console.WriteLine(_tableTopService.Place(x, y, direction) ? "Success" : "Failed");
            }
            else
            {
                Console.WriteLine("Failed: Place command has invalid parameters");
            }
        }

        private void Move()
        {
            Console.WriteLine(_tableTopService.Move() ? "Success" : "Failed");
        }

        private void Left()
        {
            Console.WriteLine(_tableTopService.Left() ? "Success" : "Failed");
        }

        private void Right()
        {
            Console.WriteLine(_tableTopService.Right() ? "Success" : "Failed");
        }

        private void Report()
        {
            Console.WriteLine(_tableTopService.Report());
        }

        private void Exit()
        {
        }
    }
}
