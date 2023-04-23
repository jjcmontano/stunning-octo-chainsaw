using Microsoft.Extensions.Hosting;
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
            Console.WriteLine("\tplace <x coordinate> <y coordinate> <direction: n/s/e/w>: Place robot at (x, y) facing direction n/s/e/w");
            Console.WriteLine("\tmove: Move robot 1 position in the direction it is facing");
            Console.WriteLine("\tleft: Rotate robot 90° counterclockwise");
            Console.WriteLine("\tright: Rotate robot 90° clockwise");
            Console.WriteLine("\treport: Print current robot position and direction it is facing");
            Console.WriteLine("\texit: End program");
        }

        private void Place(List<string> parameters)
        {
            throw new NotImplementedException();
        }

        private void Move()
        {
            throw new NotImplementedException();
        }

        private void Left()
        {
            throw new NotImplementedException();
        }

        private void Right()
        {
            throw new NotImplementedException();
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
