#library
add_executable(ztstructalignment2 ztstructalignment.c)

set_property(TARGET ztstructalignment2 PROPERTY C_STANDARD 11)

target_link_libraries(ztstructalignment2
        PUBLIC zt
        )