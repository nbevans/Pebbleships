using System;
using System.Collections.Generic;

namespace Pebbleships {
    using Ships;

    public class RandomisedBattlegridBuilder : IBattlegridBuilder {
        private const int MaxAttempts = 10000;
        private readonly Random _prng = new Random();

        public Battlegrid Build(int width, int height, IEnumerable<IShip> ships) {
            var battlegrid = new Battlegrid(width, height);
            var map = new BattlegridMap(battlegrid.Width, battlegrid.Height);

            foreach (var ship in ships) {
                var attempts = 0;
                do {
                    Relocate(ship, battlegrid.Width, battlegrid.Height);

                    if (attempts++ > MaxAttempts)
                        throw new BattlegridException(
                            "Gave up whilst trying to randomly place the ship on the battlegrid. " +
                            "There is probably no viable positions left.");

                } while (map.TryAdd(ship) != HitType.None);

                battlegrid.Add(ship);
            }

            return battlegrid;
        }

        private void Relocate(IShip ship, int width, int height) {
            ship.Position = new Position(_prng.Next(width), _prng.Next(height));
            ship.Orientation = (Orientation)_prng.Next(2);
        }
    }
}
