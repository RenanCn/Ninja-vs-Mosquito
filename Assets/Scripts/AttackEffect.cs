using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackEffect : MonoBehaviour {

    [SerializeField]
    private float horizontalForce = 15;
    [SerializeField]
    private float verticalForce = 10;
    [SerializeField]
    private float destroyTime = 0;

    AudioSource audioSource;

    float patternHorizontalForce;
    // Use this for initialization
    void Start()
    {
        patternHorizontalForce = horizontalForce;
        audioSource = gameObject.GetComponent<AudioSource>();

        
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameManager.score += 10;
            PlayerPrefs.SetInt("pontuacao", GameManager.score);

            if (GameManager.score > PlayerPrefs.GetInt("record"))
            {
                PlayerPrefs.SetInt("record", GameManager.score);
            }
            //score_text.text = score.ToString();
            // audioSource.Play();
            other.gameObject.GetComponent<Enemy>().enabled = false; // desativa script

            CapsuleCollider2D[] capsules = other.gameObject.GetComponents<CapsuleCollider2D>();

            //desativar box colliders do inimigo
            foreach (CapsuleCollider2D capsule in capsules)
            {
                capsule.enabled = false;
            }

            if (other.transform.position.x < transform.position.x)
            {
                horizontalForce *= -1;
            }

            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontalForce, verticalForce),
                ForceMode2D.Impulse);

            Destroy(other.gameObject, destroyTime);

            horizontalForce = patternHorizontalForce;
        }
    }
}
