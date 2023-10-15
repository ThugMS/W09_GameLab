using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class MeleeMonster : MonoBehaviour
{
    #region PublicVariables
    public Animator animator;

    public bool canMove = true;
    public bool canAttack = false;
    public bool isAttack = true;

    public int attackLayer;
    #endregion

    #region PrivateVariables
    private Rigidbody m_rb;
    private BoxCollider m_attackCol;

    private Vector3 m_dir;
    private float m_speed;
    private float m_attackRange;
    #endregion

    #region PublicMethod
    private void Start()
    {
        InitSetting();
    }

    private void FixedUpdate()
    {
        MoveToTarget();
    }

    private void Update()
    {
        Attack();
    }

    public void SetAttackEnd()
    {
        isAttack = false;
        canMove = true;
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

    private void OnTriggerStay(Collider other)
    {
        canAttack = true;
    }

    private void OnTriggerExit(Collider other)
    {
        canAttack = false;
    }
    #endregion
}
