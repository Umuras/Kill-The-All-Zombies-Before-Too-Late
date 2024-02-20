using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponModel : IWeaponModel
{
    [Inject]
    public IObjectPoolingModel objectPoolingModel { get; set; }
    [Inject]
    public IPlayerAndWeaponUIModel playerAndWeaponUIModel { get; set; }
    [Inject]
    public ITargetModel targetModel { get; set; }
    [Inject]
    public IEnemyModel enemyModel { get; set; }

    public List<WeaponData> weaponData { get; set; }
    public AudioSource fireAudioSource { get; set; }
    public List<Transform> weaponMuzzleTransform { get; set; }

    public int weaponIndex { get; set; }

    public int pistolDamagePower { get; set; }
    public int pistolMagCapacity { get; set; }
    public int totalPistolMagInside { get; set; }
    public int pistolMagCount { get; set; }
    public int totalPistolAmmo { get; set; }
    public int pistolShootRange { get; set; }
    public ParticleSystem pistolParticleSystem { get; set; }

    public int rifleDamagePower { get; set; }
    public int rifleMagCapacity { get; set; }
    public int rifleMagCount { get; set; }
    public int totalRifleMagInside { get; set; }
    public int totalRifleAmmo { get; set; }
    public int rifleShootRange { get; set; }

    public Animation fireAnimation { get; set; }
    public ParticleSystem rifleParticleSystem { get; set; }

    public bool reloading { get; set; }

    public bool isWeaponIncreaseDamage { get; set; }

    private int _bulletSpeed = 100;

    private string _tagEnemy = "Enemy";
    private string _tagTarget = "Target";


    [PostConstruct]
    public void GetWeaponData()
    {
        weaponData = Resources.Load<CD_Weapon>("Data/CD_Weapon").WeaponData;
    }

    public void InitializeWeaponsProperties(AudioSource fireAudioSource, AudioSource reloadAudioSource, Animation fireAnimation, WeaponKeys firstWeapon, List<Transform> weaponMuzzleTransform, ParticleSystem pistolParticle, ParticleSystem rifleParticle)
    {
        pistolDamagePower = weaponData[(int)WeaponKeys.Pistol].weapon.damagePower;
        pistolMagCapacity = weaponData[(int)WeaponKeys.Pistol].weapon.magCapacity;
        pistolMagCount = weaponData[(int)WeaponKeys.Pistol].weapon.magCount;
        pistolShootRange = weaponData[(int)WeaponKeys.Pistol].weapon.shootRange;
        totalPistolMagInside = pistolMagCapacity;

        rifleDamagePower = weaponData[(int)WeaponKeys.Rifle].weapon.damagePower;
        rifleMagCapacity = weaponData[(int)WeaponKeys.Rifle].weapon.magCapacity;
        rifleMagCount = weaponData[(int)WeaponKeys.Rifle].weapon.magCount;
        rifleShootRange = weaponData[(int)WeaponKeys.Rifle].weapon.shootRange;
        totalRifleMagInside = rifleMagCapacity;

        totalPistolAmmo = pistolMagCapacity * pistolMagCount;
        totalRifleAmmo = rifleMagCapacity * rifleMagCount;

        this.fireAnimation = fireAnimation;
        this.weaponMuzzleTransform = weaponMuzzleTransform;
        this.fireAudioSource = fireAudioSource;
        pistolParticleSystem = pistolParticle;
        rifleParticleSystem = rifleParticle;

        weaponIndex = (int)firstWeapon;
        fireAudioSource.clip = weaponData[weaponIndex].weapon.fire;
        reloadAudioSource.clip = weaponData[weaponIndex].weapon.realod;
        fireAnimation.AddClip(weaponData[0].weapon.fireAnimationClip, WeaponKeys.Pistol.ToString());
        fireAnimation.AddClip(weaponData[1].weapon.fireAnimationClip, WeaponKeys.Rifle.ToString());

    }

    public void RaycastForWeapon(Transform muzzlePosition, float shootRange)
    {
        Vector3 crosshairPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = Camera.main.ScreenPointToRay(crosshairPosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 30, Color.blue);
       
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction*shootRange, color: Color.red);

        if (Physics.Raycast(ray, out hit, shootRange))
        {
            if (hit.collider.gameObject.CompareTag(_tagTarget))
            {
                TargetView targetGoView = hit.collider.gameObject.GetComponent<TargetView>();
                if (targetGoView != null)
                {
                    targetModel.DecrasingTargetHealthAndKillTarget(targetView: targetGoView);
                } 
            }
            else if (hit.collider.gameObject.CompareTag(_tagEnemy))
            {
                EnemyView enemyView = hit.collider.gameObject.GetComponent<EnemyView>();
                if (weaponIndex == (int)WeaponKeys.Pistol)
                {
                    enemyModel.DecreasingEnemyHealth(pistolDamagePower,enemyView);
                }
                else
                {
                    enemyModel.DecreasingEnemyHealth(rifleDamagePower, enemyView);
                }
            }
        }  
    }

    public void FireBullet(Transform muzzlePosition, WeaponKeys weaponKey)
    {
        GameObject bullet = objectPoolingModel.DequeuePoolableGameObject();
        bullet.transform.position = muzzlePosition.position;
        bullet.GetComponent<BulletView>().isCalledByPooling = true;
        bullet.GetComponent<Rigidbody>().AddForce(muzzlePosition.forward.normalized * _bulletSpeed, ForceMode.Impulse);
        Debug.Log(bullet.transform.position);
    }

    public void Reload(AudioSource reloadAudioSource)
    {
        if (weaponIndex == (int)WeaponKeys.Pistol)
        {
            if (totalPistolMagInside == pistolMagCapacity || totalPistolAmmo == 0 || totalPistolAmmo == 0 && totalPistolMagInside == 0)
            {
                reloading = false;
                playerAndWeaponUIModel.statusLabel.text = " ";
                return;
            }
        }
        else
        {
            if (totalRifleMagInside == rifleMagCapacity || totalRifleAmmo == 0 || totalRifleAmmo == 0 && totalRifleMagInside == 0)
            {
                reloading = false;
                playerAndWeaponUIModel.statusLabel.text = " ";
                return;
            }
        }
        reloading = true;
        playerAndWeaponUIModel.statusLabel.text = "RELOADING, PLEASE WAIT";
        playerAndWeaponUIModel.weaponCrossHair.gameObject.SetActive(false);


        if (!reloadAudioSource.isPlaying)
        {
            reloadAudioSource.Play();
            ReloadAsync(reloadAudioSource);
        }
    }

    public async void ReloadAsync(AudioSource reloadAudioSource)
    {
      await WaitingReloadSound(reloadAudioSource);
    }

    private async Task WaitingReloadSound(AudioSource audioSource)
    {
        int clipLength = (int)audioSource.clip.length;
        if (weaponIndex == (int)WeaponKeys.Rifle)
        {
            clipLength += 1;
        }
        await Task.Delay(clipLength * 1000);
        if (weaponIndex == (int)WeaponKeys.Pistol)
        {
            playerAndWeaponUIModel.ReloadAmmo(totalWeaponMagInside: pistolMagCapacity, totalWeaponAmmo: totalPistolAmmo, weaponMagInside: totalPistolMagInside);
        }
        else
        {
            playerAndWeaponUIModel.ReloadAmmo(totalWeaponMagInside: rifleMagCapacity, totalWeaponAmmo: totalRifleAmmo, weaponMagInside: totalRifleMagInside);
        }
    }

    public void ChangeWeapon(List<GameObject> weaponList, WeaponKeys weaponKey, Animation fireAnimation, AudioSource reloadAudioSource)
    {
        if (weaponKey == WeaponKeys.Pistol)
        {
            weaponList[weaponIndex].SetActive(false);
            weaponIndex = (int)weaponKey;
            weaponList[weaponIndex].SetActive(true);
            playerAndWeaponUIModel.ChangeWeaponAmmoText(totalPistolMagInside, totalPistolAmmo);
            fireAnimation.clip = weaponData[weaponIndex].weapon.fireAnimationClip;
            reloadAudioSource.clip = weaponData[weaponIndex].weapon.realod;
        }
        else if (weaponKey == WeaponKeys.Rifle)
        {
            weaponList[weaponIndex].SetActive(false);
            weaponIndex = (int)weaponKey;
            weaponList[weaponIndex].SetActive(true);
            playerAndWeaponUIModel.ChangeWeaponAmmoText(totalRifleMagInside, totalRifleAmmo);
            fireAnimation.clip = weaponData[weaponIndex].weapon.fireAnimationClip;
            reloadAudioSource.clip = weaponData[weaponIndex].weapon.realod;
        }
    }

    public void ResetWeaponAmmo()
    {
        totalPistolMagInside = pistolMagCapacity;
        totalPistolAmmo = pistolMagCount * pistolMagCapacity;

        totalRifleMagInside = rifleMagCapacity;
        totalRifleAmmo = rifleMagCount * rifleMagCapacity;

        if (weaponIndex == (int)WeaponKeys.Pistol)
        {
            playerAndWeaponUIModel.InitTextAmmo(totalPistolMagInside, totalPistolAmmo);
        }
        else if (weaponIndex == (int)WeaponKeys.Rifle)
        {
            playerAndWeaponUIModel.InitTextAmmo(totalRifleMagInside, totalRifleAmmo);
        }
    }

    public void ResetWeaponDamagePower()
    {
        pistolDamagePower = weaponData[(int)WeaponKeys.Pistol].weapon.damagePower;
        rifleDamagePower = weaponData[(int)WeaponKeys.Rifle].weapon.damagePower;
        isWeaponIncreaseDamage = false;
    }
}
