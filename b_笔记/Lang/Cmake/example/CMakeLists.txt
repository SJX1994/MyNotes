project(helloSJX)
cmake_minimum_required(VERSION 3.10.2)
 # adder.cpp is the file that contains the adder function 这是和主工程一起编译的情况
add_executable(${PROJECT_NAME} "main.cpp" "adder.cpp")

# adder is the directory that contains the adder library 这是和主工程分开编译的情况
add_subdirectory(Adder)
target_include_directories(${PROJECT_NAME} PUBLIC Adder)
target_link_directories(${PROJECT_NAME} PRIVATE Adder)
target_link_libraries(${PROJECT_NAME} adder)
