#include "zt/zt.h"
#include "zt4dotnet.h"

int main() {
    printf("\noffset output begins\n");
    zt_types_v2* rtn = z4d_struct_test();
    printf("\noffset output complete\n\n");
    free(rtn);
}
