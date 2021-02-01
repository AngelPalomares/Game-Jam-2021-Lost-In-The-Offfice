using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public static Dialogue instance;
    public GameObject dialogBox;

    public Text DialogText;

    public string dialog;

    public string dialog2;

    public bool dialogActive;

    public bool Coffee = false;


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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            dialogBox.SetActive(true);
            DialogText.text = dialog;
        }

        if(other.CompareTag("Player") && Coffee || other.CompareTag("Player") && Inventory.Instance.Count >= 30 )
        {
            dialogBox.SetActive(true);
            DialogText.text = dialog2;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            dialogBox.SetActive(false);
        }
    }
}
