using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    #region PublicVariables
    public LineRenderer shootLine;
    public Animator animator;
    public AudioSource audio;
    #endregion

    #region PrivateVariables
    private float m_curCoolTime;
    private float m_offset;
    #endregion

    #region PublicMethod
    public override void InitSetting()
    {
        shootCircleRadius = ConstVariable.PISTOL_CIRCLE_RADIUS;
        shootCoolTime = ConstVariable.PISTOL_COOLTIME;
        shootDamage = ConstVariable.PISTOL_DAMAGE;

        shootCurCoolTime = shootCoolTime;

        shootLine = GetComponent<LineRenderer>();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    public override void Shoot()
    {
        if (ShootCheck() == false)
            return;

        SetShootDireciton();
        ShowShoot();
    }

    public override void Skill()
    {
        
    }
    #endregion

    #region PrivateMethod


    private void ShowShoot()
    {
        shootLine.positionCount = 2;

        Vector3 shootPos = transform.position + viewCamera.transform.forward;
        shootLine.SetPosition(0, shootPos);
        shootLine.SetPosition(1, targetPos);

        StopCoroutine(nameof(IE_ShootTrail));
        StartCoroutine(nameof(IE_ShootTrail));

        animator.Play(ConstVariable.PISTOL_ANIMATION_RECOIL);
        audio.Play();
        
        shootCurCoolTime = 0;
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
