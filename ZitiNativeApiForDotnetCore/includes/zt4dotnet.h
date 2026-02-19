#ifndef ZITI4DOTNET_H
#define ZITI4DOTNET_H

#include <stdlib.h>
#include <string.h>
#include <stdint.h>

#include <zt/zt.h>
#include <zt/zt_log.h>
#include <zt/zt_events.h>
#include <uv.h>

#if _WIN32
#define Z4D_API __declspec(dllexport)
#else
#   define Z4D_API /* nothing */
# endif

#ifdef __cplusplus
extern "C" {
#endif

#define ALIGNMENT_CHECK(FIELD_TYPE)             \
typedef struct zt_alignment_check_##FIELD_TYPE##_s {       \
                    uint32_t offset;                        \
                    uint32_t size;                          \
                    const char* checksum;                   \
} zt_alignment_check_##FIELD_TYPE

#define ALIGNMENT_FIELD(FIELD_TYPE, FIELD_NAME) \
  zt_alignment_check_##FIELD_TYPE FIELD_NAME

ALIGNMENT_CHECK(zt_id_cfg);
ALIGNMENT_CHECK(zt_config);
ALIGNMENT_CHECK(api_path);
ALIGNMENT_CHECK(zt_api_versions);
ALIGNMENT_CHECK(zt_version);
ALIGNMENT_CHECK(zt_identity);
ALIGNMENT_CHECK(zt_process);
ALIGNMENT_CHECK(zt_posture_query);
ALIGNMENT_CHECK(zt_posture_query_set);
ALIGNMENT_CHECK(zt_session_type);
ALIGNMENT_CHECK(zt_service);
ALIGNMENT_CHECK(zt_address);
ALIGNMENT_CHECK(zt_client_cfg_v1);
ALIGNMENT_CHECK(zt_intercept_cfg_v1);
ALIGNMENT_CHECK(zt_server_cfg_v1);
ALIGNMENT_CHECK(zt_listen_options);
ALIGNMENT_CHECK(zt_host_cfg_v1);
ALIGNMENT_CHECK(zt_host_cfg_v2);
ALIGNMENT_CHECK(zt_mfa_enrollment);
ALIGNMENT_CHECK(zt_port_range);
ALIGNMENT_CHECK(zt_options);
ALIGNMENT_CHECK(zt_event_t);

#define ALIGNMENT_DATA(FIELD_TYPE, FIELD_NAME) \
  FIELD_TYPE FIELD_NAME##_data

typedef struct zt_types_v2_s {
    uint32_t size; //declare as a pointer but use as a value
    // declare all the alignments: offset, size, checksum
    ALIGNMENT_FIELD(zt_id_cfg, zt_id_cfg);
    ALIGNMENT_FIELD(zt_config, zt_config);
    ALIGNMENT_FIELD(api_path, api_path);
    ALIGNMENT_FIELD(zt_api_versions, zt_api_versions);
    ALIGNMENT_FIELD(zt_version, zt_version);
    ALIGNMENT_FIELD(zt_identity, zt_identity);
    ALIGNMENT_FIELD(zt_process, zt_process);
    ALIGNMENT_FIELD(zt_posture_query, zt_posture_query);
    ALIGNMENT_FIELD(zt_posture_query_set, zt_posture_query_set);
    ALIGNMENT_FIELD(zt_session_type, zt_session_type);
    ALIGNMENT_FIELD(zt_service, zt_service);
    ALIGNMENT_FIELD(zt_address, zt_address_host);
    ALIGNMENT_FIELD(zt_address, zt_address_cidr);
    ALIGNMENT_FIELD(zt_client_cfg_v1, zt_client_cfg_v1);
    ALIGNMENT_FIELD(zt_intercept_cfg_v1, zt_intercept_cfg_v1);
    ALIGNMENT_FIELD(zt_server_cfg_v1, zt_server_cfg_v1);
    ALIGNMENT_FIELD(zt_listen_options, zt_listen_options);
    ALIGNMENT_FIELD(zt_host_cfg_v1, zt_host_cfg_v1);
    ALIGNMENT_FIELD(zt_host_cfg_v2, zt_host_cfg_v2);
    ALIGNMENT_FIELD(zt_mfa_enrollment, zt_mfa_enrollment);
    ALIGNMENT_FIELD(zt_port_range, zt_port_range);
    ALIGNMENT_FIELD(zt_options, zt_options);
    ALIGNMENT_FIELD(zt_event_t, zt_context_event);
    ALIGNMENT_FIELD(zt_event_t, zt_router_event);
    ALIGNMENT_FIELD(zt_event_t, zt_service_event);
    ALIGNMENT_FIELD(zt_event_t, zt_mfa_auth_event);
    ALIGNMENT_FIELD(zt_event_t, zt_api_event);

    // now declare "_data" elemnets - the __ACTUAL__ structs
    ALIGNMENT_DATA(zt_id_cfg, zt_id_cfg);
    ALIGNMENT_DATA(zt_config, zt_config);
    ALIGNMENT_DATA(api_path, api_path);
    ALIGNMENT_DATA(zt_api_versions, zt_api_versions);
    ALIGNMENT_DATA(zt_version, zt_version);
    ALIGNMENT_DATA(zt_identity, zt_identity);
    ALIGNMENT_DATA(zt_process, zt_process);
    ALIGNMENT_DATA(zt_posture_query, zt_posture_query);
    ALIGNMENT_DATA(zt_posture_query_set, zt_posture_query_set);
    ALIGNMENT_DATA(zt_session_type, zt_session_type);
    ALIGNMENT_DATA(zt_service, zt_service);
    ALIGNMENT_DATA(zt_address, zt_address_host);
    ALIGNMENT_DATA(zt_address, zt_address_cidr);
    ALIGNMENT_DATA(zt_client_cfg_v1, zt_client_cfg_v1);
    ALIGNMENT_DATA(zt_intercept_cfg_v1, zt_intercept_cfg_v1);
    ALIGNMENT_DATA(zt_server_cfg_v1, zt_server_cfg_v1);
    ALIGNMENT_DATA(zt_listen_options, zt_listen_options);
    ALIGNMENT_DATA(zt_host_cfg_v1, zt_host_cfg_v1);
    ALIGNMENT_DATA(zt_host_cfg_v2, zt_host_cfg_v2);
    ALIGNMENT_DATA(zt_mfa_enrollment, zt_mfa_enrollment);
    ALIGNMENT_DATA(zt_port_range, zt_port_range);
    ALIGNMENT_DATA(zt_options, zt_options);
    ALIGNMENT_DATA(struct zt_context_event, zt_context_event);
    ALIGNMENT_DATA(struct zt_router_event, zt_router_event);
    ALIGNMENT_DATA(struct zt_service_event, zt_service_event);
    ALIGNMENT_DATA(struct zt_auth_event, zt_auth_event);
    ALIGNMENT_DATA(struct zt_config_event, zt_config_event);
} zt_types_v2;

Z4D_API int z4d_zt_close(zt_connection con);
Z4D_API int z4d_uv_run(void* loop);
Z4D_API void* z4d_new_loop();
Z4D_API const char** z4d_all_config_types();
Z4D_API uv_loop_t* z4d_default_loop();
Z4D_API void* z4d_registerUVTimer(uv_loop_t* loop, uv_timer_cb timer_cb, uint64_t iterations, uint64_t delay);
Z4D_API int z4d_stop_uv_timer(uv_timer_t* t);
Z4D_API int z4d_event_type_from_pointer(const zt_event_t *event);
Z4D_API zt_service* z4d_service_array_get(zt_service_array arr, int idx);

Z4D_API char** z4d_make_char_array(int size);
Z4D_API void z4d_set_char_at(char **a, char *s, int n);
Z4D_API void z4d_free_char_array(char **a, int size);

Z4D_API void z4d_zt_dump_log(zt_context ztx);
Z4D_API void z4d_zt_dump_file(zt_context ztx, const char* outputFile);

Z4D_API zt_types_v2* z4d_struct_test();
Z4D_API zt_posture_query* z4d_zt_posture_query();

typedef void (*z4d_cb)(void* context);
Z4D_API void z4d_callback_on_loop(uv_loop_t* loop, void* context, z4d_cb cb);

#ifdef __cplusplus
}
#endif

#endif /* ZITI4DOTNET_H */
