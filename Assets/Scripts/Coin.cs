using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    
    void Update()
    {
        transform.Rotate(50f * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerManager.noOfCoins += 1;
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().PlaySound("Coin");
        }
    }
}
