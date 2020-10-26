using System.Collections;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public float distance = 8.0f;
    public float height = 5.0f;
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;
    public Transform target, MainCamera;
    public MoveShip MoveShip;
    private float shakeX = 0.05f, shakeY = 0.05f;
    private int Zoom = 30, Normal = 60, Smoth = 5;
    private Camera Camera;
    private AudioSource AudioSource;
    private AudioClip[] AudioClips;

    private void Awake()
    {
        //Get componet
        Camera = GetComponent<Camera>();
        AudioSource = GetComponent<AudioSource>();
        //Load Audio
        AudioClips = Resources.LoadAll<AudioClip>("Sound");
    }
    /// <summary>
    /// Screen Shake
    /// </summary>
    /// <returns></returns>
    IEnumerator Pause()
    {
        Vector3 OriginPlaceCamera = new Vector3(0, 4.5f, transform.position.z);
        yield return new WaitForSeconds(0.5f);
        MoveShip.GameOver = false;
        MainCamera.transform.localPosition = OriginPlaceCamera;
        Time.timeScale = 0;
        MoveShip.GameOverPanel.SetActive(true);
        if(MoveShip.Record == true)
        {
            MoveShip.RecordMessage.SetActive(true);
            AudioSource.Stop();
            AudioSource.PlayOneShot(AudioClips[1]);
        }

    }
    private void LateUpdate()
    {
        // Early out if we don't have a target
        if (!target)
        {
            return;
        }
        //Zoom camera
        if (Input.GetKey(KeyCode.Space))
        {

            Camera.fieldOfView = Mathf.Lerp(Camera.fieldOfView , Zoom, Time.deltaTime * Smoth);
            height = 2.5f;
        }
        else
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(Camera.fieldOfView, Normal, Time.deltaTime * Smoth);
            height = 3.0f;
        }
        // Calculate the current rotation angles
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        var pos = transform.position;
        pos = target.position - currentRotation * Vector3.forward * distance;
        pos.y = currentHeight;
        transform.position = pos;

        // Always look at the target
        transform.LookAt(target);
        if (MoveShip.GameOver == true)
        {
            Vector3 Shake = new Vector3(Random.Range(-shakeX, shakeX), Random.Range(-shakeY, shakeY), 0);
            MainCamera.transform.localPosition += Shake;
            MoveShip.ExplosionObj.SetActive(true);
            StartCoroutine("Pause");
        }
    }
}