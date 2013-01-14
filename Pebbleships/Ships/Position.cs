using System;

namespace Pebbleships.Ships {
    public struct Position {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Position(int x, int y)
            : this() {

            X = x;
            Y = y;

            ThrowIfCoordsAreNegative(x, y);
        }

        private static void ThrowIfCoordsAreNegative(int x, int y) {
            if (x < 0)
                throw new ArgumentOutOfRangeException("x", "Must be greater than zero.");

            if (y < 0)
                throw new ArgumentOutOfRangeException("y", "Must be greater than zero.");
        }
    }
}