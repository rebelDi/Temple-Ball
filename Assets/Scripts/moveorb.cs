using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Leap;

public class moveorb : MonoBehaviour
{

    public KeyCode moveL;
    public KeyCode moveR;

    private int desiredLane = 1; // 0 = Left, 1 = Middle, 2 = Right
    private const float LANE_DISTANCE = 1.0f;
    private float horizontalSpeed = 0;
    public float speed = 4; //speed of player
    public static float timeTotal = 0;

    public bool isInvincible = false;
    public float isInvincibleTime = 0;
    public int immunityTime = 5;
    public int immunityTimeCounter = 0;

    public ProgressBarCircle pbc;
    private Text textObjFieldText;
    private Text invincibilityText;
    private UnityEngine.UI.Image invincibilityImage;
    private Canvas circleBar;
    public static bool textPopUpStatus = false;

    //Jump
    private float distToGround;
    private float jumpForce = 3.75f;
    private float gravity = 7.0f;
    private bool isOnRamp = false;
    public static bool isGrounded;

    public Transform boomObj; //for particles after death

    //LeapMotion
    Controller controller;
    public int framesCount = 0;
    float previousX = 0;
    float newX = 0;
    float previousY = 0;
    float newY = 0;

    private bool escapeKeyPressed = false;
    private float timeEscapePressed;

    // Start is called before the first frame update
    void Start()
    {
        distToGround = GetComponent<SphereCollider>().bounds.extents.y;
        textObjFieldText = GameObject.Find("Text").transform.GetComponent<Text>();
        invincibilityImage = GameObject.Find("invincibilityImage").GetComponent<UnityEngine.UI.Image>();
        invincibilityImage.enabled = false;
        invincibilityText = GameObject.Find("InvincibilityText").transform.GetComponent<Text>();
        invincibilityText.text = "";
        circleBar = GameObject.Find("CanvasProgressBar").GetComponent<Canvas>();
        circleBar.enabled = false;

        horizontalSpeed = 0;
        framesCount = 0;
        previousX = 0;
        newX = 0;
        GM.verticalSpeed = 0;

        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Manage the pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKeyDown(KeyCode.LeftAlt))
        {
            //Debug.Log("Exit");
            Application.Quit();
        }

        if (Time.time >= isInvincibleTime + 1 && isInvincible)
        {
            invincibilityText.text = "";
            invincibilityImage.enabled = false;
        }

        if (Time.time >= isInvincibleTime + immunityTime && isInvincible)
        {
            isInvincible = false;
            circleBar.enabled = false;
            immunityTimeCounter = 0;
        }
        else if (Time.time <= isInvincibleTime + immunityTime && isInvincible)
        {
            
            pbc.BarValue = immunityTimeCounter;
            textObjFieldText.text = "" + immunityTimeCounter;
            immunityTimeCounter = (int) Mathf.Abs(Mathf.Round(Time.time - isInvincibleTime - immunityTime));
        }

        //Move player
        GetComponent<Rigidbody> ().velocity = new Vector3 (horizontalSpeed, GM.verticalSpeed, speed);
        
        if (timeTotal >= 8.0)
        {
            //speed += 0.01f;
            speed += 0.02f;
            timeTotal = 0;
        } else
        {
            timeTotal += Time.deltaTime;
       }

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
                newY = firstHand.PalmPosition[1];
                //Debug.Log("x: " + firstHand.PalmPosition[0]);
            }
        }

        if (framesCount > 20)
        {
            //Move player left
            //if (Input.GetKeyDown(moveL) || Input.GetKeyDown(KeyCode.LeftArrow))
            if ((Mathf.Abs(previousX - newX) > 40 && newX < previousX) || (Input.GetKeyDown(moveL) || Input.GetKeyDown(KeyCode.LeftArrow)))
            {
                MoveLane(false);
                StartCoroutine(stopSlide());
            }

            //Move player right
            //if (Input.GetKeyDown(moveR) || Input.GetKeyDown(KeyCode.RightArrow))
            if ((Mathf.Abs(previousX - newX) > 40 && newX > previousX) || (Input.GetKeyDown(moveR) || Input.GetKeyDown(KeyCode.RightArrow)))
            {
                MoveLane(true);
                StartCoroutine(stopSlide());
            }

            //Jump
            isGrounded = Grounded();
            if (isGrounded)
            {
                //if (Input.GetKeyDown(KeyCode.Space))
                if ((Mathf.Abs(previousY - newY) > 40 && newY > previousY) || (Input.GetKeyDown(KeyCode.Space)))
                {
                    GM.verticalSpeed = jumpForce;
                }
            }
            else
            {
                GM.verticalSpeed -= (gravity * Time.deltaTime);
            }


            //Calculate where we should be in the future
            Vector3 targetPosition = transform.position.z * Vector3.forward;
            if(desiredLane == 0)
            {
                targetPosition += Vector3.left * LANE_DISTANCE;
            }
            else if(desiredLane == 2)
            {
                targetPosition += Vector3.right * LANE_DISTANCE;
            }

            //Calculate our move delta
            horizontalSpeed = (targetPosition - transform.position).normalized.x * speed * 5;

        }

    }

    private void MoveLane(bool goingRight)
    {
        desiredLane += (goingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);
    }

    [System.Obsolete]
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "lethal" && !isInvincible)
        {
            gameObject.active = false;

            horizontalSpeed = 0;
            framesCount = 0;
            previousX = 0;
            newX = 0;
            GM.verticalSpeed = 0;

            Instantiate(boomObj, transform.position, boomObj.rotation);
            GM.lvlCompStatus = "Fail";
            //Application.LoadLevel(Application.loadedLevel);
            //SceneManager.LoadScene("LevelComplete");
        }
       
        if (other.gameObject.tag == "lethal" && isInvincible)
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "capsule")
        {
            isInvincible = true;
            isInvincibleTime = Time.time;

            circleBar.enabled = true;

            //timer
            pbc.maxValue = immunityTime;
            pbc.BarValue = immunityTime;
            immunityTimeCounter = immunityTime;

            invincibilityText.text = "You are invincible!";
            invincibilityImage.enabled = true;
            moveorb.textPopUpStatus = true;
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "nonLethal" && isInvincible)
        {
            other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }
        if (other.gameObject.name == "rampbottomtrig")
        {
            GM.verticalSpeed = 2;
        }
        if (other.gameObject.name == "ramptoptrig")
        {
            GM.verticalSpeed = 0;
        }
        if (other.gameObject.name == "exit")
        {
            GM.lvlCompStatus = "Success";
            SceneManager.LoadScene("LevelComplete");
        }
        if (other.gameObject.tag == "coin")
        {
            Destroy(other.gameObject);
            GM.coinTotal += 1;
        }
    }

    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(.5f);
        horizontalSpeed = 0;
    }

    private bool Grounded()
    {
        if (isOnRamp)
        {
            return false;
        }
        else
        {
            return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
        }
    }
}
