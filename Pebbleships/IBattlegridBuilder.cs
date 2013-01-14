using System;
using System.Collections.Generic;

namespace Pebbleships {
    using Ships;

    public interface IBattlegridBuilder {
        Battlegrid Build(int width, int height, IEnumerable<IShip> ships);
    }
}