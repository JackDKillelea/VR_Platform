using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JacksScripts
{

    public class CameraDraggingVR : MonoBehaviour
    {
#if UNITY_EDITOR

        bool isDragging = false; // flag to keep track of draging
        float startPosX; // mouse start position X
        float startPosY; // mouse start position Y

        Camera cam; // camera component

        // Use this for initialization
        void Start()
        {
            cam = GetComponent<Camera>();
        }

        // Update is called once per frame
        void Update()
        {
            // if right click is pressed then do nothing but if dragged at the same time do something
            if (Input.GetMouseButtonDown(1) && !isDragging)
            {
                isDragging = true;
                startPosX = Input.mousePosition.x; // initial mouse point X
                startPosY = Input.mousePosition.y; // initial mouse point Y
            }
            else if (Input.GetMouseButtonUp(1) && isDragging) // if the right button is not pressed but we are dragging
            {
                isDragging = false;
            }
        }

        void LateUpdate()
        {
            if (isDragging)
            {   // get the end position of where the mouse is
                float endPosX = Input.mousePosition.x;
                float endPosY = Input.mousePosition.y;

                // get the difference by the two positions
                float difX = endPosX - startPosX;
                float difY = endPosY - startPosY;

                // get the new screen centre
                float newCentreX = Screen.width / 2 + difX;
                float newCentreY = Screen.height / 2 + difY;

                // get the world coord
                Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(newCentreX, newCentreY, cam.nearClipPlane));

                // makes the camera look at the worldPos
                transform.LookAt(worldPos);

                // starting pos for next call so it doesnt use the original start every time
                startPosX = endPosX;
                startPosY = endPosY;
            }
        }
#endif
    }
}
