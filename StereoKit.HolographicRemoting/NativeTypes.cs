using System;
using System.Runtime.InteropServices;

namespace StereoKit.HolographicRemoting
{
    enum XrStructureType : UInt64
    {
        REMOTING_REMOTE_CONTEXT_PROPERTIES_MSFT             = 1000065000,
        REMOTING_CONNECT_INFO_MSFT                          = 1000065001,
        REMOTING_LISTEN_INFO_MSFT                           = 1000065002,
        REMOTING_DISCONNECT_INFO_MSFT                       = 1000065003,
        REMOTING_EVENT_DATA_LISTENING_MSFT                  = 1000065004,
        REMOTING_EVENT_DATA_CONNECTED_MSFT                  = 1000065005,
        REMOTING_EVENT_DATA_DISCONNECTED_MSFT               = 1000065006,
        REMOTING_AUTHENTICATION_TOKEN_REQUEST_MSFT          = 1000065007,
        REMOTING_CERTIFICATE_DATA_MSFT                      = 1000065008,
        REMOTING_CERTIFICATE_VALIDATION_RESULT_MSFT         = 1000065009,
        REMOTING_SERVER_CERTIFICATE_VALIDATION_MSFT         = 1000065010,
        REMOTING_AUTHENTICATION_TOKEN_VALIDATION_MSFT       = 1000065011,
        REMOTING_SERVER_CERTIFICATE_REQUEST_MSFT            = 1000065012,
        REMOTING_SECURE_CONNECTION_CLIENT_CALLBACKS_MSFT    = 1000065013,
        REMOTING_SECURE_CONNECTION_SERVER_CALLBACKS_MSFT    = 1000065014,
        EVENT_DATA_REMOTING_DATA_CHANNEL_CREATED_MSFT       = 1000065015,
        EVENT_DATA_REMOTING_DATA_CHANNEL_OPENED_MSFT        = 1000065016,
        EVENT_DATA_REMOTING_DATA_CHANNEL_DATA_RECEIVED_MSFT	= 1000065017,
        EVENT_DATA_REMOTING_DATA_CHANNEL_CLOSED_MSFT        = 1000065018,
        REMOTING_DATA_CHANNEL_CREATE_INFO_MSFT              = 1000065019,
        REMOTING_DATA_CHANNEL_STATE_MSFT                    = 1000065020,
        REMOTING_DATA_CHANNEL_SEND_DATA_INFO_MSFT           = 1000065021,
        EVENT_DATA_REMOTING_TIMESTAMP_CONVERSION_READY_MSFT = 1000065022,
        REMOTING_AUDIO_OUTPUT_CAPTURE_SETTINGS_MSFT         = 1000065023,
    }

    enum XrResult : Int32
    {
        Success = 0,
    }

    enum XrRemotingVideoCodecMSFT : UInt32
    {
        ANY  = 0,
        H264 = 1,
        H265 = 2,
        MAX_ENUM  = 0x7FFFFFFF
    }

    enum XrRemotingDepthBufferStreamResolutionMSFT : UInt32
    {
        HALF     = 0,
        FULL     = 1,
        QUARTER  = 2,
        DISABLED = 3,
        MAX_ENUM = 0x7FFFFFFF
    }

    enum XrRemotingConnectionStateMSFT : UInt32
    {
        DISCONNECTED = 0,
        CONNECTING   = 1,
        CONNECTED    = 2,
        MAX_ENUM     = 0x7FFFFFFF
    }

    enum XrRemotingDisconnectReasonMSFT : UInt32
    {
        NONE                             = 0,
        UNKNOWN                          = 1,
        NO_SERVER_CERTIFICATE            = 2,
        HANDSHAKE_PORT_BUSY              = 3,
        HANDSHAKE_UNREACHABLE            = 4,
        HANDSHAKE_CONNECTION_FAILED      = 5,
        AUTHENTICATION_FAILED            = 6,
        REMOTING_VERSION_MISMATCH        = 7,
        INCOMPATIBLE_TRANSPORT_PROTOCOLS = 8,
        HANDSHAKE_FAILED                 = 9,
        TRANSPORT_PORT_BUSY              = 10,
        TRANSPORT_UNREACHABLE            = 11,
        TRANSPORT_CONNECTION_FAILED      = 12,
        PROTOCOL_VERSION_MISMATCH        = 13,
        PROTOCOL_ERROR                   = 14,
        VIDEO_CODEC_NOT_AVAILABLE        = 15,
        CANCELED                         = 16,
        CONNECTION_LOST                  = 17,
        DEVICE_LOST                      = 18,
        DISCONNECT_REQUEST               = 19,
        HANDSHAKE_NETWORK_UNREACHABLE    = 20,
        HANDSHAKE_CONNECTION_REFUSED     = 21,
        VIDEO_FORMAT_NOT_AVAILABLE       = 22,
        PEER_DISCONNECT_REQUEST          = 23,
        PEER_DISCONNECT_TIMEOUT          = 24,
        SESSION_OPEN_TIMEOUT             = 25,
        REMOTING_HANDSHAKE_TIMEOUT       = 26,
        INTERNAL_ERROR                   = 27,
        HANDSHAKE_PERMISSION_DENIED      = 28,
        MAX_ENUM                         = 0x7FFFFFFF
    }

    enum XrRemotingAudioOutputCaptureModeMSFT : UInt32
    {
        AUDIO_OUTPUT_CAPTURE_MODE_SYSTEM_WIDE_MSFT  = 0,
        AUDIO_OUTPUT_CAPTURE_MODE_APP_ONLY_MSFT     = 1
    }

    [StructLayout(LayoutKind.Sequential)]
    struct XrRemotingRemoteContextPropertiesMSFT
    {
        public XrStructureType                           type;
        public IntPtr                                    next;
        public uint                                      maxBitrateKbps;
        public int                                       enableAudio;
        public XrRemotingVideoCodecMSFT                  videoCodec;
        public XrRemotingDepthBufferStreamResolutionMSFT depthBufferStreamResolution;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct XrRemotingConnectInfoMSFT
    {
        public XrStructureType type;
        public IntPtr          next;
        public IntPtr          remoteHostName;
        public ushort          remotePort;
        public int             secureConnection;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct XrRemotingAudioOutputCaptureSettingsMSFT
    {
        public XrStructureType                      type;
        public IntPtr                               next;
        public XrRemotingAudioOutputCaptureModeMSFT audioOutputCaptureMode;
    }
}