namespace Objects
{
    public class ExampleBullet : PooledObject
    {
        // Bullet-specific logic here, such as movement, collision, etc.

        public override void ResetObject()
        {
            base.ResetObject();
            // Reset bullet-specific properties like velocity, position, etc.
        }
    }
}