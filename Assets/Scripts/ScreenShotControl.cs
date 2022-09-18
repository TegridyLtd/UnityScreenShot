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

using System.IO;
using UnityEngine;

public class ScreenShotControl : MonoBehaviour
{
    public Camera shotCam;

    public int imageHeight;
    public int imageWidth;

    public string folderName = "ScreenShots";
    public string fileName = "Image";

    void Awake()
    {
        //setup the cameras target and disable it till needed
        shotCam.targetTexture = new RenderTexture(imageWidth, imageHeight, 24);
        shotCam.enabled = false;
    }

    public void TakePicture()
    {
        //Render the camera and set the current active render
        shotCam.enabled = true;
        shotCam.Render();
        RenderTexture.active = shotCam.targetTexture;

        //save the RenderTexture to a texture 
        Texture2D screenShot = new(imageWidth, imageHeight, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0, 0, imageWidth, imageHeight), 0, 0);

        //Create a byte array and save the data to disk
        byte[] saveData = screenShot.EncodeToPNG();

        //make sure the folder exists and write the file
        if (!Directory.Exists(Application.dataPath + "/" + folderName)) Directory.CreateDirectory(Application.dataPath + "/" + folderName);
        File.WriteAllBytes(FileNameTimeStamp(), saveData);
        shotCam.enabled = false;
    }

    private string FileNameTimeStamp()
    {
        return Application.dataPath + "/" + folderName + "/" + fileName + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
    }
}
