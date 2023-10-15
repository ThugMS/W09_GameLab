using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSpawner : MonoBehaviour
{
    #region PublicVariables
    public float spawnTimer = 30f;

    public bool canSpawn = false;

    public GameObject monster;
    public GameObject monsterParent;

    public Transform[] spawnPos;
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    private void Start()
    {
        canSpawn = true;
        monsterParent = GameObject.Find("Monsters");
    }

    private void Update()
    {
        if(canSpawn)
        {
            Spawn();
            canSpawn = false;
            StartCoroutine(nameof(IE_SpawnTimer));
        }
    }

    public void Spawn()
    {
        transform.position = FirstPersonController.instance.transform.position;
        for(int i = 0; i < spawnPos.Length; i++)
        {
            Instantiate(monster, spawnPos[i].position, Quaternion.identity, monsterParent.transform);
        }
    }
    #endregion

    #region PrivateMethod
    private IEnumerator IE_SpawnTimer()
    {
        yield return new WaitForSeconds(spawnTimer);

        canSpawn = true;
    }
    #endregion
}
