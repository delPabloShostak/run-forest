using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    private int count;
    private int countCoub;
    private CharacterController _charController;
    public Text countText, leftText, winText;
    void Start()
    {
        _charController = GetComponent<CharacterController>();
        count = 0;
        winText.text = "";
        countCoub = GameObject.FindGameObjectsWithTag("gun").Length;
        setCount();
    }
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charController.Move(movement);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "gun")
        {
            Destroy(other.gameObject);
            count++;
            setCount();
        }
    }
    private void setCount()
    {
        countText.text = " Кількість :" + count.ToString();
        leftText.text = "Залишилось :" + (countCoub - count).ToString();
        if (count >= countCoub)
            winText.text = "Ліс зачищено!";
    }
}