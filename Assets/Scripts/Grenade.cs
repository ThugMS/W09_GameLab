using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    #region PublicVariables
    #endregion

    #region PrivateVariables
    private float m_throwSpeed;
    private float m_upPower;
    private Vector3 m_dir;
    private Rigidbody m_rb;
    #endregion

    #region PublicMethod

    private void Update()
    {
        m_rb.AddForce(m_dir * m_throwSpeed, ForceMode.Force);
    }

    public void InitSetting(Vector3 _dir)
    {
        m_rb = GetComponent<Rigidbody>();
        m_throwSpeed = ConstVariable.GRENADE_SPEED;
        m_upPower = ConstVariable.GRENADE_UPPOWER;
        m_dir = _dir;

        AddForce();
    }

    public void AddForce()
    {
        m_rb.AddForce(Vector3.up * m_upPower, ForceMode.Impulse);
    }
    #endregion

    #region PrivateMethod
    private void OnCollisionEnter(Collision collision)
    {
        
    }
    #endregion
}
