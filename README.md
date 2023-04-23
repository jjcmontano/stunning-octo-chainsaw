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
* `place <x coordinate> <y coordinate> <direction: n/s/e/w>`: Place robot at (x, y) facing direction north/south/east/west
* `move`: Move robot 1 position in the direction it is facing
* `left`: Rotate robot 90° counterclockwise
* `right`: Rotate robot 90° clockwise
* `report`: Print current robot position and direction it is facing
* `exit`: End program