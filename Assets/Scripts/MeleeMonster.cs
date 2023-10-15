using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class MeleeMonster : MonoBehaviour, IMonsterHit, IExplosionInteract
{
    #region PublicVariables
    public Animator animator;

    public bool canMove = true;
    public bool canAttack = false;
    public bool isAttack = false;
    public bool isDeath = false;

    public int attackLayer;
    
    #endregion

    #region PrivateVariables
    private Rigidbody m_rb;
    private BoxCollider m_attackCol;

    private Vector3 m_dir;
    private float m_speed;
    private float m_attackRange;
    private float m_health;

    private Vector3 m_deathColCenter = new Vector3(0, 0.2f, 0.6f);
    #endregion

    #region PublicMethod
    private void Start()
    {
        InitSetting();
    }

    private void FixedUpdate()
    {
        if (isDeath == true)
            return;

        MoveToTarget();
    }

    private void Update()
    {
        if (isDeath == true)
            return;

        Attack();
    }

    public void SetAttackEnd()
    {
        isAttack = false;
        canMove = true;
    }

    public void IGetDamage(float _damage)
    {
        m_health -= _damage;
        CheckDeath();
    }

    public void IExplosionInteract(float _power, Vector3 _pos, float _exploDistance, float _damage)
    {
        if(isDeath || m_health - _damage <= 0)
        {
            PushedByExplosion(_power, _pos, _exploDistance);
        }

        IGetDamage(_damage);
    }

    public void PushedByExplosion(float _power, Vector3 _pos, float _exploDistance)
    {
        Vector3 dir = (transform.position + new Vector3(0, 1, 0) - _pos).normalized;
        float dis = Vector3.Distance(transform.position, _pos);

        float power = _power * (dis / _exploDistance);
        dir.y *= 2f;

        m_rb.AddForce(dir * power, ForceMode.Impulse);
    }
    #endregion

    #region PrivateMethod
    private void InitSetting()
    {
        TryGetComponent<Animator>(out animator);
        TryGetComponent<Rigidbody>(out m_rb);
        transform.Find("AttackCollider").TryGetComponent<BoxCollider>(out m_attackCol);

        attackLayer = LayerMask.GetMask("Player");

        m_speed = ConstVariable.MELEEMONSTER_SPEED;
        m_attackRange = ConstVariable.MELEEMONSTER_RANGE;
        m_health = ConstVariable.MELEEMONSTER_HEALTH;
    }
    private void MoveToTarget()
    {
        if (canMove == false)
            return;

        Vector3 targetPos = FirstPersonController.instance.transform.position;
        targetPos.y = transform.position.y;

        transform.LookAt(targetPos);

        m_dir = (targetPos - transform.position).normalized;

        m_rb.MovePosition(transform.position + m_dir * m_speed * Time.fixedDeltaTime);
    }

    private void Attack()
    {
        if (!(canAttack == true && isAttack == false))
            return;

        isAttack = true;
        canMove = false;

        animator.Play(ConstVariable.MELEEMONSTER_ATTACKANIM);
    }

    private void CheckDeath()
    {
        if(m_health <= 0)
        {   
            isDeath = true;
            animator.SetTrigger("Death");
            CapsuleCollider col;
            col = transform.GetComponent<CapsuleCollider>();
            col.center = m_deathColCenter;
            col.direction = 2;
            Invoke(nameof(DestroySelf), 10f);
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            canAttack = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            canAttack = false;
        }
    }

    #endregion
}
