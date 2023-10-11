using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public static PlayerGun instance;

    #region PublicVariables
    public WEAPON_TYPE equipGun;
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    public void ChangeWeapon(WEAPON_TYPE _type)
    {
        equipGun = _type;
    }
    #endregion

    #region PrivateMethod
    #endregion
}
