using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
          if(Inventory.Instance.HasRedKey)
          {
            Inventory.Instance.Count--;
            Inventory.Instance.HasRedKey = false;
            Destroy(gameObject);

          }
    }
}
