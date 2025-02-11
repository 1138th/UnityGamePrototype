using UnityEngine;
using UnityEngine.UI;

public class MagazineAmmoCounter : MonoBehaviour
{
    [SerializeField] private Text currentMagazineAmmoAmount;
    [SerializeField] private PlayerShootingController shootingController;

    public void Update()
    {
        int bulletsRemain = shootingController.WeaponData.AmmoCount - shootingController.BulletsShot;
        currentMagazineAmmoAmount.text = $"{bulletsRemain} / {shootingController.WeaponData.AmmoCount}";
    }
}