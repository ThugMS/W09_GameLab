using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;
    #region PublicVariables
    public List<GameObject> particles;
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void ShowParticle(int _index, Vector3 _pos, Vector3 _angle)
    {
        GameObject obj = Instantiate(particles[_index], _pos, Quaternion.identity);
        obj.transform.LookAt(_angle);

    }
    #endregion

    #region PrivateMethod
    #endregion
}
