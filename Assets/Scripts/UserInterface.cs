using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UserInterface : MonoBehaviour {

    //private attributes
    private float health;
    private int deaths;

    //components 
    private GameObject player;
    private Text HealthLabel;

	// Use this for initialization
	void Start () {
        HealthLabel = GameObject.Find("Health").GetComponent<Text>();
        
    }
	
	// Update is called once per frame
	void Update () {

        if (player = GameObject.FindGameObjectWithTag("Player")) {
            health = player.GetComponent<Collider>().GetComponent<Entity>().health;
            HealthLabel.text = "Health: " + health;
        }
        else {
            HealthLabel.text = "Health: 0";
        }
	}
}
