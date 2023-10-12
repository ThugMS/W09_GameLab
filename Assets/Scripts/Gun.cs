using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    #region PublicVariables
    public Vector3 shootDirection;
    public Vector3 targetPos;

    public GameObject viewCamera;
    
    public int targetLayer;

    [Header("Stat")]
    public float shootCircleRadius;
    public float shootCoolTime;
    public float shootCurCoolTime;
    public float shootDamage;
    

    #endregion

    #region PrivateVariables

    #endregion

    #region PublicMethod
    public virtual void Awake()
    {
        if (viewCamera == null)
        {
            viewCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        targetLayer = 1 << LayerMask.GetMask("Monster", "Wall", "Default");

        InitSetting();
    }
    public virtual void Update()
    {
        shootCurCoolTime += Time.deltaTime;
    }

    public abstract void InitSetting();

    public abstract void Shoot();

    public void SetShootDireciton()
    {
        RaycastHit hit;

        if(Physics.SphereCast(viewCamera.transform.position, shootCircleRadius, viewCamera.transform.forward, out hit, targetLayer))
        {
            targetPos = hit.point;
            shootDirection = hit.point - transform.position;
        }
        else
        {
            targetPos = viewCamera.transform.position + viewCamera.transform.forward * 50f;
            shootDirection = targetPos - transform.position;
        }
    }

    public bool ShootCheck()
    {
        return shootCurCoolTime >= shootCoolTime;
    }

    #endregion

    #region PrivateMethod
    #endregion
}
