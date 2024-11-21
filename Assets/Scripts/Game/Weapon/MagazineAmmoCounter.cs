using UnityEngine;
using UnityEngine.UI;

public class MagazineAmmoCounter : MonoBehaviour
{
    [SerializeField] private Text currentMagazineAmmoAmount;
    [SerializeField] private ShootingController shootingController;

    public void Update()
    {
        int bulletsRemain = WeaponData.AmmoCount - shootingController.BulletsShot;
        currentMagazineAmmoAmount.text = $"{bulletsRemain} / {WeaponData.AmmoCount}";
    }
}