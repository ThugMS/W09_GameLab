using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMonsterProjectile : MonoBehaviour
{
    #region PublicVariables
    #endregion

    #region PrivateVariables
    private Rigidbody m_rb;
    private float m_speed;
    private Vector3 m_dir;
    #endregion

    #region PublicMethod
    private void FixedUpdate()
    {
        m_rb.MovePosition(m_rb.position + transform.forward * m_speed * Time.fixedDeltaTime);
    }
    public void InitSetting(float _speed, Vector3 _pos)
    {
        TryGetComponent<Rigidbody>(out m_rb);

        transform.LookAt(_pos);
        
        m_dir = _pos;
        m_speed = _speed;
    }
    #endregion

    #region PrivateMethod
    #endregion
}
