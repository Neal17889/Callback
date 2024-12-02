using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPanel : MonoBehaviour
{
    public GameObject ButtonBack;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActiveButtonBack());
    }

    IEnumerator ActiveButtonBack()
    {
        yield return new WaitForSeconds(30f);
        this.ButtonBack.SetActive(true);
    }
}
