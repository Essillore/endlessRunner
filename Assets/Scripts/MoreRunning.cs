using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreRunning : MonoBehaviour
{

    public GameObject roadSection;

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(roadSection, new Vector3(0,0,93), Quaternion.identity);
    }

}
