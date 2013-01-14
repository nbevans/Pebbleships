using System;

namespace Pebbleships.Ships {
    public interface IShip {
        int Length { get; }
        Orientation Orientation { get; set; }
        Position Position { get; set; }
        Battlegrid Owner { get; set; }
        int Damage { get; set; }
    }
}
