using UnityEngine;
using System.Collections;

public class FOV : MonoBehaviour {
    public Transform target;
    void Update() {
        Vector3 targetDir = target.position - transform.position;
        Vector3 forward = transform.forward;
        float angle = Vector3.Angle(targetDir, forward);
        if (angle < 5.0F)
            Debug.Log("close");

    }
}