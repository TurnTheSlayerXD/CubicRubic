cmake_minimum_required(VERSION 3.23)
project(Cube)

set(CMAKE_CXX_STANDARD 23)

find_package(OpenGL REQUIRED)
find_package(GLUT REQUIRED)

set(GLUT_INCLUDE_DIR C:/glut/include/)




include_directories(${GLUT_INCLUDE_DIR})




add_executable(Cube main.cpp file_interaction.h CubicRubic.h initialization.cpp rotation_functions.cpp OpenGLInteractions.cpp)

target_link_libraries(Cube ${OPENGL_LIBRARIES})
target_link_libraries(Cube ${GLUT_LIBRARIES})
target_link_libraries(Cube -lopengl32 -g -lfreeglut)