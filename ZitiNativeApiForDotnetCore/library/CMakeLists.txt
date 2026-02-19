#library
set(ZITI_4_DOTNET_HEADERS
        ${PROJECT_SOURCE_DIR}/includes/zt4dotnet.h
)
set(ZITI_4_DOTNET_SOURCE
        ${PROJECT_SOURCE_DIR}/library/zt4dotnet.c
)

add_library(zt4dotnet SHARED ${ZITI_4_DOTNET_SOURCE} ${ZITI_4_DOTNET_HEADERS} zt.def )

target_include_directories(zt4dotnet
        PUBLIC ${CMAKE_HOME_DIRECTORY}/includes
)

target_compile_definitions(zt4dotnet
        PUBLIC
                BUILD_DATE=${BUILD_DATE}
                GIT_BRANCH=${GIT_BRANCH}
                GIT_COMMIT_HASH=${GIT_COMMIT_HASH}
        PRIVATE
                BUILDING_ZITI_SHARED=1
                GIT_VERSION=${GIT_VERSION}
                ZITI_LOG_PREFIX=${PROJECT_NAME}
)

set_property(TARGET zt4dotnet PROPERTY C_STANDARD 11)

find_package(protobuf-c CONFIG REQUIRED)

target_link_libraries(zt4dotnet
        PUBLIC zt
        )
# USE WHEN DEBUGGING - target_link_options(zt4dotnet PRIVATE "/VERBOSE:LIB")

add_executable(ztstructalignment ${ZITI_4_DOTNET_HEADERS} ${PROJECT_SOURCE_DIR}/library/ztstructalignment.c)

target_include_directories(ztstructalignment
        PUBLIC ${CMAKE_HOME_DIRECTORY}/includes
        )

set_property(TARGET ztstructalignment PROPERTY C_STANDARD 11)

target_link_libraries(ztstructalignment
        PUBLIC zt4dotnet
        )
# USE WHEN DEBUGGING - target_link_options(ztstructalignment PRIVATE "/VERBOSE:LIB")
