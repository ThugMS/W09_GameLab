using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingInput : MonoBehaviour
{
    #region PublicVariables
    public float rotationSpeedChangeConstant = 0.01f;
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {   
            if(FirstPersonController.instance.RotationSpeed > 0)
            {
                FirstPersonController.instance.RotationSpeed -= rotationSpeedChangeConstant;
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (FirstPersonController.instance.RotationSpeed > 0)
            {
                FirstPersonController.instance.RotationSpeed += rotationSpeedChangeConstant;
            }
        }
    }
    #endregion

    #region PrivateMethod
    #endregion
}
