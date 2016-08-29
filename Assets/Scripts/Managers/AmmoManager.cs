using UnityEngine;
using UnityEngine.UI;

public class AmmoManager : MonoBehaviour
{
    public PlayerShooting playerShooting;

    private Text text;

    public void Start()
    {
        text = GetComponent<Text>();
    }

    public void Update()
    {
        text.text = "Ammo: " + playerShooting.currentAmmo;
    }
}