using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float RotationSpeed;
    public enum Type
    {
        AMMOR, COIN, GRENDE, HEART, WEAPON
    };

    public Type type;
    public int Value;

    private void Update()
    {
        transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
    }


}
