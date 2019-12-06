using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DelayedRestart : MonoBehaviour
{
    public float restartDelay;

    public void Start()
    {
        StartCoroutine(RestartScene());
    }
    IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(restartDelay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
