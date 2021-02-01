using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
    [SerializeField]
    private bool Pellets;

    [SerializeField]
    private bool Cube;

    [SerializeField]
    private bool RedKey;

    [SerializeField]
    private bool BlueKey;

    [SerializeField]
    private bool Yellowkey;

    public static Collectible instance;


    private void Awake()
    {
        instance = this;
    }

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
        if (collision.CompareTag("Player"))
        {
            if (Pellets)
            {
                AudioManager.instance.PlaySFX(5);
                Inventory.Instance.Count++;
                Menu.instance.HighScore += 100;
                Destroy(gameObject);
                Debug.Log("Collected");
            }

            if (Cube)
            {
                Destroy(gameObject);
                Debug.Log("Collected");
            }

            if(RedKey)
            {
                Inventory.Instance.HasRedKey = true;
                Destroy(gameObject);
            }

        }
    }

}
