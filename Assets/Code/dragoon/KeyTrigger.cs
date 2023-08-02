using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTrigger : MonoBehaviour
{
    public GameObject m_LevelToActive;
    public AudioSource audioSource;
    public GameObject m_KeyModel;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            audioSource.Play();
            if (m_LevelToActive)
            {
                m_LevelToActive.SetActive(true);
            }
            m_KeyModel.transform.gameObject.GetComponent<Renderer>().enabled = false;
            m_KeyModel.transform.gameObject.GetComponent<SphereCollider>().enabled = false;
            GameManager.instance.checkPoint.transform.position = transform.position;
        }
    }
}
