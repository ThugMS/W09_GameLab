using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    #region PublicVariables
    public WEAPON_TYPE equipWeaponType;

    public GameObject equipWeapon;
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    private void Awake()
    {
        ChangeWeapon(WEAPON_TYPE.Pistol);
    }

    public void ChangeWeapon(WEAPON_TYPE _type)
    {
        equipWeaponType = _type;
        equipWeapon = transform.GetChild(0).gameObject;
    }

    public void ShootAction()
    {
        switch(equipWeaponType){
            case WEAPON_TYPE.Sword:

                break;

            default:
                equipWeapon.GetComponent<Gun>().Shoot();
                break;
        }
    }
    #endregion

    #region PrivateMethod
    #endregion
}
