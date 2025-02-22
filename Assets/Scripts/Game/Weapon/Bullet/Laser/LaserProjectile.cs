public class LaserProjectile : Laser
{
    protected override void Update()
    {
        if (!Host.isActiveAndEnabled)
        {
            InvokeDestroyed(this);
        }
    }
}