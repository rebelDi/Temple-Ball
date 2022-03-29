using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    public static float verticalSpeed = 0;
    public static int coinTotal = 0;
    public static float timeTotal = 0;
    public static float statisticsTime = 0;
    public float waitToLoad = 0;

    public float zScenePos = 56.6f;

    public static float cameraVelocilyZ = 1;

    public static string lvlCompStatus = "";

    //Usual Textures
    public Transform buildingBlockNoPit;
    public Transform buildingBlockPitMid;
    public Transform buildingBlockPitLeft;
    public Transform buildingBlockPitRight;
    public Transform buildingBlockPitFull;

    //Ice Textures
    public Transform buildingIceBlockNoPit;
    public Transform buildingIceBlockPitMid;
    public Transform buildingIceBlockPitLeft;
    public Transform buildingIceBlockPitRight;
    public Transform buildingIceBlockPitFull;

    //Fire Textures
    public Transform buildingFireBlockNoPit;
    public Transform buildingFireBlockPitMid;
    public Transform buildingFireBlockPitLeft;
    public Transform buildingFireBlockPitRight;
    public Transform buildingFireBlockPitFull;

    private Transform[] normalBlocks;
    private Transform[] iceBlocks;
    private Transform[] fireBlocks;
    private Transform[] blocksForGeneration;

    public Transform coinObj;
    public Transform obstObj;
    public Transform capsuleObj;

    public int randNum;

    // Start is called before the first frame update
    void Start()
    {
        normalBlocks = new Transform[]{ buildingBlockNoPit, buildingBlockPitMid, buildingBlockPitLeft, buildingBlockPitRight, buildingBlockPitFull};
        iceBlocks = new Transform[]{ buildingIceBlockNoPit, buildingIceBlockPitMid, buildingIceBlockPitLeft, buildingIceBlockPitRight, buildingIceBlockPitFull};
        fireBlocks = new Transform[]{ buildingFireBlockNoPit, buildingFireBlockPitMid, buildingFireBlockPitLeft, buildingFireBlockPitRight, buildingFireBlockPitFull};
        blocksForGeneration = normalBlocks;

        for (int i = -1; i < 29; i += 4)
        {
            Instantiate(buildingBlockNoPit, new Vector3(0, 0, i), buildingBlockNoPit.rotation);
        }

        Instantiate(buildingBlockNoPit, new Vector3(0, 3.26f, 40.6f), buildingBlockNoPit.rotation);
        Instantiate(buildingBlockNoPit, new Vector3(0, 3.26f, 44.6f), buildingBlockNoPit.rotation);

        Instantiate(buildingBlockPitMid, new Vector3(0, 3.26f, 48.6f), buildingBlockPitMid.rotation);
        Instantiate(buildingBlockPitMid, new Vector3(0, 3.26f, 52.6f), buildingBlockPitMid.rotation);       
    }

    //int flag = ;

    // Update is called once per frame
    void Update()
    {       
        if (!Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z+6), 2))
        {        
            //Debug.Log(Mathf.Round(Time.time));        

            //Generate new location
            if (Mathf.Round(Time.time) % 5 == 0)
            {
                randNum = Random.Range(0, 3);
                switch (randNum)
                {
                    case 0:
                        blocksForGeneration = normalBlocks;
                        break;
                    case 1:
                        blocksForGeneration = iceBlocks;
                        break;
                    case 2:
                        blocksForGeneration = fireBlocks;
                        break;
                    default:
                        break;
                }
            }


            //generating building blocks
            if (zScenePos < 1000)
            {
                randNum = Random.Range(0, 15);
                switch (randNum)
                {
                    case 0:
                        Instantiate(blocksForGeneration[0], new Vector3(0, 3.26f, zScenePos), buildingBlockNoPit.rotation);
                        Instantiate(obstObj, new Vector3(-1, 4.26f, zScenePos), obstObj.rotation);
                        Instantiate(coinObj, new Vector3(0, 4.26f, zScenePos), coinObj.rotation);
                        break;

                    case 1:
                        Instantiate(blocksForGeneration[0], new Vector3(0, 3.26f, zScenePos), buildingBlockNoPit.rotation);
                        Instantiate(obstObj, new Vector3(0, 4.26f, zScenePos), obstObj.rotation);
                        Instantiate(coinObj, new Vector3(1, 4.26f, zScenePos), coinObj.rotation);
                        break;

                    case 2:
                        Instantiate(blocksForGeneration[0], new Vector3(0, 3.26f, zScenePos), buildingBlockNoPit.rotation);
                        Instantiate(obstObj, new Vector3(1, 4.26f, zScenePos), obstObj.rotation);
                        Instantiate(coinObj, new Vector3(-1, 4.26f, zScenePos), coinObj.rotation);
                        break;

                    case 3:
                        Instantiate(blocksForGeneration[1], new Vector3(0, 3.26f, zScenePos), buildingBlockNoPit.rotation);
                        Instantiate(obstObj, new Vector3(-1, 4.26f, zScenePos), coinObj.rotation);
                        Instantiate(coinObj, new Vector3(0, 4.26f, zScenePos), coinObj.rotation);
                        Instantiate(coinObj, new Vector3(1, 4.26f, zScenePos), coinObj.rotation);
                        break;

                    case 4:
                        Instantiate(blocksForGeneration[1], new Vector3(0, 3.26f, zScenePos), buildingBlockNoPit.rotation);
                        Instantiate(capsuleObj, new Vector3(0, 4.26f, zScenePos), capsuleObj.rotation);
                        Instantiate(coinObj, new Vector3(-1, 4.26f, zScenePos), coinObj.rotation);
                        break;

                    case 5:
                        Instantiate(blocksForGeneration[1], new Vector3(0, 3.26f, zScenePos), buildingBlockNoPit.rotation);
                        Instantiate(capsuleObj, new Vector3(-1, 4.26f, zScenePos), capsuleObj.rotation);
                        Instantiate(coinObj, new Vector3(1, 4.26f, zScenePos), coinObj.rotation);
                        break;

                    case 6:
                        Instantiate(blocksForGeneration[2], new Vector3(0, 3.26f, zScenePos), buildingBlockNoPit.rotation);
                        Instantiate(capsuleObj, new Vector3(1, 4.26f, zScenePos), capsuleObj.rotation);
                        Instantiate(obstObj, new Vector3(-1, 4.26f, zScenePos), coinObj.rotation);
                        break;

                    case 7:
                        Instantiate(blocksForGeneration[2], new Vector3(0, 3.26f, zScenePos), buildingBlockPitMid.rotation);
                        Instantiate(coinObj, new Vector3(-1, 4.26f, zScenePos), coinObj.rotation);
                        break;

                    case 8:
                        Instantiate(blocksForGeneration[2], new Vector3(0, 3.26f, zScenePos), buildingBlockPitRight.rotation);
                        Instantiate(coinObj, new Vector3(-1, 4.26f, zScenePos), coinObj.rotation);
                        break;

                    case 9:
                        Instantiate(blocksForGeneration[3], new Vector3(0, 3.26f, zScenePos), buildingBlockPitLeft.rotation);
                        Instantiate(coinObj, new Vector3(1, 4.26f, zScenePos), coinObj.rotation);
                        break;
                    case 10:
                        Instantiate(blocksForGeneration[3], new Vector3(0, 3.26f, zScenePos), buildingBlockPitLeft.rotation);
                        Instantiate(coinObj, new Vector3(0, 4.26f, zScenePos), coinObj.rotation);
                        Instantiate(obstObj, new Vector3(-1, 4.26f, zScenePos), coinObj.rotation);
                        break;
                    case 11:
                        Instantiate(blocksForGeneration[3], new Vector3(0, 3.26f, zScenePos), buildingBlockPitLeft.rotation);
                        Instantiate(coinObj, new Vector3(0, 4.26f, zScenePos), coinObj.rotation);
                        break;
                    case 12:
                        Instantiate(blocksForGeneration[4], new Vector3(0, 3.26f, zScenePos), buildingBlockPitLeft.rotation);
                        Instantiate(coinObj, new Vector3(1, 4.26f, zScenePos), coinObj.rotation);
                        break;
                    case 13:
                        Instantiate(blocksForGeneration[4], new Vector3(0, 3.26f, zScenePos), buildingBlockPitLeft.rotation);
                        Instantiate(coinObj, new Vector3(-1, 4.26f, zScenePos), coinObj.rotation);
                        break;
                    case 14:
                        Instantiate(blocksForGeneration[4], new Vector3(0, 3.26f, zScenePos), buildingBlockPitLeft.rotation);
                        Instantiate(coinObj, new Vector3(0, 4.26f, zScenePos), coinObj.rotation);
                        break;
                    default:
                        break;
                }

                zScenePos += 4;
            }
        }
        
        //gaming time
        timeTotal += Time.deltaTime;
        statisticsTime = Time.time;

        if (lvlCompStatus == "Fail")
        {
            waitToLoad += Time.deltaTime;
        }

        if (waitToLoad > 2)
        {
            SceneManager.LoadScene("LevelComplete");
        }
    }
}
