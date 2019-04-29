using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private PlayerMove pMovement;

    public Rect upBtnPos;
    public Rect downBtnPos;
    public Rect leftBtnPos;
    public Rect rightBtnPos;

    public Vector2 btnSize = new Vector2(60, 60);

    public Vector2 upBtnP = new Vector2(39, 390);
    public Vector2 downBtnP = new Vector2(39, 485);
    public Vector2 rightBtnP = new Vector2(80, 440);
    public Vector2 leftBtnP = new Vector2(0, 440);

    private Touch firstTouch;
    public Vector3 touchPos;

    private void Start()
    {
        pMovement = GameObject.Find("OrcDude").GetComponent<PlayerMove>();
    }

    private Vector2 ConvertPoint(Vector3 pos)
    {
        //Debug.Log("touched at: " + Camera.main.WorldToScreenPoint(pos));
        return Camera.main.WorldToScreenPoint(pos);
    }

    private void printScreenInfo()
    {
        Debug.Log("Screen Height (pixels): " + Screen.height);
        Debug.Log("Screen Width (pixels): " + Screen.width);

        
    }

    private void Update()
    {
        printScreenInfo();
        if (Input.touchCount > 0)
        {
            firstTouch = Input.touches[0];
            Vector3 touchPos = firstTouch.position;
            //Debug.Log("Touched at: " + touchPos.ToString());
            //Vector2 touchPos = ConvertPoint(firstTouch.position);

            if ((touchPos.x >= Screen.width / 2 && touchPos.x <= Screen.width)
                && (touchPos.y <= Screen.height / 2 && touchPos.y >= 0))
            {
                Debug.Log("hitting Q1");
            }
            if ((touchPos.x <= Screen.width/2 && touchPos.x >= 0)
                && (touchPos.y <= Screen.height/2 && touchPos.y >= 0))
            {
                Debug.Log("hitting Q2");
            }
            if ((touchPos.x <= Screen.width / 2 && touchPos.x >= 0)
                && (touchPos.y >= Screen.height / 2 && touchPos.y <= Screen.height))
            {
                Debug.Log("hitting Q3");
            }

            if ((touchPos.x >= Screen.width / 2 && touchPos.x <= Screen.width)
                && (touchPos.y >= Screen.height / 2 && touchPos.y <= Screen.height))
            {
                Debug.Log("hitting Q4");
            }
        }
    }

    //private void OnGUI()
    //{

    //    //upBtnPos = new Rect(new Vector2(upBtnP.x, upBtnP.y), btnSize);
    //    //downBtnPos = new Rect(new Vector2(downBtnP.x, downBtnP.y), btnSize);
    //    //rightBtnPos = new Rect(new Vector2(rightBtnP.x, rightBtnP.y), btnSize);
    //    //leftBtnPos = new Rect(new Vector2(leftBtnP.x, leftBtnP.y), btnSize);
    //    Vector2 vect = ConvertPoint(new Vector3(upBtnP.x, upBtnP.y, 0));
    //    upBtnPos = new Rect(vect, btnSize);


    //    if (Input.touchCount > 0)
    //    {
    //        firstTouch = Input.touches[0];

    //        Vector2 touchPos = ConvertPoint(firstTouch.position);

    //        RaycastHit hit;

    //        if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(touchPos.x, touchPos.y)), out hit, 100))
    //        {
    //            if ((hit.transform.position.x <= upBtnPos.x + .1f && hit.transform.position.x >= upBtnPos.x - .1f)
    //                   && (hit.transform.position.y <= upBtnPos.y + .1f && hit.transform.position.y >= upBtnPos.y - .1f))
    //            {
    //                pMovement.MoveUp();
    //            }


    //            //if ((touchPos.x <= upBtnPos.x + .1f && touchPos.x >= upBtnPos.x - .1f)
    //            // && (touchPos.y <= upBtnPos.y + .1f && touchPos.y >= upBtnPos.y - .1f))
    //            //{
    //            //    pMovement.MoveUp();
    //            //}
    //        }
    //    }
        //}
        ////down
        //if (Input.touchCount > 0)
        //{
        //    firstTouch = Input.touches[0];

        //    Vector3 touchPos = ConvertPoint(firstTouch.position);
        //    downBtnPos.position = ConvertPoint(downBtnPos.position);
        //    if ((touchPos.x <= downBtnPos.x + .1f && touchPos.x >= downBtnPos.x - .1f)
        //        && (touchPos.y <= downBtnPos.y + .1f && touchPos.y >= downBtnPos.y - .1f))
        //    {
        //        pMovement.MoveDown();
        //    }
        //}

        ////left
        //if (Input.touchCount > 0)
        //{
        //    firstTouch = Input.touches[0];

        //    Vector3 touchPos = ConvertPoint(firstTouch.position);
        //    leftBtnPos.position = ConvertPoint(leftBtnPos.position);
        //    if ((touchPos.x <= leftBtnPos.x + .1f && touchPos.x >= leftBtnPos.x - .1f)
        //        && (touchPos.y <= leftBtnPos.y + .1f && touchPos.y >= leftBtnPos.y - .1f))
        //    {
        //        pMovement.MoveLeft();
        //    }
        //}
        ////right
        //if (Input.touchCount > 0)
        //{
        //    firstTouch = Input.touches[0];

        //    Vector3 touchPos = ConvertPoint(firstTouch.position);
        //    rightBtnPos.position = ConvertPoint(rightBtnPos.position);
        //    if ((touchPos.x <= rightBtnPos.x + .1f && touchPos.x >= rightBtnPos.x - .1f)
        //        && (touchPos.y <= rightBtnPos.y + .1f && touchPos.y >= rightBtnPos.y - .1f))
        //    {
        //        pMovement.MoveRight();
        //    }
        //}

    //}
}
