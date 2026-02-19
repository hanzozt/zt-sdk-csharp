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
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: InternalsVisibleTo("UnitTests")]
namespace Hanzo ZT.Native {

    //typedef void (*zt_service_cb)(zt_context ztx, zt_service*, int status, void* data);
    [UnmanagedFunctionPointer(API.CALL_CONVENTION)] public delegate void zt_service_cb(IntPtr zt_context, IntPtr zt_service, int status, GCHandle on_service_context);
    // typedef void (* zt_conn_cb) (zt_connection conn, int status);
    [UnmanagedFunctionPointer(API.CALL_CONVENTION)] public delegate void zt_conn_cb(IntPtr zt_connection, int status);
    // typedef void (* zt_conn_cb) (zt_connection conn, int status);
    [UnmanagedFunctionPointer(API.CALL_CONVENTION)] public delegate void zt_listen_cb(IntPtr zt_connection, int status);
    // typedef void (* zt_client_cb) (zt_connection serv, zt_connection client, int status);
    [UnmanagedFunctionPointer(API.CALL_CONVENTION)] public delegate void zt_client_cb(IntPtr zt_connection_server, IntPtr zt_connection_client, int status);
    // typedef void (* zt_write_cb) (zt_connection conn, ssize_t status, void* write_ctx);
    [UnmanagedFunctionPointer(API.CALL_CONVENTION)] public delegate void zt_write_cb(IntPtr zt_connection, int status, GCHandle write_context);
    // typedef void (* zt_enroll_cb) (zt_config* cfg, int status, char* err_message, void* enroll_ctx);
    [UnmanagedFunctionPointer(API.CALL_CONVENTION)] public delegate void zt_enroll_cb(IntPtr zt_config, int status, string errorMessage, GCHandle enroll_context);
    // typedef ssize_t(*zt_data_cb)(zt_connection conn, uint8_t* data, ssize_t length);
    [UnmanagedFunctionPointer(API.CALL_CONVENTION)] public delegate int zt_data_cb(IntPtr conn, IntPtr data, int length);
    //typedef void (*zt_pr_mac_cb)(zt_context ztx, char *id, char **mac_addresses, int num_mac);
    [UnmanagedFunctionPointer(API.CALL_CONVENTION)] public delegate void zt_pr_mac_cb(IntPtr zt_context, string id, string[] mac_addresses, int num_mac);
    //typedef void (* zt_pq_mac_cb) (zt_context ztx, char* id, zt_pr_mac_cb response_cb);
    [UnmanagedFunctionPointer(API.CALL_CONVENTION)] public delegate void zt_pq_mac_cb(IntPtr zt_context, string id, zt_pr_mac_cb response_cb);
    //typedef void (*zt_pr_os_cb)(zt_context ztx, char *id, char *os_type, char *os_version, char *os_build);
    [UnmanagedFunctionPointer(API.CALL_CONVENTION)] public delegate void zt_pr_os_cb(IntPtr zt_context, string id, string os_type, string os_version, string os_build);
    //typedef void (*zt_pq_os_cb)(zt_context ztx, char *id, zt_pr_os_cb response_cb);
    [UnmanagedFunctionPointer(API.CALL_CONVENTION)] public delegate void zt_pq_os_cb(IntPtr zt_context, string id, zt_pr_os_cb response_cb);
    //typedef void (* zt_pr_process_cb) (zt_context ztx, char* id, char* path, bool is_running, char* sha_512_hash, char** signers, int num_signers);
    [UnmanagedFunctionPointer(API.CALL_CONVENTION)] public delegate void zt_pr_process_cb(IntPtr zt_context, string id, string path, bool is_running, string sha_512, string[] signers, int num_signers);
    //typedef void (* zt_pq_process_cb) (zt_context ztx, const char* id, const char* path, zt_pr_process_cb response_cb);
    [UnmanagedFunctionPointer(API.CALL_CONVENTION)] public delegate void zt_pq_process_cb(IntPtr zt_context, string id, string path, zt_pr_process_cb response_cb);
    //typedef void (*zt_pr_domain_cb)(zt_context ztx, char *id, char *domain);
    [UnmanagedFunctionPointer(API.CALL_CONVENTION)] public delegate void zt_pr_domain_cb(IntPtr zt_context, string id, string domain);
    //typedef void (*zt_pq_domain_cb)(zt_context ztx, char *id, zt_pr_domain_cb response_cb);
    [UnmanagedFunctionPointer(API.CALL_CONVENTION)] public delegate void zt_pq_domain_cb(IntPtr zt_context, string id, zt_pr_domain_cb response_cb);
    // typedef void (*zt_mfa_cb)(zt_context ztx, int status, void *ctx);
    [UnmanagedFunctionPointer(API.CALL_CONVENTION)] public delegate void on_mfa_cb(IntPtr zt_context, int status, IntPtr ctx);
    // typedef void (*zt_mfa_enroll_cb)(zt_context ztx, int status, zt_mfa_enrollment *mfa_enrollment, void *ctx);
    [UnmanagedFunctionPointer(API.CALL_CONVENTION)] public delegate void on_enable_mfa(IntPtr zt_context, int status, IntPtr /* zt_mfa_enrollment*/ enrollment, IntPtr ctx);
    // typedef void (*zt_mfa_recovery_codes_cb)(zt_context ztx, int status, char **recovery_codes, void *ctx);
    [UnmanagedFunctionPointer(API.CALL_CONVENTION)] public delegate void on_mfa_recovery_codes(IntPtr zt_context, int status, IntPtr /* string[] */ recovery_codes, IntPtr ctx);
    //typedef void (*zt_event_cb)(zt_context ztx, const zt_event_t *event);
    [UnmanagedFunctionPointer(API.CALL_CONVENTION)] public delegate void zt_event_cb(IntPtr zt_context, IntPtr zt_event);
    //typedef void (*zt_close_cb)(zt_connection conn);
    [UnmanagedFunctionPointer(API.CALL_CONVENTION)] public delegate void zt_close_cb(IntPtr conn);
    //typedef void (*uv_close_cb)(uv_handle_t* handle);
    [UnmanagedFunctionPointer(API.CALL_CONVENTION)] public delegate void OnUVTimer(IntPtr handle);
    //typedef void (* uv_close_cb) (uv_handle_t* handle);
    [UnmanagedFunctionPointer(API.CALL_CONVENTION)] public delegate void uv_close_cb(IntPtr handle);

    internal class MarshalUtils<T> {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        internal static List<T> convertPointerToList(IntPtr arrayPointer) {
            IntPtr currentArrLoc;
            var result = new List<T>();
            var sizeOfPointer = Marshal.SizeOf(typeof(IntPtr));

            while ((currentArrLoc = Marshal.ReadIntPtr(arrayPointer)) != IntPtr.Zero) {
                T objectT;
                if (typeof(T) == typeof(string)) {
                    objectT = (T)(object)Marshal.PtrToStringUTF8(currentArrLoc);
                } else if (typeof(T).IsValueType && !typeof(T).IsPrimitive) {
                    objectT = Marshal.PtrToStructure<T>(currentArrLoc);
                } else {
                    // marshal operations for other types can be added here
                    throw new Exception("Marshalling is not yet supported for " + typeof(T));
                }
#pragma warning disable CS8604 // Possible null reference argument.
                result.Add(objectT);
#pragma warning restore CS8604 // Possible null reference argument.
                arrayPointer = IntPtr.Add(arrayPointer, sizeOfPointer);
            }
            return result;
        }

        internal static List<model_map_entry> convertPointerMapToList(IntPtr arrayPointer) {
            IntPtr currentArrLoc;
            var result = new List<model_map_entry>();
            var sizeOfPointer = Marshal.SizeOf(typeof(IntPtr));

            while ((currentArrLoc = arrayPointer) != IntPtr.Zero) {
                var objectT = Marshal.PtrToStructure<model_map_entry>(currentArrLoc);
                result.Add(objectT);
                arrayPointer = objectT._next;
            }
            return result;
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
    }
}
