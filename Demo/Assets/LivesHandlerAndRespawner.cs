using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesHandlerAndRespawner : MonoBehaviour {

    public bool hasItem;
    public Transform talkPanel;
    public string[] conversation1, conversation2;
    public float timeForSentence;
    private Transform movingOnPlatform;
    [SerializeField]
    private GameObject winCanvas;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator moveWithTransform(Transform t)
    {
        while (movingOnPlatform != null) {
            Vector3 pos = t.position;
            yield return null;
            transform.position += t.position - pos;
        }
    }

    private IEnumerator talk(int i)
    {
        FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
        talkPanel.gameObject.SetActive(true);
        for (int t = 0; t<(i==1?conversation2:conversation1).Length; t++)
        {
            talkPanel.Find("texto").GetComponent<TMPro.TextMeshProUGUI>().text = (i == 1 ? conversation2 : conversation1)[t];
            yield return new WaitForSeconds(timeForSentence);
        }
        talkPanel.gameObject.SetActive(false);
        if (i != 1)
            FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
        else
        {
            winCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Yep");
        if(other.tag == "Respawn")
        {
            Debug.Log("Yup");
            FindObjectOfType<TextEnablerTimed>().enableText();
            
        }
        if (other.transform.tag == "MovingPlatform")
        {
            Debug.Log("Jore");
            movingOnPlatform = other.transform;
            StartCoroutine(moveWithTransform(other.transform));
        }
        if (other.transform.tag == "TalkingMan")
        {
            StartCoroutine(talk(hasItem ? 1 : 0));
        }
        if (other.transform.tag == "Finish")
        {
            Debug.Log("Goal");
            hasItem = true;
            GameObject.Find("GoldIngotSprite").GetComponent<UnityEngine.UI.Image>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == movingOnPlatform)
            movingOnPlatform = null;
    }
}
