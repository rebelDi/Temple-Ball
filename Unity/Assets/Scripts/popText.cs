using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popText : MonoBehaviour
{
    private string text = "You are invincible!";
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("oh");
        if (moveorb.textPopUpStatus)
        {
            Debug.Log("Hi");
            StartCoroutine(popUpText());
        }
        else
        {
            gameObject.transform.GetComponent<Text>().text = "";
            //gameObject.SetActive(false);
        }
    }

    private IEnumerator popUpText()
    {
        Debug.Log("changes");
        //gameObject.SetActive(true);
        gameObject.transform.GetComponent<Text>().text = text;
        yield return new WaitForSeconds(1);
        gameObject.transform.GetComponent<Text>().fontSize = 30;
        yield return new WaitForSeconds(1);
        //gameObject.SetActive(false);
        gameObject.transform.GetComponent<Text>().text = "";
        gameObject.transform.GetComponent<Text>().fontSize = 20;
        moveorb.textPopUpStatus = false;
    }
}
