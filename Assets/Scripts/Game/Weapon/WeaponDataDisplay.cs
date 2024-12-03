using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDataDisplay : MonoBehaviour
{
    private List<WeaponData> weaponsList = new List<WeaponData>();
    private int currentWeapon;

    private WeaponData weaponData;
    private Text damageText;
    private Text projectilesText;
    private Text ammoText;
    private Text attackSpeedText;
    private Text reloadTimeText;
    private Text penetrationPowerText;
    private Image weaponSprite;

    private void Awake()
    {
        weaponsList.Add(new AssaultRifleData());
        weaponsList.Add(new ShotgunData());
        weaponsList.Add(new RevolverData());

        weaponData = weaponsList[currentWeapon];

        GameObject numbersObject = transform.Find("DataNumbers").gameObject;
        damageText = numbersObject.transform.Find("Damage").GetComponent<Text>();
        projectilesText = numbersObject.transform.Find("Projectiles").GetComponent<Text>();
        ammoText = numbersObject.transform.Find("Ammo").GetComponent<Text>();
        attackSpeedText = numbersObject.transform.Find("AttackSpeed").GetComponent<Text>();
        reloadTimeText = numbersObject.transform.Find("ReloadTime").GetComponent<Text>();
        penetrationPowerText = numbersObject.transform.Find("PenetrationPower").GetComponent<Text>();
        weaponSprite = transform.Find("WeaponSprite").GetComponent<Image>();
    }

    private void Update()
    {
        damageText.text = weaponData.Damage.ToString();
        projectilesText.text = weaponData.ProjectilesCount.ToString();
        ammoText.text = weaponData.AmmoCount.ToString();
        attackSpeedText.text = weaponData.AttackSpeed.ToString();
        reloadTimeText.text = weaponData.ReloadTime.ToString();
        penetrationPowerText.text = weaponData.PenetrationPower.ToString();
        weaponSprite.sprite = weaponData.WeaponSprite;
    }

    public void ClickNextWeapon()
    {
        if (currentWeapon == weaponsList.Count - 1)
        {
            currentWeapon = -1;
        }
        weaponData = weaponsList[++currentWeapon];
    }

    public void ClickPrevWeapon()
    {
        if (currentWeapon == 0)
        {
            currentWeapon = weaponsList.Count;
        }
        weaponData = weaponsList[--currentWeapon];
    }

    public void SaveWeaponData()
    {
        MetaManager.Instance.WeaponData = weaponData;
    }
}