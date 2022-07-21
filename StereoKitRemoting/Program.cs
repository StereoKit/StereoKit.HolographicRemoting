using StereoKit;
using StereoKit.HolographicRemoting;
using System;


// Adding the Holographic Remoting IStepper based on command line
// arguments allows us to create separate Launch Profiles for running
// locally vs. running with remoting via Visual Studio's UI! Note the
// "Run - Remote" and "Run - Local" options in the run bar.
const string defaultIp   = "192.168.1.231";
int          remotingArg = Array.IndexOf(args, "-remote");
if (remotingArg != -1)
{
	// If the "-remote" option is followed by text that is not another
	// "-" option, we can treat that as an IP address other than the
	// default we have hard-coded here.
	string ip = (remotingArg+1 < args.Length && args[remotingArg + 1].StartsWith("-") == false)
		? args[remotingArg + 1]
		: defaultIp;

	SK.AddStepper(new HolographicRemoting(ip));
}


// Initialize StereoKit
SKSettings settings = new SKSettings {
	appName      = "StereoKitRemoting",
	assetsFolder = "Assets",
};
if (!SK.Initialize(settings))
	Environment.Exit(1);


// Create assets used by the app
Pose  cubePose = new Pose(0,0,-0.5f, Quat.Identity);
Model cube     = Model.FromMesh(
	Mesh.GenerateRoundedCube(Vec3.One*0.1f, 0.02f),
	Default.MaterialUI);

Matrix   floorTransform = Matrix.TS(0, -1.5f, 0, new Vec3(30, 0.1f, 30));
Material floorMaterial  = new Material(Shader.FromFile("floor.hlsl"));
floorMaterial.Transparency = Transparency.Blend;


// Core application loop
SK.Run(() => {
	if (SK.System.displayType == Display.Opaque)
		Default.MeshCube.Draw(floorMaterial, floorTransform);

	UI.Handle("Cube", ref cubePose, cube.Bounds);
	cube.Draw(cubePose.ToMatrix());
});