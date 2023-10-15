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
    public Animator leftHandAnimator;
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
        CheckPunch();
    }

    public void Punch()
    {
        Vector3 pos = transform.position + transform.forward * (1 + ConstVariable.PUNCH_BOXSIZE);
        float size = ConstVariable.PUNCH_BOXSIZE;

        Collider[] cols = Physics.OverlapBox(pos, new Vector3(size, size, size), Quaternion.identity);

        foreach(Collider col in cols)
        {
            if(col.gameObject.layer == LayerMask.NameToLayer("Projectile"))
            {
                col.transform.GetComponent<IProjectile>()?.IProjectileAction(PROJECTILE_INTERACT_TYPE.Melee);
            }
        }
    }
    #endregion

    #region PrivateMethod
    private void InitSetting() 
    {
        m_input = transform.parent.GetComponent<StarterAssetsInputs>();
        TryGetComponent<Animator>(out leftHandAnimator);

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

        CharacterController playerController = transform.parent.GetComponent<CharacterController>();

        grenade.GetComponent<Grenade>().InitSetting(m_targetDir.normalized, playerController.velocity);
        
        
        
    }



    private void GetDirection()
    {
        m_targetPosition = transform.position + FirstPersonController.instance.CinemachineCameraTarget.transform.forward * ConstVariable.GRENADE_RANGE;

        m_targetDir = m_targetPosition - transform.position;
    }

    private void CheckPunch()
    {
        if (m_input.punch == false)
            return;

        m_input.punch = false;

        leftHandAnimator.Play("LeftHandPunch");
    }
    #endregion
}
