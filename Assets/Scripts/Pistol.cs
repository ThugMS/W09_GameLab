using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Pistol : Gun
{
    #region PublicVariables
    public LineRenderer shootLine;
    public Animator animator;
    public AudioSource pistolSound;

    public bool isSkillPressed = false;
    public RaycastHit[] skillHit;
    #endregion

    #region PrivateVariables
    private float m_curCoolTime;
    private float m_offset;

    [Header("Skill")]
    private float m_skillChargeTime;
    private float m_skillChargeCurTime = 0;
    [SerializeField] private GameObject m_skillEffect;
    private float m_skillEffectScale;
    #endregion

    #region PublicMethod
    public override void InitSetting()
    {
        shootCircleRadius = ConstVariable.PISTOL_CIRCLE_RADIUS;
        shootCoolTime = ConstVariable.PISTOL_COOLTIME;
        shootDamage = ConstVariable.PISTOL_DAMAGE;

        skillCoolTime = ConstVariable.PISTOL_SKILL_COOLTIME;
        skillDamage = ConstVariable.PISTOL_SKILL_DAMAGE;
        m_skillChargeTime = ConstVariable.PISTOL_SKILL_CHARGETIME;
        m_skillEffectScale = ConstVariable.PISTOL_SKILL_EFFECTSCALE;

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
            return;

        if (SkillCheck() == false)
            return;

        if (SkillChargeCheck() == false)
            return;

        SetSkillDirection();
        ShowSkill();
    }
    #endregion

    #region PrivateMethod
    private void SetSkillDirection()
    {
        isHit = false;
        isProjectile = false;

        skillHit = Physics.SphereCastAll(viewCamera.transform.position, shootCircleRadius, viewCamera.transform.forward, targetLayer);

        shootDirection = targetPos - transform.position;
        targetPos = viewCamera.transform.position + viewCamera.transform.forward * 50f;
        foreach (var item in skillHit)
        {
            isHit = true;
            hit = item;
            CheckProjectile();

            if (item.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                targetPos = item.transform.position;
                break;
            }
        }
    }

    private void ShowShoot()
    {
        ShowLine();

        if (isHit == true)
        {   
            ParticleManager.instance.ShowParticle(ConstVariable.PISTOL_PARTICLE_INDEX, targetPos, transform.position);
            DamageToMonster(shootDamage);
        }

        ResetShootCoolTime();
    }

    private void ShowSkill()
    {
        ShowSkillLine();

        foreach (var item in skillHit)
        {
            ParticleManager.instance.ShowParticle(ConstVariable.PISTOL_PARTICLE_INDEX, item.transform.position, transform.position);
            hit = item;
            DamageToMonster(skillDamage);
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
        StopCoroutine(nameof(IE_SkillTrail));
        StartCoroutine(nameof(IE_ShootTrail));

        animator.Play(ConstVariable.PISTOL_ANIMATION_RECOIL);
        pistolSound.Play();
    }

    private void ShowSkillLine()
    {
        shootLine.positionCount = 2;

        Vector3 shootPos = transform.position + viewCamera.transform.forward;
        shootLine.SetPosition(0, shootPos);
        shootLine.SetPosition(1, targetPos);

        StopCoroutine(nameof(IE_ShootTrail));
        StopCoroutine(nameof(IE_SkillTrail));
        StartCoroutine(nameof(IE_SkillTrail));

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

        float chargeRate = m_skillChargeCurTime / m_skillChargeTime;

        Vector3 scale = new Vector3(chargeRate * m_skillEffectScale, chargeRate * m_skillEffectScale, chargeRate * m_skillEffectScale);
        m_skillEffect.transform.localScale = scale;
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

        m_skillEffect.transform.localScale = Vector3.zero;
    }

    private IEnumerator IE_ShootTrail()
    {
        float width = 0.1f;

        shootLine.startColor = UnityEngine.Color.white;
        shootLine.endColor = UnityEngine.Color.white;

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

    private IEnumerator IE_SkillTrail()
    {
        float width = 0.4f;
        UnityEngine.Color colorSt = new UnityEngine.Color(255f / 255f, 198f / 255f, 102f / 255f);

        shootLine.startColor = colorSt;
        shootLine.endColor = colorSt;

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
