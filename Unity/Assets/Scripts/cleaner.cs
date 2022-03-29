using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cleaner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    [System.Obsolete]
    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);           
    }
}
