using UnityEngine;

public class PlayerRespawn: MonoBehaviour
{
    public float threshold;
    public bool isCheckPointTook;

    void FixedUpdate() {
        if  (transform.position.y < threshold){
            if (isCheckPointTook) {
                transform.position = new Vector3(-7.82f, 11.37181f, -11.1343f);
            } else {
                transform.position = new Vector3(-0.02f, 0.022f, 0.87f);
            }
        }
    }
}