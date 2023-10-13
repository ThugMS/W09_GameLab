using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeftHandAction : MonoBehaviour
{
    #region PublicVariables
    public THROWING_TYPE throwingType;

    public List<GameObject> throwingObjs;
    #endregion

    #region PrivateVariables
    private StarterAssetsInputs m_input;

    private Vector3 m_targetDir;
    private Vector3 m_targetPosition;
    #endregion

    #region PublicMethod
    private void Start()
    {
        InitSetting();
    }

    private void Update()
    {
        CheckThrow();
    }
    #endregion

    #region PrivateMethod
    private void InitSetting()
    {
        m_input = transform.parent.GetComponent<StarterAssetsInputs>();
        throwingType = THROWING_TYPE.Grenade;
    }
    private void CheckThrow()
    {
        if (m_input.throwing == false)
            return;

        m_input.throwing = false;

        Throw();
    }

    private void Throw()
    {
        GameObject obj = throwingObjs[(int)throwingType];

        GameObject grenade = Instantiate(obj, transform.position, Quaternion.identity);

        GetDirection();

        grenade.GetComponent<Grenade>().InitSetting(m_targetDir.normalized);
    }

    private void GetDirection()
    {
        m_targetPosition = transform.position + FirstPersonController.instance.CinemachineCameraTarget.transform.forward * ConstVariable.GRENADE_RANGE;

        m_targetDir = m_targetPosition - transform.position;
    }
    #endregion
}
