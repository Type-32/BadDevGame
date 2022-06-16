using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderManager : MonoBehaviour
{
    public GameObject gun;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(gun, transform.position, transform.rotation, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
