using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class MeleeMonster : MonoBehaviour
{
    #region PublicVariables
    public Animator animator;
    #endregion

    #region PrivateVariables
    private Rigidbody m_rb;

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
    #endregion

    #region PrivateMethod
    private void InitSetting()
    {
        TryGetComponent<Animator>(out animator);
        TryGetComponent<Rigidbody>(out m_rb);

        m_speed = ConstVariable.MELEEMONSTER_SPEED;
        m_attackRange = ConstVariable.MELEEMONSTER_RANGE;
    }
    private void MoveToTarget()
    {
        Vector3 targetPos = FirstPersonController.instance.transform.position;
        targetPos.y = transform.position.y;

        transform.LookAt(targetPos);

        m_dir = (targetPos - transform.position).normalized;

        m_rb.MovePosition(transform.position + m_dir * m_speed * Time.fixedDeltaTime);
    }
    #endregion
}
