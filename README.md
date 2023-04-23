# stunning-octo-chainsaw
## Technical challenge

## Pre-requisites:
* .NET 7.0 SDK https://dotnet.microsoft.com/en-us/download/dotnet/7.0

## How to run
* Either open the .\Robots.sln file in Visual Studio 2022 and run the RobotsConsole project as a startup project.

-- OR --

* On Linux, execute `.\RobotsConsole.sh`
* On Windows, execute `.\RobotsConsole.ps1`

## Command help
**NOTE**: Command validation is case-insensitive and whitespace is ignored, but expects valid parentheses and parameters otherwise.
* `place(<x coordinate>,<y coordinate>,<direction: north/south/east/west>)`: Place robot at (x,y) facing direction north/south/east/west
* `move()`: Move robot 1 position forward, in the direction it is currently facing
* `left()`: Rotate robot 90° counterclockwise
* `right()`: Rotate robot 90° clockwise
* `report()`: Print current robot position and direction it is facing
* `exit()`: End program