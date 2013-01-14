namespace Pebbleships {
    using Ships;

    public interface IHitTestable {
        HitType HitTest(Position position, bool recordHit, out IShip ship);
    }
}