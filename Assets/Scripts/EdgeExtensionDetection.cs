using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeExtensionDetection : MonoBehaviour
{
    public EdgeWorldDetection edgeScript;
    public IsometricCharacterController playerScript;

    void Update() 
    {
        if (edgeScript.GetWall()) 
        {
            gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        }
        else 
        {
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerScript.onPlatform = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerScript.onPlatform = false;
        }
    }
}
