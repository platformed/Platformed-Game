using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public static PlayerHealth instance;
    public float Health;
    public int lives;

    public Text livesText;

	// Use this for initialization
	void Start () {
        lives = 3;
        Health = 100;
	}
    void Awake()
    {
        instance = this;
    }
	
	// Update is called once per frame
	void Update () {
        livesText.text = lives.ToString();
        if(Health <= 0)
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
        transform.position = World.instance.startFlagPosition;
        if(lives > 0)
        {
            lives -= 1;
        }
    }
}
