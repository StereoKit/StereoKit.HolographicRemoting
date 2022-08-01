# StereoKit.HolographicRemoting

<p align="center">
    <a href="https://tldrlegal.com/license/mit-license"><img src="https://img.shields.io/github/license/StereoKit/StereoKit.HolographicRemoting" /></a>
    <a href="https://www.nuget.org/packages/StereoKit.HolographicRemoting/"><img src="https://img.shields.io/nuget/v/StereoKit.HolographicRemoting" /></a>
</p>

This [NuGet package](https://www.nuget.org/packages/StereoKit.HolographicRemoting) is an implementation of HoloLens' [Holographic Remoting](https://docs.microsoft.com/en-us/windows/mixed-reality/develop/native/holographic-remoting-overview) for StereoKit! This is an easy way to get desktop functionality onto your HoloLens, or cut down on iteration time while testing on-device. Among other things, Holographic Remoting does _not_ require UWP, so you can use .NET Core projects with this!

## Getting started

First, add StereoKit.HolographicRemoting as a NuGet package to your project!

Here's the _easiest_ possible integration of StereoKit.HolographicRemoting! The key element here is that HolographicRemoting should be added before `SK.Initialize` is called, as HolographicRemoting needs to request an OpenXR extension before OpenXR starts.

```CSharp
using StereoKit;
using StereoKit.HolographicRemoting;

// Replace the IP here with the IP of your HoloLens!
SK.AddStepper(new HolographicRemoting("192.168.1.231"));

SK.Initialize();
SK.Run(() => { });
```

A more robust option would be to add the option and IP address to your command line arguments. This pairs really well with .NET Core's launchSettings.json, where you can set up multiple launch profiles with different command line arguments!

```CSharp
using StereoKit;
using StereoKit.HolographicRemoting;

// You can invoke this app as:
// `app.exe -remote`
// or
// `app.exe -remote 192.168.1.231`
const string defaultIp = "192.168.1.231";
int          remoteArg = Array.IndexOf(args, "-remote");
if (remoteArg != -1)
{
	bool ipArg = (remoteArg + 1 < args.Length && args[remoteArg + 1].StartsWith("-") == false);
	SK.AddStepper(new HolographicRemoting(ipArg ? args[remoteArg + 1] : defaultIp));
}

SK.Initialize();
SK.Run(() => { });
```

## Limitations

Since Holographic Remoting means that sensor data is transmitted from the HoloLens to the PC, some HoloLens features or sensors may not be available! For example, camera aligned Mixed Reality Capture won't work through Holographic Remoting.

This package also does not implement 100% of the HoloGraphic Remoting feature set! It does all the important bits, but skips on some of the optional features such as remote speech, and custom data channels. If these are important to your project, feel free to open a PR or raise an issue!