using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private readonly string formURL = "https://forms.gle/is7rDXFSZuMeuPxk7";

    private bool quitting = false;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) == true)
        {
            Application.Quit();
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Quit.");
        Application.OpenURL(formURL);
    }

    private IEnumerator Quit(float time)
    {
        quitting = true;
        yield return new WaitForSeconds(time);
        Application.Quit();
    }

    public void GameEnd(float timeToQuit)
    {
        if (quitting == false)
        {
            StartCoroutine(Quit(timeToQuit));
        }
    }
}
