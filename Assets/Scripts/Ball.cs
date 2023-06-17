using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public Transform target;
    public float force;
    public Slider forceUI;
    public float maxForce = 45f;
    public AudioClip hitSound;

    private AudioSource audioSource;

     void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            force = Mathf.Min(force + 0.5f, maxForce);
            slider();
        
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            shoot();
            StartCoroutine(wait());
        }
    }

    void shoot()
    {
        Vector3 Shoot = (target.position - this.transform.position).normalized;
        //GetComponent<Rigidbody>().AddForce(Shoot * force + new Vector3(0, 3f, 0), ForceMode.Impulse);
        GetComponent<Rigidbody>().AddForce(Shoot * force, ForceMode.Impulse);

        // Play the hit sound
     AudioSource.PlayClipAtPoint(hitSound, transform.position);
    }

    public void slider()
    {
        forceUI.value = force;
    }

    public void ResetGauge()
    {
        force = 0;
        forceUI.value = 0;
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1.5f);
        ResetGauge();
     
    }
}

