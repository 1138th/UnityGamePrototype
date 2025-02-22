public class LaserTag : Laser
{
    protected override void Update()
    {
        transform.rotation = Host.transform.rotation;

        if (!Host.isActiveAndEnabled)
        {
            InvokeDestroyed(this);
        }
    }
}