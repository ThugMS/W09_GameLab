using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    #region PublicVariables
    public Vector3 shootDirection;
    public Vector3 targetPos;
    public RaycastHit hit;
    public GameObject viewCamera;
    
    public int targetLayer;

    [Header("Stat")]
    public float shootCircleRadius;
    public float shootCoolTime;
    public float shootCurCoolTime;
    public float shootDamage;

    public float skillCoolTime;
    public float skillCurCoolTime;
    public float skillDamage;

    public bool isHit = false;
    public bool isProjectile = false;
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

        targetLayer = 1 << LayerMask.GetMask("Monster", "Wall", "Projectile");

        InitSetting();
    }
    public virtual void Update()
    {
        UpdateCoolTime();
    }

    public abstract void InitSetting();

    public abstract void Shoot();

    public abstract void Skill(bool _pressed);

    public void SetShootDireciton()
    {
        isHit = false;
        isProjectile = false;

        if (Physics.SphereCast(viewCamera.transform.position, shootCircleRadius, viewCamera.transform.forward, out hit, targetLayer))
        {
            isHit = true;
            targetPos = hit.point;
            shootDirection = hit.point - transform.position;
            CheckProjectile();
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

    public bool SkillCheck()
    {
        return skillCurCoolTime >= skillCoolTime;
    }

    public void ResetShootCoolTime()
    {
        shootCurCoolTime = 0;
    }

    public void UpdateCoolTime()
    {
        shootCurCoolTime += Time.deltaTime;
        skillCurCoolTime += Time.deltaTime;
    }

    public void CheckProjectile()
    {
        if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            isProjectile = true;
            hit.transform.GetComponent<IProjectile>().ProjectileAction();
        }

    }

    public void DamageToMonster(float _damage)
    {
        if (!(hit.transform.gameObject.layer == LayerMask.NameToLayer("Monster")))
            return;

        hit.transform.GetComponent<IMonsterHit>().IGetDamage(_damage);
    }
    #endregion

    #region PrivateMethod
    #endregion
}
