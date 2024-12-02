using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            bool isDead = collision.gameObject.GetComponent<PlayerControllerSystem>().isDead;
            if (!isDead)
            {
                MainPlayerCamera.Instance.isGameWin = true;
                MainPlayerCamera.Instance.VictoryScreen();
                StartCoroutine(ToCredits());
            }
        }
    }

    IEnumerator ToCredits()
    {
        yield return new WaitForSeconds(15f);
        SceneManager.LoadScene(2);
    }
}
