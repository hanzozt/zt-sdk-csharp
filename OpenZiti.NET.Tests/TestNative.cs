using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hanzo ZT.Native;
using System.Drawing;

namespace Hanzo ZT.NET.Tests {
    [TestClass]
    public class NativeCodeAlignmentChecker {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
        private void verifyFieldCheck<T>(AlignmentCheck check, string expectedChecksum, uint expectedOffset, uint expectedSize) {
            Assert.AreEqual(expectedChecksum, check.checksum);
            Assert.AreEqual((int)expectedOffset, (int)check.offset);
            Assert.AreEqual(expectedSize, check.size);
        }

        [TestMethod]
        public void TestCSDKStructAlignments() {
            Log.Info("test begins with: " + Native.API.GetZitiPath());
            IntPtr testData = TestBlitting.z4d_struct_test();
            uint size = Marshal.PtrToStructure<uint>(testData);
            byte[] managedArray = new byte[size];
            Marshal.Copy(testData, managedArray, 0, (int)size);
            zt_types native_structs = Marshal.PtrToStructure<zt_types>(testData);
            
            // as of 0.35.0, run ./build/library/Debug/ztstructalignment.exe and capture the output
            // use that output to verify the expected offsets below.
            // __IF__ an offset is failing, there's an alignment issue in that struct that must be figured out!
            // often this is a field in the struct that has been removed from the native code that you'll need to
            // remove from the managed struct. Sometimes it'll be due to field alignment issues that are more difficult
            // to debug
            Log.Info("----");
#if ZITI_64BIT
            Assert.AreEqual((int)2224, (int)native_structs.size);
            verifyFieldCheck<zt_id_cfg>(native_structs.f02_zt_id_cfg, "zt_id_cfg",  24, 24);
            verifyFieldCheck<zt_config>(native_structs.f03_zt_config, "zt_config",  40, 40);
            verifyFieldCheck<zt_api_path>(native_structs.f04_api_path, "api_path",  56, 8);
            verifyFieldCheck<zt_api_versions>(native_structs.f05_zt_api_versions, "zt_api_versions", 72,  8);
            verifyFieldCheck<zt_version>(native_structs.f06_zt_version, "zt_version", 88,  32);
            verifyFieldCheck<zt_identity>(native_structs.f07_zt_identity, "zt_identity", 104,  24);
            verifyFieldCheck<zt_process>(native_structs.f08_zt_process, "zt_process", 120,  8);
            verifyFieldCheck<zt_posture_query>(native_structs.f10_zt_posture_query_set, "zt_posture_query_set", 152,  32);
            verifyFieldCheck<zt_posture_query_set>(native_structs.f11_zt_session_type, "zt_session_type", 168,  4);
            verifyFieldCheck<zt_session_type>(native_structs.f12_zt_service, "zt_service", 184,  64);
            verifyFieldCheck<zt_service>(native_structs.f13_zt_address_host, "zt_address_host", 200,  260);
            verifyFieldCheck<zt_address>(native_structs.f14_zt_address_cidr, "zt_address_cidr", 216,  4);
            verifyFieldCheck<zt_client_cfg_v1>(native_structs.f15_zt_client_cfg_v1, "zt_client_cfg_v1", 232,  264);
            verifyFieldCheck<zt_intercept_cfg_v1>(native_structs.f16_zt_intercept_cfg_v1, "zt_intercept_cfg_v1", 248,  40);
            verifyFieldCheck<zt_server_cfg_v1>(native_structs.f17_zt_server_cfg_v1, "zt_server_cfg_v1", 264,  24);
            verifyFieldCheck<zt_listen_options>(native_structs.f18_zt_listen_options, "zt_listen_options", 280,  48);
            verifyFieldCheck<zt_host_cfg_v1>(native_structs.f19_zt_host_cfg_v1, "zt_host_cfg_v1", 296,  80);
            verifyFieldCheck<zt_host_cfg_v2>(native_structs.f20_zt_host_cfg_v2, "zt_host_cfg_v2", 312,  8);
            verifyFieldCheck<zt_mfa_enrollment>(native_structs.f21_zt_mfa_enrollment, "zt_mfa_enrollment", 328,  24);
            verifyFieldCheck<zt_port_range>(native_structs.f22_zt_port_range, "zt_port_range", 344,  8);
            verifyFieldCheck<zt_options>(native_structs.f23_zt_options, "zt_options", 360,  96);
            verifyFieldCheck<zt_context_event>(native_structs.f24_zt_context_event, "zt_context_event", 376,  40);
            verifyFieldCheck<zt_router_event>(native_structs.f25_zt_router_event, "zt_router_event", 392,  40);
            verifyFieldCheck<zt_service_event>(native_structs.f26_zt_service_event, "zt_service_event", 408,  40);
            verifyFieldCheck<zt_mfa_auth_event>(native_structs.f27_zt_mfa_auth_event, "zt_mfa_auth_event", 424,  40);
            verifyFieldCheck<zt_api_event>(native_structs.f28_zt_api_event, "zt_api_event", 440,  40);
#else
            Assert.AreEqual((int)1608, (int)native_structs.size);
            verifyFieldCheck<zt_id_cfg>(native_structs.f02_zt_id_cfg, "zt_id_cfg", 16, 12);
            verifyFieldCheck<zt_config>(native_structs.f03_zt_config, "zt_config", 28, 20);
            verifyFieldCheck<zt_api_path>(native_structs.f04_api_path, "api_path", 40, 4);
            verifyFieldCheck<zt_api_versions>(native_structs.f05_zt_api_versions, "zt_api_versions", 52, 4);
            verifyFieldCheck<zt_version>(native_structs.f06_zt_version, "zt_version", 64, 16);
            verifyFieldCheck<zt_identity>(native_structs.f07_zt_identity, "zt_identity", 76, 12);
            verifyFieldCheck<zt_process>(native_structs.f08_zt_process, "zt_process", 88, 4);
            verifyFieldCheck<zt_posture_query>(native_structs.f10_zt_posture_query_set, "zt_posture_query_set", 112, 16);
            verifyFieldCheck<zt_posture_query_set>(native_structs.f11_zt_session_type, "zt_session_type", 124, 4);
            verifyFieldCheck<zt_session_type>(native_structs.f12_zt_service, "zt_service", 136, 36);
            verifyFieldCheck<zt_service>(native_structs.f13_zt_address_host, "zt_address_host", 148, 260);
            verifyFieldCheck<zt_address>(native_structs.f14_zt_address_cidr, "zt_address_cidr", 160, 4);
            verifyFieldCheck<zt_client_cfg_v1>(native_structs.f15_zt_client_cfg_v1, "zt_client_cfg_v1", 172, 264);
            verifyFieldCheck<zt_intercept_cfg_v1>(native_structs.f16_zt_intercept_cfg_v1, "zt_intercept_cfg_v1", 184, 20);
            verifyFieldCheck<zt_server_cfg_v1>(native_structs.f17_zt_server_cfg_v1, "zt_server_cfg_v1", 196, 12);
            verifyFieldCheck<zt_listen_options>(native_structs.f18_zt_listen_options, "zt_listen_options", 208, 40);
            verifyFieldCheck<zt_host_cfg_v1>(native_structs.f19_zt_host_cfg_v1, "zt_host_cfg_v1", 220, 44);
            verifyFieldCheck<zt_host_cfg_v2>(native_structs.f20_zt_host_cfg_v2, "zt_host_cfg_v2", 232, 4);
            verifyFieldCheck<zt_mfa_enrollment>(native_structs.f21_zt_mfa_enrollment, "zt_mfa_enrollment", 244, 12);
            verifyFieldCheck<zt_port_range>(native_structs.f22_zt_port_range, "zt_port_range", 256, 8);
            verifyFieldCheck<zt_options>(native_structs.f23_zt_options, "zt_options", 268, 56);
            verifyFieldCheck<zt_context_event>(native_structs.f24_zt_context_event, "zt_context_event", 280, 20);
            verifyFieldCheck<zt_router_event>(native_structs.f25_zt_router_event, "zt_router_event", 292, 20);
            verifyFieldCheck<zt_service_event>(native_structs.f26_zt_service_event, "zt_service_event", 304, 20);
            verifyFieldCheck<zt_mfa_auth_event>(native_structs.f27_zt_mfa_auth_event, "zt_mfa_auth_event", 316, 20);
            verifyFieldCheck<zt_api_event>(native_structs.f28_zt_api_event, "zt_api_event", 328, 20);
#endif
            zt_types_with_values values = Marshal.PtrToStructure<zt_types_with_values>(testData);
            
            Assert.AreEqual("cert", values.zt_id_cfg.cert);
            Assert.AreEqual("key", values.zt_id_cfg.key);
            Assert.AreEqual("ca", values.zt_id_cfg.ca);
            
            Assert.AreEqual("controller_url", values.zt_config.controller_url);
            Assert.AreEqual("cert", values.zt_config.id.cert);
            Assert.AreEqual("key", values.zt_config.id.key);
            Assert.AreEqual("ca", values.zt_config.id.ca);
            
            Assert.AreEqual("path", values.zt_api_path.path);
            
            Assert.AreNotEqual(IntPtr.Zero, values.zt_api_versions.api_path_map);
            
            Assert.AreEqual("version", values.zt_version.version);
            Assert.AreEqual("revision", values.zt_version.revision);
            Assert.AreEqual("build_date", values.zt_version.build_date);
            Assert.AreNotEqual(IntPtr.Zero, values.zt_version.api_versions);
            
            Assert.AreEqual("id", values.zt_identity.id);
            Assert.AreEqual("name", values.zt_identity.name);
            Assert.AreNotEqual(IntPtr.Zero, values.zt_identity.tags);
            
            Assert.AreEqual("path", values.zt_process.path);
            
            Assert.AreEqual("id", values.zt_posture_query.id);
            Assert.IsTrue(values.zt_posture_query.is_passing);
            Assert.AreEqual("query_type", values.zt_posture_query.query_type);
            Assert.AreNotEqual(IntPtr.Zero, values.zt_posture_query.process);
            Assert.AreNotEqual(IntPtr.Zero, values.zt_posture_query.processes);
            Assert.AreEqual(10, values.zt_posture_query.timeout);
            Assert.AreNotEqual(IntPtr.Zero, values.zt_posture_query.timeoutRemaining);
            int tor = Marshal.ReadInt32(values.zt_posture_query.timeoutRemaining);
            Assert.AreEqual(20, tor);
            Assert.AreEqual("updated_at", values.zt_posture_query.updated_at);
            
            Assert.AreEqual("policy_id", values.zt_posture_query_set.policy_id);
            Assert.IsTrue(values.zt_posture_query_set.is_passing);
            Assert.AreEqual("policy_type", values.zt_posture_query_set.policy_type);
            Assert.AreNotEqual(IntPtr.Zero, values.zt_posture_query_set.posture_queries);
            
            Assert.AreEqual(zt_session_type.Dial, values.zt_session_type);
            
            Assert.AreEqual("elem1id", values.zt_service.id);
            Assert.AreEqual("elem1", values.zt_service.name);
            Assert.AreNotEqual(IntPtr.Zero, values.zt_service.permissions);
            Assert.IsTrue(values.zt_service.encryption);
            Assert.AreEqual(111, values.zt_service.perm_flags);
            Assert.AreNotEqual(IntPtr.Zero, values.zt_service.config);
            Assert.AreNotEqual(IntPtr.Zero, values.zt_service.posture_query_set);
            Assert.AreNotEqual(IntPtr.Zero, values.zt_service.posture_query_map);
            Assert.AreEqual("updated_at", values.zt_service.updated_at);
            
            Assert.AreEqual(zt_address_type.Host, values.zt_address_host.Type);
            Assert.AreEqual("hostname", values.zt_address_host.Hostname);
            
            Assert.AreEqual(zt_address_type.CIDR, values.zt_address_cidr.Type);
            Assert.AreEqual(AddressFamily.InterNetwork, values.zt_address_cidr.AF);
            Assert.AreEqual(8, values.zt_address_cidr.Bits);
            Assert.AreEqual("100.200.50.25", values.zt_address_cidr.IP.ToString());
            
            Assert.AreEqual(zt_address_type.Host, values.zt_client_cfg_v1.hostname.Type);
            Assert.AreEqual("hostname", values.zt_client_cfg_v1.hostname.Hostname);
            Assert.AreEqual(80, values.zt_client_cfg_v1.port);
            
            Assert.AreNotEqual(IntPtr.Zero, values.zt_intercept_cfg_v1.protocols);
            Assert.AreNotEqual(IntPtr.Zero, values.zt_intercept_cfg_v1.addresses);
            Assert.AreNotEqual(IntPtr.Zero, values.zt_intercept_cfg_v1.port_ranges);
            Assert.AreNotEqual(IntPtr.Zero, values.zt_intercept_cfg_v1.dial_options_map);
            Assert.AreEqual("source_ip", values.zt_intercept_cfg_v1.source_ip);
            
            Assert.AreEqual("protocol", values.zt_server_cfg_v1.protocol);
            Assert.AreEqual("hostname", values.zt_server_cfg_v1.hostname);
            Assert.AreEqual(443, values.zt_server_cfg_v1.port);
            
            Assert.IsTrue(values.zt_listen_options.bind_with_identity);
            Assert.AreEqual((ulong)1000000, values.zt_listen_options.connect_timeout);
            Assert.AreEqual(100, values.zt_listen_options.connect_timeout_seconds);
            Assert.AreEqual(9, values.zt_listen_options.cost);
            Assert.AreEqual("identity", values.zt_listen_options.identity);
            Assert.AreEqual(10, values.zt_listen_options.max_connections);
            Assert.AreEqual("precedence", values.zt_listen_options.precedence);
            
            Assert.AreEqual("protocol", values.zt_host_cfg_v1.protocol);
            Assert.IsTrue(values.zt_host_cfg_v1.forward_protocol);
            Assert.AreNotEqual(IntPtr.Zero, values.zt_host_cfg_v1.allowed_protocols);
            Assert.AreEqual("address", values.zt_host_cfg_v1.address);
            Assert.IsTrue(values.zt_host_cfg_v1.forward_address);
            Assert.AreNotEqual(IntPtr.Zero, values.zt_host_cfg_v1.allowed_addresses);
            Assert.AreEqual(1090, values.zt_host_cfg_v1.port);
            Assert.IsTrue(values.zt_host_cfg_v1.forward_port);
            Assert.AreNotEqual(IntPtr.Zero, values.zt_host_cfg_v1.allowed_port_ranges);
            Assert.AreNotEqual(IntPtr.Zero, values.zt_host_cfg_v1.allowed_source_addresses);
            Assert.AreNotEqual(IntPtr.Zero, values.zt_host_cfg_v1.listen_options);
            
            Assert.AreNotEqual(IntPtr.Zero, values.zt_host_cfg_v2.terminators);
            
            Assert.IsTrue(values.zt_mfa_enrollment.is_verified);
            Assert.AreNotEqual(IntPtr.Zero, values.zt_mfa_enrollment.recovery_codes);
            Assert.AreEqual("provisioningUrl", values.zt_mfa_enrollment.provisioning_url);
            
            Assert.AreEqual(80, values.zt_port_range.low);
            Assert.AreEqual(443, values.zt_port_range.high);
            
            Assert.AreEqual("config", values.zt_options.config);
            Assert.IsTrue(values.zt_options.disabled);
            Assert.AreNotEqual(IntPtr.Zero, values.zt_options.config_types);
            Assert.AreEqual((uint)232323, values.zt_options.api_page_size);
            Assert.AreEqual((int)3322, (int)values.zt_options.refresh_interval);
            Assert.AreEqual(zt_metric_type.EWMA_15m, values.zt_options.metrics_type);
            Assert.AreEqual(111, values.zt_options.router_keepalive);
            Assert.AreEqual("ctxhere", Marshal.PtrToStringUTF8(values.zt_options.app_ctx));
            Assert.AreEqual((uint)98, values.zt_options.events);

            Assert.AreEqual(zt_event_type.ZitiContextEvent, values.zt_context_event.zt_event_type);
            Assert.AreEqual(245, values.zt_context_event.ctrl_status);
            Assert.AreEqual("zt_context_event_err_0__", values.zt_context_event.err);
            
            zt_router_event rev = values.zt_router_event;
            Assert.AreEqual(zt_event_type.ZitiRouterEvent, values.zt_router_event.zt_event_type);
            Assert.AreEqual(zt_router_status.EdgeRouterConnected, rev.status);
            Assert.AreEqual("ere_name", rev.name);
            Assert.AreEqual("ere_address", rev.address);
            Assert.AreEqual("ere_version", rev.version);
            
            zt_service_event svcev = values.zt_service_event;
            Assert.AreEqual(zt_event_type.ZitiServiceEvent, values.zt_service_event.zt_event_type);
            Assert.AreNotEqual(IntPtr.Zero, svcev.removed);
            Assert.AreNotEqual(IntPtr.Zero, svcev.changed);
            Assert.AreNotEqual(IntPtr.Zero, svcev.added);
            Assert.AreEqual(svcev.added, svcev.removed);
            Assert.AreEqual(svcev.changed, svcev.removed);
            Assert.AreEqual(svcev.changed, svcev.added);
            IntPtr ptr1 = Native.API.z4d_service_array_get(svcev.removed, 0);
            IntPtr ptr2 = Native.API.z4d_service_array_get(svcev.removed, 1);
            zt_service removed1 = Marshal.PtrToStructure<zt_service>(ptr1);
            zt_service removed2 = Marshal.PtrToStructure<zt_service>(ptr2);
            
            Assert.AreEqual("elem1", removed1.name);
            Assert.AreEqual("elem2", removed2.name);
            Assert.AreEqual("elem1id", removed1.id);
            Assert.AreEqual("elem2id", removed2.id);
            Assert.AreEqual(111, removed1.perm_flags);
            Assert.AreEqual(222, removed2.perm_flags);
                        
            zt_api_event apie = values.zt_api_event;
            Assert.AreEqual(zt_event_type.ZitiAPIEvent, values.zt_api_event.zt_event_type);
            Assert.AreEqual("new_ctrl_address", apie.new_ctrl_address);
            Assert.AreEqual("new_ca_bundle", apie.new_ca_bundle);

            Log.Info("test complete");
        }
    }
}
