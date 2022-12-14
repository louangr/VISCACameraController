# VISCA Camera Controller

VISCA Camera Controller is a Windows desktop app to control a PTZ camera using the VISCA protocol via a serial communication.

<br>

<p align="center">
  <img src="./docs/dark.png" style="margin-right: 15px" />
  <img src="./docs/light.png" style="margin-left: 15px" />
</p>

<br>

## How to get it ?
Download the latest package in the `Releases` section.

<br>

## But if you don't trust my package...
You can deploy it yourself, for that you have to :
- download sources
- install Visual Studio
- open the solution (`VISCACameraController.sln`)
- download and install necessary SDKs
- in the Solution Explorer, right-click the `VISCACameraController (Package)` project, choose `Publish` and click `Create App Packages...`
- select `Sideloading`, then click `Next`
- select `Yes, select a certificate`, then click `Create` and fill fields
- trust your new certificate, then click `Next`
- select the targeted CPU architecture and ensure `Solution Configuration` is `Release`
- finally click `Create` and after compiling find your package in the `Output location` folder

<br>

## Hidden feature

The app was created for the purpose of using the presets only. This does not permit anyone to change the presets using the app.

But you can still set a preset by a hidden feature. To find out, double click on preset 8 while the app is not connecting. A new part will appear.