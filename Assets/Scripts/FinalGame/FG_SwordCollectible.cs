using System.Collections;
using UnityEngine;

public class FG_SwordCollectible : MonoBehaviour
{
    [SerializeField] private float pickupValue;
    private Animator anim;
    private FG_GameManager gameManager;

    void Awake()
    {
        anim = GetComponent<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FG_GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }

    private IEnumerator DisableAfterAnimation()
    {
        yield return new WaitForSeconds(1.0f); // Wait for the pickup animation to finish (assuming it takes 1 second)
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.AddScore(pickupValue);
            anim.SetTrigger("collected");
            StartCoroutine(DisableAfterAnimation());
        }
    }
}