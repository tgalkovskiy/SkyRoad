using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private GameObject Explos;
    private bool DestroyAst = false;
    private AudioSource AudioSource;
    private AudioClip[] AudioClips;
    private void Start()
    {
        //Get componet
        AudioSource = GetComponent<AudioSource>();
        //Load Audio clips
        AudioClips = Resources.LoadAll<AudioClip>("Sound");
    }
    private void OnTriggerEnter(Collider other)
    {
        //Collision shot at asteroid
        if(other.tag == "Shot")
        {
            //Sound collision
            //AudioSource.PlayClipAtPoint(AudioClips[0], transform.position, 1f);
            AudioSource.PlayOneShot(AudioClips[0]);
            //Spawn Explosion
            Explos = Instantiate(Resources.Load<GameObject>("Prefabs/Explosion"));
            //Transform Explosion
            Explos.transform.position = transform.position;
            //Destroy Shot
            Destroy(other.gameObject);
            DestroyAst = true;
        }
    }
    /// <summary>
    /// Destroy game object
    /// </summary>
    /// <returns></returns>
    IEnumerator AsteroidDestroy()
    {
        yield return new WaitForSeconds(0.4f);
        //Destros Asteroid
        Destroy(gameObject);
        //Destroy Explosion
        Destroy(Explos);
    }
    private void Update()
    {
        if (DestroyAst == true)
        {
            //Strart Coroutine
            StartCoroutine("AsteroidDestroy");
        }
    }
}
