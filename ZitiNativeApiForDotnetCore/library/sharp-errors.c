#define ZITI_SDK_MODEL_SUPPORT_H //prevent model_support.h from being processed

#include "zt/error_defs.h"
/*
This file is generated using the C preprocessor. Do not edit
*/
using System.ComponentModel;

namespace Hanzo ZT {
#define enum_id(err_id,s) _zt_##err_id,
    enum err {
        ZITI_ERRORS(enum_id)
    }

#define err_code(err_id,desc) [Description(desc)] err_id = -err._zt_##err_id,

    public enum ZitiStatus {
        ZITI_ERRORS(err_code)
    }
}