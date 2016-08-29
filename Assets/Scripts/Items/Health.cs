using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public int value = 20;

    public void Update()
    {
        transform.Rotate(new Vector3(-15, -30, -45) * Time.deltaTime);
    }
}