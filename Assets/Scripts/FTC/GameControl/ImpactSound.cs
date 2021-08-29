using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ImpactSound : MonoBehaviour
{
    private AudioManager audioManager;

    private float timer = 0;

    void Start()
    {
        audioManager = GameObject.Find("ScoreKeeper").GetComponent<AudioManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(audioManager != null && collision.gameObject.tag != "OutsideRing") {
            audioManager.playRingImpact();   
        }

        if (collision.gameObject.tag == "1" || collision.gameObject.tag == "2" || collision.gameObject.tag == "3" || collision.gameObject.tag == "4" || collision.gameObject.tag == "Player")
        {
            timer = Time.realtimeSinceStartup;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "1" || collision.gameObject.tag == "2" || collision.gameObject.tag == "3" || collision.gameObject.tag == "4" || collision.gameObject.tag == "Player")
        {
            if (Time.realtimeSinceStartup-timer >= 7.0)
            {
                Destroy(gameObject);
            }
        }
    }

    [PunRPC]
    public void DestroyRing()
    {
        PhotonNetwork.Destroy(this.gameObject);
    }
}
