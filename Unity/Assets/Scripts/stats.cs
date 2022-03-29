using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stats : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMesh>().text = "Coins: " + GM.coinTotal +
                                        "\nTime: " + (Mathf.Round(GM.statisticsTime * 100)) / 100.0;
    }
}
