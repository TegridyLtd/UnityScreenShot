/////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2022 Tegridy Ltd                                          //
// Author: Darren Braviner                                                 //
// Contact: db@tegridygames.co.uk                                          //
/////////////////////////////////////////////////////////////////////////////
//                                                                         //
// This program is free software; you can redistribute it and/or modify    //
// it under the terms of the GNU General Public License as published by    //
// the Free Software Foundation; either version 2 of the License, or       //
// (at your option) any later version.                                     //
//                                                                         //
// This program is distributed in the hope that it will be useful,         //
// but WITHOUT ANY WARRANTY.                                               //
//                                                                         //
/////////////////////////////////////////////////////////////////////////////
//                                                                         //
// You should have received a copy of the GNU General Public License       //
// along with this program; if not, write to the Free Software             //
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,              //
// MA 02110-1301 USA                                                       //
//                                                                         //
/////////////////////////////////////////////////////////////////////////////

using UnityEngine;
public class FlyCam : MonoBehaviour
{
    public ScreenShotControl screenShot;
    public float minSpeed = 0.1f;
    public float maxSpeed = 5.0f;
    public float mouseSpeed = 4.0f;

    float speed;
    private Vector3 rotation;

    private void OnEnable(){ Cursor.lockState = CursorLockMode.Locked; }
    private void OnDisable() { Cursor.lockState = CursorLockMode.None; }


    private void Update()
    {
        //Check if user wants to change move speed
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) speed += 0.1f;
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) speed -= 0.1f;
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);

        //rotate the camera with the mouse movement
        rotation = transform.eulerAngles;
        rotation.x -= Input.GetAxis("Mouse Y") * mouseSpeed;
        rotation.y += Input.GetAxis("Mouse X") * mouseSpeed;
        transform.eulerAngles = rotation;

        //Move the game object horizontally
        transform.position +=
            Input.GetAxis("Horizontal") * speed * transform.right +
            Input.GetAxis("Vertical") * speed * transform.forward;


        //move the game object vertically 
        if (Input.GetKey(KeyCode.E))
        { transform.position += speed * transform.up; }

        if (Input.GetKey(KeyCode.Q))
        { transform.position -= speed * transform.up; }

        //Take a screenshot if the player pushes fire
        if (Input.GetButton("Fire1")) screenShot.TakePicture(); 
    }
}
