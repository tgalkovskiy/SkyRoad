using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    private float SpeedShot = 100;
    void Update()
    {
       
       transform.Translate(new Vector3(0, 0, 1) * (SpeedShot) * Time.deltaTime);
    }
}
