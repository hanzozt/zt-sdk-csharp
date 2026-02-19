#include "zt/zt.h"

#define OFFSET(TYPE, MEMBER) printf("\noffset of " #TYPE "." #MEMBER ": %zu", offsetof(TYPE, MEMBER));

int main() {
    printf("\noffset output begins\n");
    zt_auth_query_mfa z = {0};
    z.type_id = "type";
    OFFSET(zt_auth_query_mfa, type_id);
    z.provider = "provider";
    OFFSET(zt_auth_query_mfa, provider);
    z.http_method = "http_method";
    OFFSET(zt_auth_query_mfa, http_method);
    z.http_url = "http_url";
    OFFSET(zt_auth_query_mfa, http_url);
    z.min_length = 1;
    OFFSET(zt_auth_query_mfa, min_length);
    z.max_length = 2;
    OFFSET(zt_auth_query_mfa, max_length);
    z.format = "format";
    OFFSET(zt_auth_query_mfa, format);

    zt_id_cfg idcfg = {0};
    idcfg.cert = "cert";
    OFFSET(zt_id_cfg, cert);
    idcfg.key = "key";
    OFFSET(zt_id_cfg, key);
    idcfg.ca = "ca";
    OFFSET(zt_id_cfg, ca);

    zt_config config = {0};
    config.controller_url = "controller_url";
    OFFSET(zt_config, controller_url);
    config.id = idcfg;
    OFFSET(zt_config, id);

    api_path apip = {0};
    apip.path = "path";
    OFFSET(api_path, path);

    zt_api_versions zav = {0};
    model_map edgemap = {0};
    zav.edge = edgemap;
    OFFSET(zt_api_versions, edge);

    zt_version v = {0};
    v.version = "version";
    OFFSET(zt_version, version);
    v.revision = "revision";
    OFFSET(zt_version, revision);
    v.build_date = "build_date";
    OFFSET(zt_version, build_date);
    v.api_versions = &zav;
    OFFSET(zt_version, api_versions);

    zt_identity zi = {0};
    zi.id = "id";
    OFFSET(zt_identity, id);
    zi.name = "name";
    OFFSET(zt_identity, name);
    model_map json_model_map = {0};
    zi.app_data = json_model_map;
    OFFSET(zt_identity, app_data);

    zt_process zproc = {0};
    zproc.path = "path";
    OFFSET(zt_process, path);

    zt_posture_query zpq = {0};
    zpq.id = "id";
    OFFSET(zt_posture_query, id);
    zpq.is_passing = true;
    OFFSET(zt_posture_query, is_passing);
    zpq.query_type = "query_type";
    OFFSET(zt_posture_query, query_type);
    zpq.process = &zproc;
    OFFSET(zt_posture_query, process);
    zt_process_array zpa = {0};
    zpq.processes = zpa;
    OFFSET(zt_posture_query, processes);
    zpq.timeout = 10;
    OFFSET(zt_posture_query, timeout);
    int timeremain = 20;
    zpq.timeoutRemaining = &timeremain;
    OFFSET(zt_posture_query, timeoutRemaining);
    zpq.updated_at = "updated_at";
    OFFSET(zt_posture_query, updated_at);

    zt_posture_query_set zpqs = {0};
    zpqs.policy_id = "policy_id";
    OFFSET(zt_posture_query_set, policy_id);
    zpqs.is_passing = true;
    OFFSET(zt_posture_query_set, is_passing);
    zpqs.policy_type = "policy_type";
    OFFSET(zt_posture_query_set, policy_type);
    zt_posture_query_array zpqa = {0};
    zpqs.posture_queries = zpqa;
    OFFSET(zt_posture_query_set, posture_queries);

    zt_posture_query_set_array zpqsa = calloc(sizeof(zt_posture_query_set), 2);
    zpqsa[0] = &zpqs;
    zt_session_type_array zsta = calloc(sizeof(zt_session_type), 2);
    zt_session_type st = {0};

    zt_service zs = {0};
    zs.id = "id";
    OFFSET(zt_service, id);
    zs.name = "name";
    OFFSET(zt_service, name);
    st = zt_session_type_Bind;
    zsta[0] = &st;
    zs.permissions = zsta;
    OFFSET(zt_service, permissions);
    zs.encryption = "encryption";
    OFFSET(zt_service, encryption);
    zs.perm_flags = 255;
    OFFSET(zt_service, perm_flags);
    zs.config = json_model_map;
    OFFSET(zt_service, config);
    zs.posture_query_set = zpqsa;
    OFFSET(zt_service, posture_query_set);
    model_map zpqm = {0};
    zs.posture_query_map = zpqm;
    OFFSET(zt_service, posture_query_map);
    zs.updated_at = "updated_at";
    OFFSET(zt_service, updated_at);

    zt_address zahost = {0};
    zahost.type = zt_address_hostname;
    OFFSET(zt_address, type);
    strncpy(zahost.addr.hostname,"hostname",9);
    OFFSET(zt_address, addr.hostname);
    zt_address zacidr = {0};
    zacidr.type = zt_address_cidr;
    OFFSET(zt_address, type);
    zacidr.addr.cidr.af = AF_INET;
    OFFSET(zt_address, addr.cidr.af);
    zacidr.addr.cidr.bits = 8;
    OFFSET(zt_address, addr.cidr.bits);

    //in6_addr v6addr = {0};
    //zacidr.addr.cidr.ip = "ip";

    zt_client_cfg_v1 v1 = {0};
    v1.hostname = zahost;
    OFFSET(zt_client_cfg_v1, hostname);
    v1.port = 80;
    OFFSET(zt_client_cfg_v1, port);

    zt_port_range zpr = {0};
    zpr.low = 80;
    OFFSET(zt_port_range, low);
    zpr.high = 443;
    OFFSET(zt_port_range, high);

    zt_port_range pr = {0};
    pr.low = 80;
    OFFSET(zt_port_range, low);
    pr.high = 442;
    OFFSET(zt_port_range, high);

    zt_intercept_cfg_v1 zicv1 = {0};
    model_list protos = {0};
    zt_protocol zp = zt_protocol_tcp;
    model_list_append(&protos, &zp);
    zicv1.protocols = protos;
    OFFSET(zt_intercept_cfg_v1, protocols);
    model_list zads = {0};
    model_list_append(&zads, &zahost);
    model_list_append(&zads, &zacidr);
    zicv1.addresses = zads;
    OFFSET(zt_intercept_cfg_v1, addresses);
    model_list portranges = {0};
    model_list_append(&portranges, &pr);
    zicv1.port_ranges = portranges;
    OFFSET(zt_intercept_cfg_v1, port_ranges);
    model_map dopts = {0};
    model_map_set(&dopts, "key", "value");
    zicv1.dial_options = dopts;
    OFFSET(zt_intercept_cfg_v1, dial_options);
    zicv1.source_ip = "source_ip";
    OFFSET(zt_intercept_cfg_v1, source_ip);

    zt_server_cfg_v1 zscfgv1 = {0};
    zscfgv1.protocol = "protocol";
    OFFSET(zt_server_cfg_v1, protocol);
    zscfgv1.hostname = "hostname";
    OFFSET(zt_server_cfg_v1, hostname);
    zscfgv1.port = 443;
    OFFSET(zt_server_cfg_v1, port);

    zt_listen_options lopts = {0};
    lopts.bind_with_identity = true;
    OFFSET(zt_listen_options, bind_with_identity);
    duration d = 1000000;
    lopts.connect_timeout = d;
    OFFSET(zt_listen_options, connect_timeout);
    lopts.connect_timeout_seconds = 100;
    OFFSET(zt_listen_options, connect_timeout_seconds);
    lopts.cost = 2;
    OFFSET(zt_listen_options, cost);
    lopts.identity = "identity";
    OFFSET(zt_listen_options, identity);
    lopts.max_connections = 10;
    OFFSET(zt_listen_options, max_connections);
    lopts.precendence = "precedence";
    OFFSET(zt_listen_options, precendence);

    zt_host_cfg_v1 zhcv1 = {0};
    zhcv1.protocol = "protocol";
    OFFSET(zt_host_cfg_v1, protocol);
    zhcv1.forward_protocol = true;
    OFFSET(zt_host_cfg_v1, forward_protocol);
    string_array apa = calloc(sizeof(char*), 3);
    apa[0] = "proto1";
    apa[1] = "proto2";
    zhcv1.allowed_protocols = apa;
    OFFSET(zt_host_cfg_v1, allowed_protocols);
    zhcv1.address = "address";
    OFFSET(zt_host_cfg_v1, address);
    zhcv1.forward_address = true;
    OFFSET(zt_host_cfg_v1, forward_address);
    zt_address_array allowadds = calloc(sizeof(zt_address), 2);
    allowadds[0] = &zahost;
    zhcv1.allowed_addresses = allowadds;
    OFFSET(zt_host_cfg_v1, allowed_addresses);
    zhcv1.port = 1090;
    OFFSET(zt_host_cfg_v1, port);
    zhcv1.forward_port = true;
    OFFSET(zt_host_cfg_v1, forward_port);
    zt_port_range_array zpra = calloc(sizeof(zt_port_range), 2);
    zpra[0] = &zpr;
    zhcv1.allowed_port_ranges = zpra;
    OFFSET(zt_host_cfg_v1, allowed_port_ranges);
    zhcv1.allowed_source_addresses = allowadds;
    OFFSET(zt_host_cfg_v1, allowed_source_addresses);
    zhcv1.listen_options = &lopts;
    OFFSET(zt_host_cfg_v1, listen_options);

    zt_host_cfg_v2 hv2 = {0};
    model_list terms = {0};
    model_list_append(&terms, &hv2);
    hv2.terminators = terms;
    OFFSET(zt_host_cfg_v2, terminators);

    zt_mfa_enrollment mfae = {0};
    mfae.is_verified = true;
    OFFSET(zt_mfa_enrollment, is_verified);
    string_array codes = calloc(sizeof(char*), 2);
    codes[0] = "code1";
    mfae.recovery_codes = codes;
    OFFSET(zt_mfa_enrollment, recovery_codes);
    mfae.provisioning_url = "provisioningUrl";
    OFFSET(zt_mfa_enrollment, provisioning_url);

    printf("\noffset output complete\n\n");
}






