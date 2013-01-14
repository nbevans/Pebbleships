using System;

using NUnit.Framework;

namespace Pebbleships {
    using Ships;

    [TestFixture]
    public class BattlegridTests {

        [Test]
        public void Given_a_10_by_10_grid_when_two_ships_added_with_overlapping_cells_then_expect_a_throw() {
            Assert.Throws<ShipConflictBattlegridException>(
                () => new Battlegrid(10, 10)
                          .Add(new Battleship(), new Position(3, 3), Orientation.Horizontal)
                          .Add(new Destroyer(), new Position(5, 1), Orientation.Vertical));
        }

        [Test]
        public void Given_a_10_by_10_grid_when_a_ship_is_added_that_is_out_of_those_bounds_then_expect_a_throw() {
            Assert.Throws<ShipOffTheBattlegridException>(
                () => new Battlegrid(10, 10)
                          .Add(new Battleship(), new Position(8, 5), Orientation.Horizontal));

            Assert.Throws<ShipOffTheBattlegridException>(
                () => new Battlegrid(10, 10)
                          .Add(new Battleship(), new Position(5, 8), Orientation.Vertical));
        }

        [Test]
        public void Given_a_10_by_10_grid_when_a_ship_is_added_and_pebbles_thrown_at_it_then_expect_hits_and_eventually_a_sinking() {
            var battlegrid =
                new Battlegrid(10, 10)
                    .Add(new Destroyer(), new Position(4, 4), Orientation.Horizontal);

            Assert.That(battlegrid.LaunchPebble(new Position(4, 4)) == StrikeResult.Hit);
            Assert.That(battlegrid.LaunchPebble(new Position(5, 4)) == StrikeResult.Hit);
            Assert.That(battlegrid.LaunchPebble(new Position(6, 4)) == StrikeResult.Hit);
            Assert.That(battlegrid.LaunchPebble(new Position(7, 4)) == StrikeResult.FullySunk);
        }
    }
}
