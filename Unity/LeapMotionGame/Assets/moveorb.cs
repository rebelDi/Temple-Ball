using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Leap;

public class moveorb : MonoBehaviour
{

    public KeyCode moveL;
    public KeyCode moveR;

    public float horizVel = 0;
    public int laneNum = 2;
    public string controlLocked = "n";
    public Transform boomObj;


    Controller controller;
    public int framesCount = 0;
    float previousX = 0;
    float newX = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody> ().velocity = new Vector3 (horizVel, GM.vertVel, 4);

        controller = new Controller();
        Frame currentFrame = controller.Frame();
        List<Hand> hands = currentFrame.Hands;
        framesCount++;

        if (currentFrame.Hands.Count > 0)
        {
            Hand firstHand = null;
            if (hands[0].IsRight)
            {
                firstHand = hands[0];
            } else
            {
                if (currentFrame.Hands.Count > 1)
                    firstHand = hands[1];
            }

            if (firstHand != null)
            {
                newX = firstHand.PalmPosition[0];
                Debug.Log("x: " + firstHand.PalmPosition[0]);
            }
        }

        if (framesCount > 20)
        {
            if (Input.GetKeyDown(moveL) && laneNum > 1 && controlLocked == "n")
            //if (Mathf.Abs(previousX - newX) > 40 && newX < previousX && controlLocked == "n" && laneNum > 1)
            {
                horizVel = -2;
                StartCoroutine(stopSlide());
                laneNum -= 1;
                controlLocked = "y";
            }

            if (Input.GetKeyDown(moveR) && laneNum < 3 && controlLocked == "n")
                //if (Mathf.Abs(previousX - newX) > 40 && newX > previousX && controlLocked == "n" && laneNum < 3)
            {
                horizVel = 2;
                StartCoroutine(stopSlide());
                laneNum += 1;
                controlLocked = "y";
            }
        }

    }

    [System.Obsolete]
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "lethal")
        {
            Destroy(gameObject);

            horizVel = 0;
            laneNum = 2;

            controlLocked = "n";
            framesCount = 0;
            previousX = 0;
            newX = 0;
            GM.vertVel = 0;

            GM.cameraVelocilyZ = 0;
            Instantiate(boomObj, transform.position, boomObj.rotation);
            GM.lvlCompStatus = "Fail";

            //Application.LoadLevel(Application.loadedLevel);
            //SceneManager.LoadScene("LevelComplete");
        }
        if (other.gameObject.name == "capsule")
        {
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "rampbottomtrig")
        {
            GM.vertVel = 2;
        }
        if (other.gameObject.name == "ramptoptrig")
        {
            GM.vertVel = 0;
        }
        if (other.gameObject.name == "exit")
        {
            GM.lvlCompStatus = "Success";
            SceneManager.LoadScene("LevelComplete");
        }
        if (other.gameObject.name == "coin")
        {
            Destroy(other.gameObject);
            GM.coinTotal += 1;
        }
    }

    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(.5f);
        horizVel = 0;
        controlLocked = "n";
    }
}
