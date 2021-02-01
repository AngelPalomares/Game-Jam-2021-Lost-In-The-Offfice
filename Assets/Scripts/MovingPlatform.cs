using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject[] Waypoints;
    public GameObject Player;
    public AudioSource ElevatorMusic, stop;
    public GameObject Test;
    public int current = 0;
    public float Speed;

    public float pauseDuration = 3f;
    private float pauseTimer = 0f;
    private bool paused = false;

    public bool Goingdown = false;
    public float AnotherPause = 2f;
    public float anotherTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (paused)
        {
            pauseTimer += Time.deltaTime;
            if (pauseTimer >= pauseDuration)
            {
                Test.SetActive(true);
                paused = false;
                pauseTimer = 0f;
            }
            return;
        }

            if(Goingdown)
            {
                    Goingdown = false;

            }
            else
            {
                Test.SetActive(true);
            }


        transform.position = Vector3.MoveTowards(transform.position, Waypoints[current].transform.position, Speed * Time.fixedDeltaTime);


        if (transform.position == Waypoints[current].transform.position)
        {

            current++;
            Test.SetActive(false);
            paused = true;
            if (current == Waypoints.Length)
            {
                current = 0;
                Goingdown = true;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(UnityTags.PLAYER))
        {
            AudioManager.instance.ElevatorrMusic();
            
            Player = collision.gameObject;

            Player.transform.parent = transform;


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(UnityTags.PLAYER))
        {
            AudioManager.instance.MainThemePlay();

            Player = collision.gameObject;

            Player.transform.parent = null;

        }
    }
}
