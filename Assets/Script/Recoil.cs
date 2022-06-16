using UnityEngine;

public class Recoil : MonoBehaviour
{
    private Vector3 currentRotation;
    private Vector3 targetRotation;
    //public float recoilX;
    //public float recoilY;
    //public float recoilZ;

    [Header("Camera Recoil Generic Stats")]
    public float snappiness;
    public float returnSpeed;
    void Start()
    {
        
    }

    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void RecoilFire(float verticalRecoil, float horizontalRecoil, float sphericalShake, float transitionalSnappiness, float recoilReturnSpeed)
    {
        snappiness = transitionalSnappiness;
        returnSpeed = recoilReturnSpeed;
        targetRotation += new Vector3(-verticalRecoil, Random.Range(-horizontalRecoil, horizontalRecoil), Random.Range(-sphericalShake, sphericalShake));
    }
}
