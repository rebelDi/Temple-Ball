using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    public static float vertVel = 0;
    public static int coinTotal = 0;
    public static float timeTotal = 0;
    public float waitToLoad = 0;

    public static float cameraVelocilyZ = 1;

    public static string lvlCompStatus = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeTotal += Time.deltaTime;

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
