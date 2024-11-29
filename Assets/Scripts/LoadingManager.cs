using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    public GameObject UITips;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        
        yield return new WaitForSeconds(2f);
        UITips.SetActive(false);
        yield return DataManager.Instance.LoadData();
    }

}
