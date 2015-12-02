using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            foreach (GameObject child in GameObject.FindGameObjectsWithTag("OrbitOrbCollectible")){
                if (!child.GetComponent<OrbitCollectible>().pickedUp)
                {
                    child.GetComponent<OrbitCollectible>().pickedUp = true;
                    Destroy(this.gameObject);
                }
            }

        }
    }
}
