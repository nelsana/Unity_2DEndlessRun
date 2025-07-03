using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Coin
        if (other.gameObject.tag == "Coin")
        {
            // Add score ++
            FindObjectOfType<AudioManager>().Play("Coin");
            GameManager.Instance.AddScore();
            Destroy(other.gameObject, 0.02f);
        }
        //Wall
        if (other.gameObject.tag == "Wall")
        {
            // game Over!!
            GameManager.Instance.GameOver();
        }
    }
}
