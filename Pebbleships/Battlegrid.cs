using System;
using System.Collections.Generic;

namespace Pebbleships {
    using Ships;

    public class Battlegrid {
        private readonly List<IShip> _ships = new List<IShip>();
        private readonly BattlegridMap _map;

        public int Width { get; private set; }

        public int Height { get; private set; }

        public IEnumerable<IShip> Ships { get { return _ships; } }

        public Battlegrid(int width, int height) {
            Width = width;
            Height = height;
            ThrowIfGridSizeIsInvalid();

            _map = new BattlegridMap(width, height);
        }

        public StrikeResult LaunchPebble(Position position) {
            IShip damagedShip;
            if (_map.HitTest(position, true, out damagedShip) == HitType.ExistingShip) {
                if (damagedShip.Damage < damagedShip.Length)
                    damagedShip.Damage++;

                if (damagedShip.Damage >= damagedShip.Length)
                    return StrikeResult.FullySunk;

                return StrikeResult.Hit;
            }

            return StrikeResult.Miss;
        }

        public Battlegrid Add(IShip ship, Position position, Orientation orientation) {
            if (ship == null) throw new ArgumentNullException("ship");
            if (ship.Owner != null) throw new ShipOwnedByAnotherBattlegridException();
            ship.Position = position;
            ship.Orientation = orientation;
            Add(ship);
            return this;
        }

        public Battlegrid Add(IShip ship) {
            if (ship == null) throw new ArgumentNullException("ship");
            if (ship.Owner != null) throw new ShipOwnedByAnotherBattlegridException();
            _map.Add(ship);
            _ships.Add(ship);
            ship.Owner = this;
            return this;
        }

        private void ThrowIfGridSizeIsInvalid() {
            if (Width < 10 || Width > 100)
                throw new BattlegridException("The battlegrid must have a width between 10 and 100.");

            if (Height < 10 || Height > 100)
                throw new BattlegridException("The battlegrid must have a height between 10 and 100.");
        }
    }
}
