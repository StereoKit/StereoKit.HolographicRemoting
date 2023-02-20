namespace StereoKit.HolographicRemoting
{
	static class NativeAPI
	{
		public delegate XrResult del_xrRemotingConnectMSFT				(ulong instance, ulong systemId, in XrRemotingConnectInfoMSFT connectInfo);
		public delegate XrResult del_xrRemotingSetContextPropertiesMSFT	(ulong instance, ulong systemId, in XrRemotingRemoteContextPropertiesMSFT contextProperties);
		public delegate XrResult del_xrRemotingGetConnectionStateMSFT	(ulong instance, ulong systemId, out XrRemotingConnectionStateMSFT connectionState, out XrRemotingDisconnectReasonMSFT lastDisconnectReason);

		public static del_xrRemotingConnectMSFT					xrRemotingConnectMSFT;
		public static del_xrRemotingSetContextPropertiesMSFT	xrRemotingSetContextPropertiesMSFT;
		public static del_xrRemotingGetConnectionStateMSFT		xrRemotingGetConnectionStateMSFT;

		public static void LoadFunctions()
		{
			xrRemotingConnectMSFT				= Backend.OpenXR.GetFunction<del_xrRemotingConnectMSFT             >("xrRemotingConnectMSFT");
			xrRemotingSetContextPropertiesMSFT	= Backend.OpenXR.GetFunction<del_xrRemotingSetContextPropertiesMSFT>("xrRemotingSetContextPropertiesMSFT");
			xrRemotingGetConnectionStateMSFT	= Backend.OpenXR.GetFunction<del_xrRemotingGetConnectionStateMSFT  >("xrRemotingGetConnectionStateMSFT");
		}
	}
}