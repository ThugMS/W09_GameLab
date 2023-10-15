using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Grenade : MonoBehaviour, IProjectile
{
    #region PublicVariables
    public bool isTrace = false;
    public Vector3 targetPos;

    public int targetLayer;
    #endregion

    #region PrivateVariables
    private float m_throwSpeed;
    private float m_upPower;
    private Vector3 m_dir;
    private Rigidbody m_rb;
    #endregion

    #region PublicMethod
    private void Start()
    {
        targetLayer = LayerMask.GetMask("Player", "Monster");
    }

    private void Update()
    {   
        if(isTrace == true)
        {
            Vector3 dir = targetPos - transform.position;
            transform.Translate(dir.normalized * 100f * Time.deltaTime);
        }
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

    public void TraceTarget(Vector3 _pos)
    {
        isTrace = true;
        targetPos = _pos;
        m_rb = GetComponent<Rigidbody>();
        m_rb.useGravity = false;
    }

    public void AttackTarget()
    {
        transform.position = targetPos;
    }

    public void AddForce()
    {
        m_rb.AddForce(Vector3.up * m_upPower, ForceMode.Impulse);
        m_rb.AddForce(m_dir * m_throwSpeed, ForceMode.Impulse);
    }

    public void ProjectileAction()
    {   

        int num = ConstVariable.GRENADE_DIVDENUM;
        int cnt = 0;
        GameObject monsters = GameObject.Find("Monsters");
        int monstersChildCount = monsters.transform.childCount;
        while (cnt < num)
        {
            for(int i = 0; i < monstersChildCount; i++)
            {
                Vector3 targetPos = monsters.transform.GetChild(i).position;

                GameObject obj = Instantiate(gameObject, transform.position, Quaternion.identity);
                obj.GetComponent<Grenade>().TraceTarget(targetPos);
                cnt++;

                if (cnt >= num)
                    break;
            }
        }

        Explosion();
        EffectManager.instance.TimeStopEffect();
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

    private void CheckTarget()
    {
        Collider[] cols;

        cols = Physics.OverlapSphere(transform.position, ConstVariable.GRENADE_EXPLOSION_DISTANCE, targetLayer);

        foreach(Collider col in cols)
        {
            col.gameObject.GetComponent<IExplosionInteract>()?.IExplosionInteract(ConstVariable.GRENADE_EXPLOSION_POWER, transform.position, ConstVariable.GRENADE_EXPLOSION_DISTANCE, ConstVariable.GRENADE_EXPLOSION_DAMAGE);
        }
    }

    private void Explosion()
    {
        CheckTarget();
        ParticleManager.instance.ShowParticle(ConstVariable.GRENADE_PARTICLE_INDEX, transform.position);
        Destroy(gameObject);
    }

    public void GetDamage(float _damage)
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
