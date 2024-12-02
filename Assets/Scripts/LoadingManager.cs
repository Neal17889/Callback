using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public GameObject UITips;
    public GameObject UITitle;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        //SoundManager.Instance.PlayMusic(SoundDefine.Music_Login);
        UITips.SetActive(true);
        yield return new WaitForSeconds(2f);
        UITips.SetActive(false);
        UITitle.SetActive(true);
        yield return DataManager.Instance.LoadData();
        
    }

}
