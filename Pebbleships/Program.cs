using System;
using System.Globalization;
using Pebbleships.Ships;

namespace Pebbleships {
    public class Program {
        public static void Main(string[] args) {
            var ships = new IShip[] { new Battleship(), new Destroyer(), new Destroyer() };
            var battlegrid =
                new RandomisedBattlegridBuilder()
                    .Build(10, 10, ships);

            var sunkCount = 0;
            do {
                var line = Console.ReadLine();
                if (line != null && line.Equals("cheat", StringComparison.OrdinalIgnoreCase)) {
                    WriteCheatInfo(battlegrid);
                    continue;
                }

                var position = ParsePosition(line);
                var result = battlegrid.LaunchPebble(position);

                if (result == StrikeResult.Hit)
                    Console.WriteLine("Your pebble scored a hit!");

                if (result == StrikeResult.FullySunk) {
                    sunkCount++;
                    Console.WriteLine("Rejoice commander! You've sunk a ship by throwing pebbles at it!");
                }

                if (result == StrikeResult.Miss)
                    Console.WriteLine("We're throwing pebbles whilst blind, commander!");

            } while (sunkCount < ships.Length);

            Console.WriteLine("You win!!!");
            Console.ReadKey();
        }

        private static void WriteCheatInfo(Battlegrid battlegrid) {
            foreach (var ship in battlegrid.Ships)
                Console.WriteLine(
                    "{0} x {1} : {2}",
                    (char)(ship.Position.X + 65),
                    ship.Position.Y,
                    ship.Orientation);
        }

        private static Position ParsePosition(string line) {
            if (line == null || line.Length != 2)
                throw new InvalidOperationException(
                    "You must type a position in the format of A0 where A is the horizontal column and 0 is the vertical row.");

            var x = char.ToUpperInvariant(line[0]) - 65;
            var y = int.Parse(line[1].ToString(CultureInfo.InvariantCulture));
            return new Position(x, y);
        }
    }
}
