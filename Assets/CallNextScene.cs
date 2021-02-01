using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;
using UnityEngine.SceneManagement;

public class CallNextScene : MonoBehaviour
{
    public string Winner;
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
            Global.ItemsCollected = Inventory.Instance.Count;
            Global.TimeTaken =(int)Menu.instance.TimeRemaining;
            Global.TotalItems = 40;

            SceneManager.LoadScene(Winner);

        }
    }
}
