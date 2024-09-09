using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public Transform mainCam;
    public Transform midBG;
    public Transform sideBG;
    public float lenghtBG;

    // Update is called once per frame
    void Update()
    {
        if (mainCam.position.x > midBG.position.x)
        {
            UpdateBackGroundPosition(Vector3.right);
        }
        else if (mainCam.position.x < midBG.position.x)
        {
            UpdateBackGroundPosition(Vector3.left);
        }
    }

    void UpdateBackGroundPosition(Vector3 direction)
    {
        sideBG.position = midBG.position + direction * lenghtBG;
        Transform temp = midBG;
        midBG = sideBG;
        sideBG = temp;
    }
}
