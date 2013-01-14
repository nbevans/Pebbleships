using System;
using System.Collections.Generic;
using System.Linq;

namespace Pebbleships {
    using Ships;

    internal class BattlegridMap : IHitTestable {
        private readonly IShip[,] _map;

        public BattlegridMap(int width, int height) {
            _map = new IShip[width, height];
        }

        public BattlegridMap(int width, int height, IEnumerable<IShip> ships) {
            _map = new IShip[width, height];

            foreach (var ship in ships)
                Add(ship);
        }

        public HitType HitTest(Position position, bool recordHit, out IShip ship) {
            if (position.X >= _map.GetLength(0) || position.Y >= _map.GetLength(1)) {
                ship = null;
                return HitType.GridBoundaries;
            }

            ship = _map[position.X, position.Y];
            if (ship != null) {
                if (recordHit)
                    _map[position.X, position.Y] = null;

                return HitType.ExistingShip;
            }

            return HitType.None;
        }

        public void Add(IShip ship) {
            switch (TryAddCore(ship)) {
                case HitType.ExistingShip:
                    throw new ShipConflictBattlegridException();
                case HitType.GridBoundaries:
                    throw new ShipOffTheBattlegridException();
                default:
                    return;
            }
        }

        public HitType TryAdd(IShip ship) {
            return TryAddCore(ship);
        }

        private HitType TryAddCore(IShip ship) {
            var cells = GetCells(ship);
            foreach (var cell in cells) {
                IShip notUsed;
                var test = HitTest(cell, false, out notUsed);
                if (test != HitType.None)
                    return test;

                _map[cell.X, cell.Y] = ship;
            }

            return HitType.None;
        }

        private IEnumerable<Position> GetCells(IShip ship) {
            switch (ship.Orientation) {
                case Orientation.Horizontal:
                    return Enumerable
                        .Range(ship.Position.X, ship.Length)
                        .Select(coordX => new Position(coordX, ship.Position.Y));

                case Orientation.Vertical:
                    return Enumerable
                        .Range(ship.Position.Y, ship.Length)
                        .Select(coordY => new Position(ship.Position.X, coordY));

                default:
                    throw new InvalidOperationException("The orientation of the ship is invalid.");
            }
        }
    }
}
