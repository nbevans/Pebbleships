using System;

namespace Pebbleships {
    public class BattlegridException : ApplicationException {
        public BattlegridException(string message) : base(message) { }
        public BattlegridException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class ShipConflictBattlegridException : BattlegridException {
        public ShipConflictBattlegridException() : base("The ship conflicts with an existing ship on the battlegrid.") { }
    }

    public class ShipOffTheBattlegridException : BattlegridException {
        public ShipOffTheBattlegridException() : base("The ship position is either fully or partially off of the battlegrid.") { }
    }

    public class ShipOwnedByAnotherBattlegridException : BattlegridException {
        public ShipOwnedByAnotherBattlegridException() : base("The ship is already assigned to another battlegrid.") { }
    }
}
