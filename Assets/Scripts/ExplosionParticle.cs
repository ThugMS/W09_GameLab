using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class ExplosionParticle : MonoBehaviour
{
    #region PublicVariables
    public float destroyDelay = 3f;
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    
    private void Start()
    {
        Invoke(nameof(DestroyGameObject), destroyDelay);
    }
    #endregion

    #region PrivateMethod
    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }
    #endregion
}
