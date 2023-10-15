using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    #region PublicVariables
    public Vector3 pos;

    public Rigidbody rb;
    public bool play = false;
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Invoke(nameof(SetPlayTrue), 0.1f);
            transform.position = pos;

            Vector3 rot;

            rot.x = Random.Range(0, 360);
            rot.y = Random.Range(0, 360);
            rot.z = Random.Range(0, 360);

            Vector3 force = new Vector3( 0, 0, Random.Range(50 , 100));

            rb.AddForce(force);
            transform.rotation = Quaternion.Euler(rot);
        }

        if(rb.velocity.magnitude == 0f && play == true)
        {
            Debug.Log("Dice Stop");
            play = false;

            CheckNumber();
        }
    }

    
    #endregion

    #region PrivateMethod
    private void SetPlayTrue()
    {
        play = true;
    }

    private void CheckNumber()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, Vector3.up, out hit))
        {
            if (hit.transform.CompareTag("DiceNum"))
            {
                Debug.Log(hit.transform.name);
            }
        }
    }
    #endregion
}
