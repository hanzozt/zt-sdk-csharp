/*
Copyright NetFoundry Inc.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

https://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Hanzo ZT.NET.Tests")]
namespace Hanzo ZT.Native {
#pragma warning disable 0649
#pragma warning disable 0169

    public class TestBlitting {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
        public const int ZITI_EVENT_UNION_SIZE = TestBlitting.ptr * 5;
#if ZITI_64BIT
        public const int ptr = 8;
#else
        public const int ptr = 4;
#endif
        //Z4D_API zt_types_t* z4d_struct_test();
        [DllImport(API.Z4D_DLL_PATH, EntryPoint = "z4d_struct_test", CallingConvention = API.CALL_CONVENTION)]
        public static extern IntPtr z4d_struct_test();

        [DllImport(API.Z4D_DLL_PATH, EntryPoint = "z4d_zt_posture_query", CallingConvention = API.CALL_CONVENTION)]
        public static extern IntPtr z4d_zt_posture_query();

        public static T ToContextEvent<T>(T desired, IntPtr /*byte[] input*/ input) {
            int size = Marshal.SizeOf(desired);
            IntPtr ptr = IntPtr.Zero;
            try {
                ptr = Marshal.AllocHGlobal(size);
                byte[] destination = new byte[size];
                Marshal.Copy(input, destination, 0, size);
                Marshal.Copy(destination, 0, ptr, size);

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                desired = (T)Marshal.PtrToStructure(ptr, desired.GetType());
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            } finally {
                Marshal.FreeHGlobal(ptr);
            }

#pragma warning disable CS8603 // Possible null reference return.
            return desired;
#pragma warning restore CS8603 // Possible null reference return.
        }
    }

    public struct size_t {
#if ZITI_64BIT
        public long val;
#else
        public int val;
#endif
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct AlignmentCheck {
        [FieldOffset(0)] public uint offset;
        [FieldOffset(4)] public uint size;
        [FieldOffset(8)] public string checksum;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct zt_types_info {
        public uint total_size;
        public string checksum;
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct zt_types {
        public IntPtr size;
        public AlignmentCheck /*zt_id_cfg*/ f02_zt_id_cfg;
        public AlignmentCheck /*zt_config*/ f03_zt_config;
        public AlignmentCheck /*api_path*/ f04_api_path;
        public AlignmentCheck /*zt_api_versions*/ f05_zt_api_versions;
        public AlignmentCheck /*zt_version*/ f06_zt_version;
        public AlignmentCheck /*zt_identity*/ f07_zt_identity;
        public AlignmentCheck /*zt_process*/ f08_zt_process;
        public AlignmentCheck /*zt_posture_query*/ f09_zt_posture_query;
        public AlignmentCheck /*zt_posture_query_set*/ f10_zt_posture_query_set;
        public AlignmentCheck /*zt_session_type*/ f11_zt_session_type;
        public AlignmentCheck /*zt_service*/ f12_zt_service;
        public AlignmentCheck /*zt_address*/ f13_zt_address_host;
        public AlignmentCheck /*zt_address*/ f14_zt_address_cidr;
        public AlignmentCheck /*zt_client_cfg_v1*/ f15_zt_client_cfg_v1;
        public AlignmentCheck /*zt_intercept_cfg_v1*/ f16_zt_intercept_cfg_v1;
        public AlignmentCheck /*zt_server_cfg_v1*/ f17_zt_server_cfg_v1;
        public AlignmentCheck /*zt_listen_options*/ f18_zt_listen_options;
        public AlignmentCheck /*zt_host_cfg_v1*/ f19_zt_host_cfg_v1;
        public AlignmentCheck /*zt_host_cfg_v2*/ f20_zt_host_cfg_v2;
        public AlignmentCheck /*zt_mfa_enrollment*/ f21_zt_mfa_enrollment;
        public AlignmentCheck /*zt_port_range*/ f22_zt_port_range;
        public AlignmentCheck /*zt_options*/ f23_zt_options;

        //events
        public AlignmentCheck /*zt_event_t*/ f24_zt_context_event;
        public AlignmentCheck /*zt_event_t*/ f25_zt_router_event;
        public AlignmentCheck /*zt_event_t*/ f26_zt_service_event;
        public AlignmentCheck /*zt_event_t*/ f27_zt_mfa_auth_event;
        public AlignmentCheck /*zt_event_t*/ f28_zt_api_event;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct zt_types_with_values {
        public zt_types types;
        public zt_id_cfg zt_id_cfg;
        public zt_config zt_config;
        public zt_api_path zt_api_path;
        public zt_api_versions zt_api_versions;
        public zt_version zt_version;
        public zt_identity zt_identity;
        public zt_process zt_process;
        public zt_posture_query zt_posture_query;
        public zt_posture_query_set zt_posture_query_set;
        public zt_session_type zt_session_type;
        public zt_service zt_service;
        public zt_address zt_address_host;
        public zt_address zt_address_cidr;
        public zt_client_cfg_v1 zt_client_cfg_v1;
        public zt_intercept_cfg_v1 zt_intercept_cfg_v1;
        public zt_server_cfg_v1 zt_server_cfg_v1;
        public zt_listen_options zt_listen_options;
        public zt_host_cfg_v1 zt_host_cfg_v1;
        public zt_host_cfg_v2 zt_host_cfg_v2;
        public zt_mfa_enrollment zt_mfa_enrollment;
        public zt_port_range zt_port_range;
        public zt_options zt_options;
        public zt_context_event zt_context_event;
        public zt_router_event zt_router_event;
        public zt_service_event zt_service_event;
        public zt_mfa_auth_event zt_mfa_auth_event;
        public zt_api_event zt_api_event;
    }

    [StructLayout(LayoutKind.Explicit, Size = TestBlitting.ZITI_EVENT_UNION_SIZE)]
    public struct zt_context_event {
        [FieldOffset(0 * TestBlitting.ptr)]
        public zt_event_type zt_event_type;
        [FieldOffset(1 * TestBlitting.ptr)]
        public int ctrl_status;
        [FieldOffset(2 * TestBlitting.ptr)]
        public string err;

        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        //public byte[] _union;
    }

    public enum zt_router_status {
        EdgeRouterAdded,
        EdgeRouterConnected,
        EdgeRouterDisconnected,
        EdgeRouterRemoved,
        EdgeRouterUnavailable
    }

    [StructLayout(LayoutKind.Explicit, Size = TestBlitting.ZITI_EVENT_UNION_SIZE)]
    public struct zt_api_event {
        [FieldOffset(0 * TestBlitting.ptr)]
        public zt_event_type zt_event_type;
        [FieldOffset(1 * TestBlitting.ptr)]
        public string new_ctrl_address;
        [FieldOffset(2 * TestBlitting.ptr)]
        public string new_ca_bundle;
    };

    [StructLayout(LayoutKind.Explicit, Size = TestBlitting.ZITI_EVENT_UNION_SIZE)]
    public struct zt_mfa_auth_event {
        [FieldOffset(0 * TestBlitting.ptr)]
        public zt_event_type zt_event_type;
    };

    [StructLayout(LayoutKind.Explicit, Size = TestBlitting.ZITI_EVENT_UNION_SIZE)]
    public struct zt_service_event {
        [FieldOffset(0 * TestBlitting.ptr)]
        public zt_event_type zt_event_type;
        [FieldOffset(1 * TestBlitting.ptr)]
        public IntPtr removed;
        [FieldOffset(2 * TestBlitting.ptr)]
        public IntPtr changed;
        [FieldOffset(3 * TestBlitting.ptr)]
        public IntPtr added;
    }

    [StructLayout(LayoutKind.Explicit, Size = TestBlitting.ZITI_EVENT_UNION_SIZE)]
    public struct zt_router_event {
        [FieldOffset(0 * TestBlitting.ptr)]
        public zt_event_type zt_event_type;
        [FieldOffset(1 * TestBlitting.ptr)]
        public zt_router_status status;
        [FieldOffset(2 * TestBlitting.ptr)]
        public string name;
        [FieldOffset(3 * TestBlitting.ptr)]
        public string address;
        [FieldOffset(4 * TestBlitting.ptr)]
        public string version;
    }

    //old..[StructLayout(LayoutKind.Sequential)]
    //old..public struct zt_context_event {
    //old..    public int ctrl_status;
    //old..    public IntPtr err;
    //old..};
    public enum zt_event_type {
        ZitiContextEvent = 1,
        ZitiRouterEvent = 1 << 1,
        ZitiServiceEvent = 1 << 2,
        ZitiMfaAuthEvent = 1 << 3,
        ZitiAPIEvent = 1 << 4,
    }


    public enum zt_metric_type {
        EWMA_1m,
        EWMA_5m,
        EWMA_15m,
        MMA_1m,
        CMA_1m,
        EWMA_5s,
        INSTANT,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct zt_options {
        public string config;
        public bool disabled;
        public IntPtr /*public char**/ config_types;
        public uint api_page_size;
#if ZITI_64BIT
                public UInt32 refresh_interval; //the duration in seconds between checking for updates from the controller
#else
        public int refresh_interval; //the duration in seconds between checking for updates from the controller
#endif
        public zt_metric_type metrics_type; //an enum describing the metrics to collect
        public int router_keepalive;

        //posture query cbs
        public zt_pq_mac_cb pq_mac_cb;
        public zt_pq_os_cb pq_os_cb;
        public zt_pq_process_cb pq_process_cb;
        public zt_pq_domain_cb pq_domain_cb;

        public IntPtr app_ctx;

        public uint events;

        public zt_event_cb event_cb;


        public zt_metric_type MetricType {
            get {
                return (zt_metric_type)metrics_type;
            }
        }
    }



    [StructLayout(LayoutKind.Sequential)]
    public struct zt_port_range {
        public int low; //, int, none, low, __VA_ARGS__) \
        public int high; //, int, none, high, __VA_ARGS__)
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct zt_mfa_enrollment {
        public bool is_verified;
        public IntPtr recovery_codes; // convert IntPtr to string array
        public string provisioning_url;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct zt_host_cfg_v2 {
        public IntPtr terminators;//, zt_host_cfg_v1, list, terminators, __VA_ARGS__)
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct zt_host_cfg_v1 {
        public string protocol;
        public bool forward_protocol;
        public IntPtr allowed_protocols;
        public string address;
        public bool forward_address;
        public IntPtr allowed_addresses;
        public int port;
        public bool forward_port;
        public IntPtr allowed_port_ranges;//, zt_port_range, array, allowedPortRanges, __VA_ARGS__) \
        public IntPtr allowed_source_addresses;//, zt_address, array, allowedSourceAddresses, __VA_ARGS__) \
        public IntPtr listen_options;//, zt_listen_options, ptr, listenOptions, __VA_ARGS__)
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct zt_listen_options {
        public bool bind_with_identity;
        public ulong connect_timeout;
        public int connect_timeout_seconds;
        public int cost;
        public string identity;
        public int max_connections;
        public string precedence;

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct zt_server_cfg_v1 {
        public string protocol;
        public string hostname;
        public int port;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct zt_intercept_cfg_v1 {
        public IntPtr protocols;
        public IntPtr addresses;
        public IntPtr port_ranges;
        public IntPtr dial_options_map;
        public string source_ip;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct zt_client_cfg_v1 {
        public zt_address hostname;
        public int port;
    }
    public enum zt_address_type {
        Host = 0,
        CIDR = 1
    }

    [StructLayout(LayoutKind.Sequential, Size = 260)]
    public struct zt_address {
        private int address_type;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] _union;

        public zt_address_type Type {
            get {
                return (zt_address_type)address_type;
            }
        }
        public string Hostname {
            get {
                string hostname = Encoding.UTF8.GetString(_union);
                int nullCharPos = hostname.IndexOf('\0');
                return nullCharPos > -1 ? hostname.Substring(0, nullCharPos) : hostname;
            }
        }

        public AddressFamily AF {
            get {
                return (AddressFamily)BitConverter.ToInt32(new ReadOnlySpan<byte>(_union, 0, 4).ToArray(), 0);
            }
        }

        public int Bits {
            get {
                return BitConverter.ToInt32(new ReadOnlySpan<byte>(_union, 4, 4).ToArray(), 0);
            }
        }

        public IPAddress IP {
            get {
                ReadOnlySpan<byte> ipb = new ReadOnlySpan<byte>(_union, 8, 4);
                IPAddress ip = new IPAddress(ipb.ToArray());
                return ip;
            }
        }

    }
    [StructLayout(LayoutKind.Explicit, Size = (TestBlitting.ptr * 8))]
    public struct zt_service {
        [FieldOffset(0 * TestBlitting.ptr)]
        public string id;
        [FieldOffset(1 * TestBlitting.ptr)]
        public string name;
        [FieldOffset(2 * TestBlitting.ptr)]
        public IntPtr permissions;
#if ZITI_64BIT
        [FieldOffset(3 * TestBlitting.ptr)]
        public bool encryption;
        [FieldOffset((3 * TestBlitting.ptr) + 4)]
        public int perm_flags;
        [FieldOffset(4 * TestBlitting.ptr)]
        public IntPtr config;
        [FieldOffset(5 * TestBlitting.ptr)]
        public IntPtr /** posture_query_set[] **/ posture_query_set;
        [FieldOffset(6 * TestBlitting.ptr)]
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        //public byte[] _union;
        public IntPtr /** Dictionary<string, posture_query_set> **/ posture_query_map;
        [FieldOffset(7 * TestBlitting.ptr)]
        public string updated_at;
#else
        [FieldOffset(3 * TestBlitting.ptr)]
        public bool encryption;
        [FieldOffset(4 * TestBlitting.ptr)]
        public int perm_flags;
        [FieldOffset(5 * TestBlitting.ptr)]
        public IntPtr config;
        [FieldOffset(6 * TestBlitting.ptr)]
        public IntPtr /* posture_query_set[] */ posture_query_set;
        [FieldOffset(7 * TestBlitting.ptr)]
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        //public byte[] _union;
        public IntPtr /*Dictionary<string, posture_query_set> */ posture_query_map;
        [FieldOffset(8 * TestBlitting.ptr)]
        public string updated_at;
#endif
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        //public byte[] _union2;
    }
    public enum zt_session_type {
        Bind = 1,
        Dial = 2
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct zt_posture_query_set {
        [FieldOffset(0 * TestBlitting.ptr)]
        public string policy_id;
        [FieldOffset(1 * TestBlitting.ptr)]
        public bool is_passing;
        [FieldOffset(2 * TestBlitting.ptr)]
        public string policy_type;
        [FieldOffset(3 * TestBlitting.ptr)]
        public IntPtr posture_queries;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct zt_posture_query {
        [FieldOffset(0 * TestBlitting.ptr)]
        public string id;
        [FieldOffset(1 * TestBlitting.ptr)]
        public bool is_passing;
        [FieldOffset(2 * TestBlitting.ptr)]
        public string query_type;
        [FieldOffset(3 * TestBlitting.ptr)]
        public IntPtr process;
        [FieldOffset(4 * TestBlitting.ptr)]
        public IntPtr processes;
        [FieldOffset(5 * TestBlitting.ptr)]
        public int timeout;
        [FieldOffset(6 * TestBlitting.ptr)]
        public IntPtr timeoutRemaining;
        [FieldOffset(7 * TestBlitting.ptr)]
        public string updated_at;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct zt_process {
        public string path;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct zt_identity {
        public string id;
        public string name;
        public IntPtr tags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct zt_version {
        public string version;
        public string revision;
        public string build_date;
        public IntPtr api_versions;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct zt_api_versions {
        public IntPtr api_path_map;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct zt_api_path {
        public string path;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct zt_config {
        public string controller_url;
        public zt_id_cfg id;
        public string cfg_source;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct zt_id_cfg {
        public string cert;
        public string key;
        public string ca;
    }



    // ----- older stuff below





    //TODO: REMOVE
    [StructLayout(LayoutKind.Sequential)]
    public struct model_map_entry {
        public IntPtr key;
        public char key_pad1;
        public char key_pad2;
        public size_t key_len;
        public uint key_hash;
        public IntPtr value;
        public IntPtr _next;
        public IntPtr _tnext;
        public IntPtr _map;
    }
    //
    //
    //    [StructLayout(LayoutKind.Sequential)]
    //    public struct tls_context {
    //
    //    }
    //


    public struct zt_dial_opts {
        private readonly int connect_timeout_seconds;
        private readonly string identity;
        private readonly IntPtr app_data;
        private size_t app_data_sz;
    }

    public struct zt_listen_opts {
        public bool bind_with_identity;//, bool, none, bindUsingEdgeIdentity, __VA_ARGS__) \
        public ulong connect_timeout;//, duration, none, connectTimeout, __VA_ARGS__)       \
        public int connect_timeout_seconds;//, int, none, connectTimeoutSeconds, __VA_ARGS__) \
        public int cost;//, int, none, cost, __VA_ARGS__) \
        public string identity;//, string, none, identity, __VA_ARGS__) \
        public int max_connections;//, int, none, maxConnections, __VA_ARGS__)\
        public string precendence;//, string, none, precendence, __VA_ARGS__)
    }















    // -- questionable

    [StructLayout(LayoutKind.Sequential)]
    public struct zt_enroll_options {
        public string jwt;
        public string enroll_key;
        public string enroll_cert;
    };
    public struct model_map_impl {
        public IntPtr /* model_map_entry[] */ entries;
        public IntPtr table;
        public int buckets;
        public size_t size;
    }
#pragma warning restore 0169
#pragma warning restore 0649
}
