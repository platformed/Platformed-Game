using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
    public float Health;
    public int Lives;

	// Use this for initialization
	void Start () {
        Lives = 3;
        Health = 100;
	}
	
	// Update is called once per frame
	void Update () {
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
