using Robots.Services.Helpers;

namespace Robots.Services
{
    public interface ICommandService
    {
        Command ProcessCommand(string inputText);
        void RunCommandLoop();
    }
}