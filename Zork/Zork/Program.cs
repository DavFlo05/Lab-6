using System;
using System.Collections.Generic;

namespace Zork
{
    internal class Program
    {
        // Our 2D game world.
        // Row increases when moving NORTH.
        // Column increases when moving EAST.
        private static readonly Room[,] _rooms =
        {
            {
                new Room("Rocky Trail"),
                new Room("Forest"),
                new Room("Canyon View")
            },
            {
                new Room("Forest Path"),
                new Room("West of House"),
                new Room("Behind House")
            },
            {
                new Room("Deep Forest"),
                new Room("North of House"),
                new Room("Clearing")
            }
        };

        // Player starts at West of House.
        private static (int Row, int Column) _location = (1, 1);

        // Valid movement commands.
        private static readonly List<Commands> _directions = new List<Commands>
        {
            Commands.NORTH,
            Commands.SOUTH,
            Commands.EAST,
            Commands.WEST
        };

        // Return the room at the player's current location.
        private static Room CurrentRoom
        {
            get
            {
                return _rooms[_location.Row, _location.Column];
            }
        }

        static void Main(string[] args)
        {
            // Set up room descriptions before starting.
            InitializeRoomDescriptions();

            Console.WriteLine("Welcome to Zork!");

            // Track the previous room for auto-look.
            Room previousRoom = null;

            // Keep playing until QUIT is entered.
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine(CurrentRoom);

                // Automatically describe newly entered rooms.
                if (CurrentRoom != previousRoom)
                {
                    Console.WriteLine(CurrentRoom.Description);
                    previousRoom = CurrentRoom;
                }

                // Ask the player for a command.
                Console.Write("> ");

                string inputString = Console.ReadLine();

                // Convert the player's input into a command.
                Commands command = ToCommand(inputString);

                switch (command)
                {
                    case Commands.LOOK:
                        Console.WriteLine(CurrentRoom.Description);
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:

                        // Attempt movement and report the result.
                        if (Move(command))
                        {
                            Console.WriteLine($"You moved {command}.");
                        }
                        else
                        {
                            Console.WriteLine("The way is shut!");
                        }

                        break;

                    case Commands.QUIT:
                        Console.WriteLine("Thank you for playing!");
                        return;

                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }
            }
        }

        // Convert player input into a Commands enum.
        private static Commands ToCommand(string commandString)
        {
            if (Enum.TryParse<Commands>(
                commandString?.Trim(),
                true,
                out Commands result))
            {
                return result;
            }

            return Commands.UNKNOWN;
        }

        // Check whether a command represents movement.
        private static bool IsDirection(Commands command)
        {
            return _directions.Contains(command);
        }

        // Move the player within the 2D array.
        private static bool Move(Commands command)
        {
            // Verify that a direction was provided.
            Assert.IsTrue(IsDirection(command), "Invalid direction.");

            switch (command)
            {
                case Commands.NORTH
                    when _location.Row < _rooms.GetLength(0) - 1:

                    _location.Row++;
                    return true;

                case Commands.SOUTH
                    when _location.Row > 0:

                    _location.Row--;
                    return true;

                case Commands.EAST
                    when _location.Column < _rooms.GetLength(1) - 1:

                    _location.Column++;
                    return true;

                case Commands.WEST
                    when _location.Column > 0:

                    _location.Column--;
                    return true;

                default:
                    return false;
            }
        }

        // Assign descriptions using room names instead of indices.
        private static void InitializeRoomDescriptions()
        {
            Dictionary<string, Room> roomMap =
                new Dictionary<string, Room>();

            // Add every room to the dictionary.
            foreach (Room room in _rooms)
            {
                roomMap[room.Name] = room;
            }

            // Assign descriptions.
            roomMap["Rocky Trail"].Description =
                "You are walking along a rocky trail.";

            roomMap["Forest"].Description =
                "You are surrounded by tall trees.";

            roomMap["Canyon View"].Description =
                "You stand near the edge of a deep canyon.";

            roomMap["Forest Path"].Description =
                "A narrow path winds through the forest.";

            roomMap["West of House"].Description =
                "This is an open field west of a white house, with a boarded front door.";

            roomMap["Behind House"].Description =
                "You are behind the white house.";

            roomMap["Deep Forest"].Description =
                "The forest grows darker and thicker here.";

            roomMap["North of House"].Description =
                "You are standing north of the white house.";

            roomMap["Clearing"].Description =
                "You enter a peaceful clearing surrounded by trees.";
        }
    }
}