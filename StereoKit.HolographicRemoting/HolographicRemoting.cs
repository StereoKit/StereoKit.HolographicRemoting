using StereoKit.Framework;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace StereoKit.HolographicRemoting
{
	public class HolographicRemoting : IStepper
	{
		static readonly string remotingExtName = "XR_MSFT_holographic_remoting";

		private string	_ipAddress;
		private ushort	_port;
		private bool	_sendAudio;
		private int		_maxBitrate;
		private bool	_wideAudioCapture;

		public  bool Enabled { get; private set; }

		public HolographicRemoting(string ipAddress, ushort port = 8265, bool sendAudio = true, int maxBitrateKbps = 20000, bool wideAudioCapture = true)
		{
			_ipAddress			= ipAddress;
			_port				= port;
			_sendAudio			= sendAudio;
			_maxBitrate			= maxBitrateKbps;
			_wideAudioCapture	= wideAudioCapture;

			if (SK.IsInitialized)
				Log.Err("HolographicRemoting must be constructed before StereoKit is initialized!");

			Backend.OpenXR.RequestExt(remotingExtName);
			Backend.OpenXR.OnPreCreateSession += OnPreCreateSession;

			// Set up the OpenXR manifest for the remoting runtime!
			string runtimePath = Path.GetDirectoryName(typeof(HolographicRemoting).Assembly.Location);
			runtimePath = Path.Combine(runtimePath, "RemotingXR.json");
			Environment.SetEnvironmentVariable("XR_RUNTIME_JSON", runtimePath);
		}

		public bool Initialize	() => Enabled;
		public void Shutdown	() { }
		public void Step		() { }

		void OnPreCreateSession()
		{
			Enabled = Backend.OpenXR.ExtEnabled(remotingExtName);
			if (!Enabled) return;

			NativeAPI.LoadFunctions();

			Log.Info($"Connecting to Holographic Remoting Player at {_ipAddress} : {_port} ...");

			XrRemotingRemoteContextPropertiesMSFT contextProperties = new XrRemotingRemoteContextPropertiesMSFT();
			contextProperties.type							= XrStructureType.REMOTING_REMOTE_CONTEXT_PROPERTIES_MSFT;
			contextProperties.enableAudio					= _sendAudio ? 1 : 0;
			contextProperties.maxBitrateKbps				= (uint)_maxBitrate;
			contextProperties.videoCodec					= XrRemotingVideoCodecMSFT.H265;
			contextProperties.depthBufferStreamResolution	= XrRemotingDepthBufferStreamResolutionMSFT.HALF;

			if (_sendAudio && !_wideAudioCapture)
			{
				XrRemotingAudioOutputCaptureSettingsMSFT audioCaptureSettings = new XrRemotingAudioOutputCaptureSettingsMSFT();
				audioCaptureSettings.type					= XrStructureType.REMOTING_AUDIO_OUTPUT_CAPTURE_SETTINGS_MSFT;
				audioCaptureSettings.audioOutputCaptureMode = XrRemotingAudioOutputCaptureModeMSFT.AUDIO_OUTPUT_CAPTURE_MODE_APP_ONLY_MSFT;
				
				int		size	= Marshal.SizeOf(typeof(XrRemotingAudioOutputCaptureSettingsMSFT));
				IntPtr	memory	= Marshal.AllocHGlobal(size);
				Marshal.StructureToPtr(audioCaptureSettings, memory, false);

				contextProperties.next = memory;
				Marshal.FreeHGlobal(memory);
			}

			if (NativeAPI.xrRemotingSetContextPropertiesMSFT(Backend.OpenXR.Instance, Backend.OpenXR.SystemId, contextProperties) != XrResult.Success)
			{
				Log.Warn("xrRemotingSetContextPropertiesMSFT failed!");
			}

			XrRemotingConnectInfoMSFT connectInfo = new XrRemotingConnectInfoMSFT();
			connectInfo.type				= XrStructureType.REMOTING_CONNECT_INFO_MSFT;
			connectInfo.remoteHostName		= Marshal.StringToHGlobalAnsi(_ipAddress);
			connectInfo.remotePort			= _port;
			connectInfo.secureConnection	= 0;
			XrResult result = NativeAPI.xrRemotingConnectMSFT(Backend.OpenXR.Instance, Backend.OpenXR.SystemId, connectInfo);
			if (result != XrResult.Success)
			{
				Log.Warn("xrRemotingConnectMSFT failed! " + result);
			}
			Marshal.FreeHGlobal(connectInfo.remoteHostName);
		}
	}
}
