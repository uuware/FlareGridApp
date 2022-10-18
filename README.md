# This project is created with Visual Studio for Mac, built on dotnet 6.0

# This project is to implement a system to track the position of a collection of rectangles on a grid

## How to build:
You can open the solution file with Visual Studio and test it.
Or you can build the project on the command line.
project can be built with `dotnet build` or `dotnet build --configuration Release`

## How to run
For example you can run the debug version.
```
dotnet FlareGridApp/bin/Debug/net6.0/FlareGridApp.dll
```

Then it will show list of commands:
```
e: Exit, n: New game, a: Add rectangles, f: Find a rectangle, r: Remove a rectangle, d: Display
```

### e: Exit
exit the application

### n: New game
The application accepts a size input with `x, y` format, then it will create a new Grid with x as the columns and y as the rows.

### a: Add rectangles
The application will notice you to input a start point and an end point to create a rectangle and put it on the Grid.
If it fails it will show `Rectangle is valid`.

### f: Find a rectangle
Input a point to find a rectangle.
If it found a rectangle, it will show the rectangle's start and end points.

### r: Remove a rectangle
You can input a point to remove it from the Grid.

### d: Display
It shows the rectangles' positions of the Grid

## About the unit test
You can run test cases on Visual Studio's test explorer or on command line with:
```
dotnet test 
```