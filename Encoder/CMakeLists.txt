cmake_minimum_required(VERSION 3.17)
project(CaffEncoder)

set(CMAKE_CXX_STANDARD 14)

add_executable(caffencoder main.cpp CaffEncoder.h CaffEncoder.cpp)
target_link_libraries(caffencoder webp webpmux) #-lwebp -lwebpmux
