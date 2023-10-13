using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Pistol : Gun
{
    #region PublicVariables
    public LineRenderer shootLine;
    public Animator animator;
    public AudioSource pistolSound;

    public bool isSkillPressed = false;
    #endregion

    #region PrivateVariables
    private float m_curCoolTime;
    private float m_offset;


    private float m_skillChargeTime;
    private float m_skillChargeCurTime = 0;
    #endregion

    #region PublicMethod
    public override void InitSetting()
    {
        shootCircleRadius = ConstVariable.PISTOL_CIRCLE_RADIUS;
        shootCoolTime = ConstVariable.PISTOL_COOLTIME;
        shootDamage = ConstVariable.PISTOL_DAMAGE;
        skillCoolTime = ConstVariable.PISTOL_SKILL_COOLTIME;
        m_skillChargeTime = ConstVariable.PISTOL_SKILL_CHARGETIME;

        shootCurCoolTime = shootCoolTime;
        skillCurCoolTime = skillCoolTime;

        shootLine = GetComponent<LineRenderer>();
        animator = GetComponent<Animator>();
        pistolSound = GetComponent<AudioSource>();
    }

    public override void Update()
    {
        base.Update();

        CheckSkillCharge();
    }

    public override void Shoot()
    {
        if (ShootCheck() == false)
            return;

        SetShootDireciton();
        ShowShoot();
       
    }

    public override void Skill(bool _pressed)
    {
        isSkillPressed = _pressed;

        if (_pressed == true)
        {
            return;
        }

        if (SkillCheck() == false)
            return;

        if (SkillChargeCheck() == false)
            return;

        SetShootDireciton();
        ShowSkill();
    }


    #endregion

    #region PrivateMethod


    private void ShowShoot()
    {
        ShowLine();

        if (isHit == true)
        {   
            ParticleManager.instance.ShowParticle(ConstVariable.PISTOL_PARTICLE_INDEX, targetPos, transform.position);
        }

        ResetShootCoolTime();
    }

    private void ShowSkill()
    {
        ShowLine();

        if (isHit == true)
        {
            ParticleManager.instance.ShowParticle(ConstVariable.PISTOL_PARTICLE_INDEX, targetPos, transform.position);
        }

        ResetSkillCoolTime();
    }

    private void ShowLine()
    {
        shootLine.positionCount = 2;

        Vector3 shootPos = transform.position + viewCamera.transform.forward;
        shootLine.SetPosition(0, shootPos);
        shootLine.SetPosition(1, targetPos);

        StopCoroutine(nameof(IE_ShootTrail));
        StartCoroutine(nameof(IE_ShootTrail));

        animator.Play(ConstVariable.PISTOL_ANIMATION_RECOIL);
        pistolSound.Play();
    }

    private void CheckSkillCharge()
    {
        if (SkillCheck() == false)
            return;

        if(isSkillPressed == true)
        {
            m_skillChargeCurTime += Time.deltaTime;
            m_skillChargeCurTime = m_skillChargeCurTime >= m_skillChargeTime ? m_skillChargeTime : m_skillChargeCurTime;
        }
        else
        {   
            m_skillChargeCurTime -= Time.deltaTime;
            m_skillChargeCurTime = m_skillChargeCurTime < 0 ? 0 : m_skillChargeCurTime;
        }
    }

    private bool SkillChargeCheck()
    {
        return m_skillChargeCurTime >= m_skillChargeTime;
    }

    private void ResetSkillCoolTime()
    {
        skillCurCoolTime = 0;
        shootCurCoolTime = 0;
        m_skillChargeCurTime = 0;
    }

    private IEnumerator IE_ShootTrail()
    {
        float width = 0.1f;

        while (true)
        {
            shootLine.startWidth = width;
            shootLine.endWidth = width;

            width -= 0.003f;
            yield return null;

            if (width <= 0)
                break;
        }

        shootLine.positionCount = 0;
    }
    #endregion

}
