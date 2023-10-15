using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeMonsterAnimation : MonoBehaviour
{
    #region PublicVariables
    public RangedMonster parentObj;
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    public void ShowAttack()
    {
        parentObj.Attack();
    }
    #endregion

    #region PrivateMethod
    #endregion
}
