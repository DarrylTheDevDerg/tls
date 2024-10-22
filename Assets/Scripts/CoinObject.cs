using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinObject : MonoBehaviour
{
    private PlayerStats pS;

    // Start is called before the first frame update
    void Start()
    {
        pS = FindObjectOfType<PlayerStats>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DealDamage>() != null)
        {
            pS.coins++;

            Destroy(gameObject);
        }
    }
}
