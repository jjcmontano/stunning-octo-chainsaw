using Microsoft.Extensions.Hosting;
using Robots.Model;
using Robots.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robots.Services
{
    /// <summary>
    /// Console interface command handler
    /// </summary>
    public class CommandService : ICommandService
    {
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
            var commandComponents = inputText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var commandText = commandComponents.FirstOrDefault();
            var parameters = commandComponents.Skip(1).ToList();

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
            else
            {
                Console.WriteLine($"{inputText}: Unrecognised command");
                PrintHelp();
                return Command.NONE;
            }

            return command;
        }

        private void PrintHelp()
        {
            Console.WriteLine("Command help:");
            Console.WriteLine("\tplace <x coordinate> <y coordinate> <direction: north/south/east/west>: Place robot at (x, y) facing direction n/s/e/w");
            Console.WriteLine("\tmove: Move robot 1 position in the direction it is facing");
            Console.WriteLine("\tleft: Rotate robot 90° counterclockwise");
            Console.WriteLine("\tright: Rotate robot 90° clockwise");
            Console.WriteLine("\treport: Print current robot position and direction it is facing");
            Console.WriteLine("\texit: End program");
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
                Console.WriteLine("Failed");
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
