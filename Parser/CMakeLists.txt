cmake_minimum_required(VERSION 3.17)
project(Parser)

set(CMAKE_CXX_STANDARD 14)

add_executable(Parser main.cpp CaffParser.h CaffParser.cpp WebpEncoder.h WebpEncoder.cpp)
target_link_libraries(Parser webp webpmux) #-lwebp -lwebpmux
