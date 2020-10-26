using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public MoveShip MoveShip;
    void Update()
    {
        transform.Translate(new Vector3(0, 0, 1) * MoveShip.SpeedNow * Time.deltaTime);
    }
}
