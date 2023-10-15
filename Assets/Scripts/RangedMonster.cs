using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMonster : MonoBehaviour, IMonsterHit
{
    #region PublicVariables
    public bool canAttack = false;
    public bool isAttack = false;
    public bool isDeath = false;

    public GameObject attackPrefab;
    #endregion

    #region PrivateVariables
    private float m_health;
    private float m_attackCoolTime;
    private float m_attackCurCoolTime;
    #endregion

    #region PublicMethod
    private void Start()
    {
        InitSetting();
    }

    private void Update()
    {
        SetAttackCoolTime();
        Attack();
    }

    public void IGetDamage(float _damage)
    {
        m_health -= _damage;
        CheckDeath();
    }
    #endregion

    #region PrivateMethod
    private void InitSetting()
    {
        m_health = ConstVariable.RANGEDMONSTER_HEALTH;
        m_attackCoolTime = ConstVariable.RANGEDMONSTER_ATTACK_COOLTIME;
        m_attackCurCoolTime = 0f;
    }

    private void CheckDeath()
    {
        if (m_health <= 0)
        {
            isDeath = true;
            DeadAction();
        }
    }

    private void DeadAction()
    {
        Destroy(gameObject);
    }

    private void Attack()
    {
        if (canAttack == false)
            return;

        canAttack = false;
        isAttack = true;

        Vector3 dir = GetAttackDirection();
        
        Vector3 spawnPos = transform.position + dir * 2f + new Vector3(0, 6.31f, 0);

        GameObject obj = Instantiate(attackPrefab, spawnPos, Quaternion.identity);
        obj.GetComponent<RangedMonsterProjectile>().InitSetting(ConstVariable.RANGEDMONSTER_ATTACK_SPEED, FirstPersonController.instance.transform.position);
    }
    
    private Vector3 GetAttackDirection()
    {
        Vector3 dir;

        Vector3 pos = transform.position + new Vector3(0, 6.31f, 0);

        dir = (FirstPersonController.instance.transform.position - pos).normalized;

        return dir;
    }

    private void SetAttackCoolTime()
    {
        if (isAttack == true)
            return;

        m_attackCurCoolTime += Time.deltaTime;

        if (m_attackCurCoolTime >= m_attackCoolTime)
            canAttack = true;
    }
    #endregion

}
