using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Grenade : MonoBehaviour, IProjectile
{
    #region PublicVariables
    public bool isTrace = false;
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
    }

    public void InitSetting(Vector3 _dir, Vector3 _playerVel)
    {
        m_rb = GetComponent<Rigidbody>();
        m_throwSpeed = ConstVariable.GRENADE_SPEED;
        m_upPower = ConstVariable.GRENADE_UPPOWER;
        m_dir = _dir;

        m_rb.AddForce(_playerVel, ForceMode.Impulse);
        AddForce();
    }

    public void ProjectileHit(GameObject _target)
    {
        
    }

    public void AddForce()
    {
        m_rb.AddForce(Vector3.up * m_upPower, ForceMode.Impulse);
        m_rb.AddForce(m_dir * m_throwSpeed, ForceMode.Impulse);
    }

    public void ProjectileAction()
    {
        Debug.Log("hit proejctile");
    }

    public void TargetHit()
    {
        
    }
    #endregion

    #region PrivateMethod
    private void OnCollisionEnter(Collision collision)
    {
        Explosion();
    }

    private void Explosion()
    {
        ParticleManager.instance.ShowParticle(ConstVariable.GRENADE_PARTICLE_INDEX, transform.position);
        Destroy(gameObject);
    }
    #endregion
}
