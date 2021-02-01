using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HideObjectTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] ObjectsToHide;
    
    void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(UnityTags.PLAYER))
        {
            if (ObjectsToHide.Length > 0)
            {
                foreach (var obj in ObjectsToHide)
                {
                    obj.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(UnityTags.PLAYER))
        {
            if (ObjectsToHide.Length > 0)
            {
                foreach (var obj in ObjectsToHide)
                {
                    obj.SetActive(true);
                }
            }
        }
    }
}
