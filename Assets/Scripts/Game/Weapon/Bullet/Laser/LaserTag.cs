public class LaserTag : Laser
{
    protected override void Update()
    {
        if (Host) transform.rotation = Host.transform.rotation;

        if (!Host || !Host.isActiveAndEnabled)
        {
            InvokeDestroyed(this);
        }
    }
}