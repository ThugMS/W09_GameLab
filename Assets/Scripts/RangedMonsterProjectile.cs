using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangedMonsterProjectile : MonoBehaviour, IProjectile
{
    #region PublicVariables
    public int targetLayer;
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

        // TEST
        //if (Input.GetKeyDown(KeyCode.F))
        //    ChangeDirection();
    }
    public void InitSetting(float _speed, Vector3 _pos)
    {
        TryGetComponent<Rigidbody>(out m_rb);

        transform.LookAt(_pos);
        
        m_dir = _pos;
        m_speed = _speed;
        targetLayer = LayerMask.GetMask("Player", "Wall");
    }

    public void IProjectileAction(PROJECTILE_INTERACT_TYPE _type)
    {
        switch(_type) {
            case PROJECTILE_INTERACT_TYPE.Shoot:
                Explosion();
                break;

            case PROJECTILE_INTERACT_TYPE.Melee:
                ChangeDirection();
                break;
        }
        EffectManager.instance.TimeStopEffect();
    }


    #endregion

    #region PrivateMethod
    private void CheckTarget()
    {
        Collider[] cols;

        cols = Physics.OverlapSphere(transform.position, ConstVariable.GRENADE_EXPLOSION_DISTANCE, targetLayer);

        foreach (Collider col in cols)
        {
            col.gameObject.GetComponent<IExplosionInteract>()?.IExplosionInteract(ConstVariable.GRENADE_EXPLOSION_POWER, transform.position, ConstVariable.GRENADE_EXPLOSION_DISTANCE, ConstVariable.GRENADE_EXPLOSION_DAMAGE * 2);
        }
    }

    private void Explosion()
    {
        CheckTarget();
        ParticleManager.instance.ShowParticle(ConstVariable.RANGEDMONSTER_PARTICLE_INDEX, transform.position);
        Destroy(gameObject);
    }

    private void ChangeDirection()
    {
        transform.LookAt(transform.position + FirstPersonController.instance.CinemachineCameraTarget.transform.forward);
        targetLayer = LayerMask.GetMask("Monster", "Wall");
        m_speed *= 4f;
    }

    private void OnTriggerStay(Collider other)
    {
        if ((1 << (other.gameObject.layer) & targetLayer) == 0)
            return;

        Explosion();
    }
    #endregion
}
