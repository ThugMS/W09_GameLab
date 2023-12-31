using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConstVariable
{
    [Header("Pistol")]
    public const float PISTOL_CIRCLE_RADIUS = 0.4f;
    public const float PISTOL_COOLTIME = 0.5f;
    public const float PISTOL_DAMAGE = 20f;
    public const float PISTOL_OFFSET = 1f;
    public const float PISTOL_SKILL_COOLTIME = 1f;
    public const float PISTOL_SKILL_DAMAGE = 50f;
    public const float PISTOL_SKILL_CHARGETIME = 1f;
    public const float PISTOL_SKILL_EFFECTSCALE = 0.3f;

    public const string PISTOL_ANIMATION_RECOIL = "PistolRecoil";
    public const int PISTOL_PARTICLE_INDEX = 0;

    [Header("Grenade")]
    public const float GRENADE_SPEED = 25f;
    public const float GRENADE_UPPOWER = 5f;
    public const float GRENADE_RANGE = 10f;
    public const float GRENADE_COOLTIME = 1f;
    public const int GRENADE_PARTICLE_INDEX = 1;
    public const int GRENADE_DIVDENUM = 4;
    public const float GRENADE_EXPLOSION_DISTANCE = 5f;
    public const float GRENADE_EXPLOSION_POWER = 50f;
    public const float GRENADE_EXPLOSION_DAMAGE = 50f;

    [Header("Melee Monster")]
    public const float MELEEMONSTER_SPEED = 5f;
    public const float MELEEMONSTER_RANGE = 3f;
    public const float MELEEMONSTER_HEALTH = 100f;
    public const string MELEEMONSTER_ATTACKANIM = "root|Anim_monster_scavenger_attack";
    public const string MELEEMONSTER_DEATHANIM = "root|Anim_monster_scavenger_Death1";

    [Header("Ranged Monster")]
    public const float RANGEDMONSTER_HEALTH = 100f;
    public const int RANGEDMONSTER_PARTICLE_INDEX = 2;
    public const float RANGEDMONSTER_ATTACK_COOLTIME = 3f;
    public const float RANGEDMONSTER_ATTACK_SPEED = 25f;

    [Header("Punch")]
    public const float PUNCH_BOXSIZE = 2f;
    public const float PUNCH_COOLTIME = 0.5f;
}
