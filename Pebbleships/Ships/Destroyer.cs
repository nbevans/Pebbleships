using System;

namespace Pebbleships.Ships {
    public class Destroyer : IShip {
        public int Length { get { return 4; } }
        public Orientation Orientation { get; set; }
        public Position Position { get; set; }
        public Battlegrid Owner { get; set; }
        public int Damage { get; set; }
    }
}
