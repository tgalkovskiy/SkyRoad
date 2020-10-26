using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateLvl : MonoBehaviour
{
    public MoveShip MoveShip;
    public GameObject[] Block;
    public GameObject FirstBlock;
    private List<GameObject> SpawnList = new List<GameObject>();
    private float Bias = 60f;
    private int Difficult = 3;
    void Start()
    {
        //Add first block
        SpawnList.Add(FirstBlock);
        //Create start Lvl
        for (int i = 0; SpawnList.Count < 10; i++)
        {
            //Spawn block
            GameObject NewBlock = Instantiate(Block[Random.Range(0, 4)]);
            Vector3 PositionBlock = new Vector3(0, 0, Block[0].transform.position.z + Bias);
            //transform block
            NewBlock.transform.position = PositionBlock;
            Bias += 60;
            //Add list block
            SpawnList.Add(NewBlock);
        }
    }
    /// <summary>
    /// Generate Lvl
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        //Difficult Block
        if (other.tag == "Block")
        {
            if(MoveShip.Score > 10)
            {
                Difficult = 4;
            }
            if(MoveShip.Score > 30)
            {
                Difficult = 5;
            }
            if(MoveShip.Score > 50)
            {
                Difficult = 7;  
            }
            if(MoveShip.Score > 70)
            {
                Difficult = 9;
            }
            Bias = 60;
            //Spawn Block 
            GameObject NewBlock1 = Instantiate(Block[Random.Range(0, Difficult)]);
            Vector3 PositionBlock1 = new Vector3(0, 0, SpawnList[9].gameObject.transform.position.z + Bias);
            //Transform block
            NewBlock1.transform.position = PositionBlock1;
            //Add block at licst
            SpawnList.Add(NewBlock1);
            //Destroy old block 
            Destroy(SpawnList[0].gameObject);
            SpawnList.RemoveAt(0);
            //growth speed
            MoveShip.SpeedStart += 0.5f;
        }
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
