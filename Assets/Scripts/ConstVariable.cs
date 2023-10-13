using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConstVariable
{
    [Header("Pistol")]
    public const float PISTOL_CIRCLE_RADIUS = 0.3f;
    public const float PISTOL_COOLTIME = 0.5f;
    public const float PISTOL_DAMAGE = 10f;
    public const float PISTOL_OFFSET = 1f;
    public const float PISTOL_SKILL_COOLTIME = 3f;
    public const float PISTOL_SKILL_CHARGETIME = 1f;
    public const float PISTOL_SKILL_EFFECTSCALE = 0.3f;

    public const string PISTOL_ANIMATION_RECOIL = "PistolRecoil";
    public const int PISTOL_PARTICLE_INDEX = 0;

    [Header("Grenade")]
    public const float GRENADE_SPEED = 15f;
    public const float GRENADE_UPPOWER = 5f;
    public const float GRENADE_RANGE = 10f;
}
