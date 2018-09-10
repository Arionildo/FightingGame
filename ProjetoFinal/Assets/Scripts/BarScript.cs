using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{

    public Transform Bar;
    public Slider barSlider;

    public float currentResource;

    public float maxResource;

    public float BarYOffset = 2;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            TestBar();
        }
    }

    public void ChangeResource(int amount)
    {
        currentResource += amount;
        currentResource = Mathf.Clamp(currentResource, 0, maxResource);
        barSlider.value = (currentResource / maxResource)*100;
        Debug.Log(barSlider.value);
    }

    //private void PositionBar()
    //{
    //    Vector3 currentPos = transform.position;

    //    Bar.position = new Vector3(currentPos.x, currentPos.y + BarYOffset, currentPos.z);

    //    Bar.LookAt(Camera.main.transform);
    //}

    private void TestBar()
    {
        Debug.Log("diminuindo vida");
        ChangeResource(-5);
    }
}
