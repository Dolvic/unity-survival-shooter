using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour
{
    public void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}