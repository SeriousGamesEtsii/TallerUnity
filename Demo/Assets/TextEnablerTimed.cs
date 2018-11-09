using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEnablerTimed : MonoBehaviour {

    [SerializeField]
    private float tiempo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void enableText()
    {
        GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
        StartCoroutine(disableText());
    }

    private IEnumerator disableText()
    {
        yield return new WaitForSeconds(tiempo);
        GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
    }
}
