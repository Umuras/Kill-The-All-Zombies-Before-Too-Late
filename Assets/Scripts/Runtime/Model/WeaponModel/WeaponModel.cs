using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModel : IWeaponModel
{
    [Inject]
    public IObjectPoolingModel objectPoolingModel { get; set; }

    public List<WeaponData> weaponData { get; set; }

    [PostConstruct]
    public void GetWeaponData()
    {
        weaponData = Resources.Load<CD_Weapon>("Data/CD_Weapon").WeaponData;
    }

    public void RaycastForWeapon(Transform muzzlePosition)
    {
        Ray ray = new Ray(muzzlePosition.position, muzzlePosition.forward);

        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction*50f, color: Color.red);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.CompareTag("Target"))
            {
                Debug.LogError("Hit Target!!!!");
            }
        }  
    }

    public void FireBullet(Transform muzzlePosition, WeaponKeys weaponKey)
    {
        if (objectPoolingModel.poolableObjectList.Count > 0)
        {
            GameObject bullet = objectPoolingModel.DequeuePoolableGameObject();
            bullet.transform.position = muzzlePosition.position;
            bullet.GetComponent<BulletView>().isCalledByPooling = true;
            bullet.GetComponent<Rigidbody>().AddForce(muzzlePosition.forward * weaponData[(int)weaponKey].weapon.shootRange, ForceMode.Impulse);
        }
    }
}
