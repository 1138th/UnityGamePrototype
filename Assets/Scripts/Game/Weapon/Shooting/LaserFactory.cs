using System.Collections.Generic;
using UnityEngine;

public class LaserFactory : MonoBehaviour
{
    [SerializeField] private Laser laserPrefab;

    private List<Laser> activeLasers = new();
    private Queue<Laser> disabledLasers = new();

    public Laser GetLaser(Character shooter)
    {
        Laser laser = null;

        if (disabledLasers.Count > 0)
        {
            laser = disabledLasers.Dequeue();
            laser.transform.position = GetLaserDefaultPosition(shooter);
            laser.transform.rotation = shooter.transform.rotation;
        }

        if (laser == null)
        {
            laser = Instantiate(laserPrefab, GetLaserDefaultPosition(shooter), shooter.transform.rotation);
        }

        activeLasers.Add(laser);

        return laser;
    }

    public void ReturnLaser(Laser laser)
    {
        laser.gameObject.SetActive(false);
        Queue<Laser> lasers = disabledLasers;
        lasers.Enqueue(laser);

        activeLasers.Remove(laser);
    }

    private Vector3 GetLaserDefaultPosition(Character shooter)
    {
        return new Vector3(shooter.transform.position.x, shooter.transform.position.y + 0.7f, shooter.transform.position.z);
    }
}