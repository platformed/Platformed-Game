using UnityEngine;
using System.Collections;
using UnitySystems.UI;

public class PlayerHealth : MonoBehaviour {
    public static PlayerHealth instance;
    public float Health;
    public int Lives;
    public int Lives { get; set; }

    public Text livesText;

	// Use this for initialization
	void Start () {
        Lives = 3;
        Health = 100;
	}
    void Awake()
    {
        instance = this;
    }
	
	// Update is called once per frame
	void Update () {
        livesText.text = lives.tostring
        if (Health <= 0)
        {
            Respawn();
        }
        if(transform.position.y <= -10)
        {
            Respawn();
        }
        
        
        
	}
    void Respawn ()
    {
        transform.position = new Vector3(128, 138, 128);
        if(Lives > 0)
        {
            Lives -= 1;
        }
    }
}
