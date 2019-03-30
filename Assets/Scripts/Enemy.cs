using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{


    [SerializeField]
    private Transform target;
    [SerializeField]
    private float speed = 3f;


   /* AudioSource audioSource;
    [SerializeField]
    private AudioClip hurtSound;
    */


    // Use this for initialization
    void Start()
    {

       // audioSource = gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.LookAt(target.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);


        if (Vector3.Distance(transform.position, target.position) > 1f)
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // audioSource.clip = hurtSound;
            //  audioSource.Play();
            GameManager.score = 0;
            PlayerPrefs.SetInt("pontuacao", GameManager.score);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}