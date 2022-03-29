using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - time > 3)
        {
            GM.lvlCompStatus = "";
            SceneManager.LoadScene("Game1");
        }
    }
}
