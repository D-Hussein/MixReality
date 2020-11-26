using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class MusicCube : MonoBehaviour
{

    //Sound
    private AudioSource source;
    public AudioClip keySound;
    public VideoClip videoClip;

    private bool on = false;
    private bool keyhit = false;
    private GameObject key;

    private float keyDownDist = 0.025f;
    private float returnSpeed = 0.001f;
    private float keyOriginalY;

    private float hitAgainTime = 0.5f;
    private float canHitAgain;

    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        var videoPlayer = gameObject.AddComponent<VideoPlayer>();
        key = transform.GetChild(0).gameObject;
        keyOriginalY = key.transform.position.y;
        videoPlayer.playOnAwake = false;
        videoPlayer.clip = videoClip;
        source.GetComponent<AudioSource>().volume = 0.2f;
        // videoPlayer.targetMaterialRenderer
        // videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.MaterialOverride;
        //  videoPlayer.targetMaterialRenderer = GetComponent<Renderer>();
        //  videoPlayer.targetMaterialProperty = "_MainTex";
        // videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        // videoPlayer.SetTargetAudioSource(0, audioSource);


    }

    // Update is called once per frame
    void Update()
    {
        var vp = GetComponent<VideoPlayer>();
        if (keyhit == true)
        {

            if (vp.isPlaying && source.isPlaying)
            {
                vp.Stop();
                source.Stop();
                keyhit = false;
                on = false;

                key.transform.position = new Vector3(key.transform.position.x, key.transform.position.y - keyDownDist, key.transform.position.z);

            }
            else
            {
                vp.Play();
                source.PlayOneShot(keySound);
                keyhit = false;
                on = !on;

                key.transform.position = new Vector3(key.transform.position.x, key.transform.position.y - keyDownDist, key.transform.position.z);

            }

        }


        if (key.transform.position.y < keyOriginalY)
        {
            key.transform.position += new Vector3(0, returnSpeed, 0);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand") && canHitAgain < Time.time)
        {
            canHitAgain = Time.time + hitAgainTime;
            keyhit = true;
        }
    }
}
