using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MusicKey : MonoBehaviour
{

    //Sound
    private AudioSource source;
    public AudioClip keySound;

    private bool on = false;
    private bool keyhit = false;
    private GameObject key;

    private float keyDownDist = 0.025f;
    private float returnSpeed = 0.001f;
    private float keyOriginalY;

    private float hitAgainTime = 0.005f;
    private float canHitAgain;

    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();

        key = transform.GetChild(0).gameObject;
        keyOriginalY = key.transform.position.y;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(keyhit == true)
        {
            source.PlayOneShot(keySound);
            keyhit = false;
            on = !on;

            key.transform.position = new Vector3(key.transform.position.x, key.transform.position.y - keyDownDist, key.transform.position.z);


        }

        if(key.transform.position.y < keyOriginalY)
        {
            key.transform.position += new Vector3(0, returnSpeed, 0);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerFinger") && canHitAgain < Time.time)
        {
            canHitAgain = Time.time + hitAgainTime;
            keyhit = true;
        }
    }
}
