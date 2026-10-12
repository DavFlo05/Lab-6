namespace Zork
{
    public class Room
    {
        // Room information.
        public string Name { get; }
        public string Description { get; set; }

        // Initialize a room with a name and optional description.
        public Room(string name, string description = "")
        {
            Name = name;
            Description = description;
        }

        // Display the room's name instead of "Zork.Room".
        public override string ToString()
        {
            return Name;
        }
    }
}
