using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    public GameObject Target;
    void Update()
    {
        this.transform.rotation = Quaternion.Euler(0, Target.transform.rotation.eulerAngles.y + 90, 0);
    }
}
