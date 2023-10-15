using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSpawner : MonoBehaviour
{
    #region PublicVariables
    public float spawnTime = 60f;
    public bool canSpawn = false;


    public GameObject monster;
    public GameObject monsterParent;
    #endregion

    #region PrivateVariables
    private GameObject myMonster;
    #endregion

    #region PublicMethod
    private void Start()
    {
        canSpawn = false;
        monsterParent = GameObject.Find("Monsters");

        Spawn();
    }

    private void Update()
    {
        if(canSpawn == true)
        {
            Spawn();
            canSpawn = false;
        }    
    }

    public void Spawn()
    {
        GameObject obj = Instantiate(monster, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity, monsterParent.transform);

        obj.GetComponent<RangedMonster>().SetSpawner(this);
    }
    #endregion

    #region PrivateMethod
    public IEnumerator IE_Spawn()
    {
        yield return new WaitForSeconds(spawnTime);

        canSpawn = true;
    }
    #endregion
}
