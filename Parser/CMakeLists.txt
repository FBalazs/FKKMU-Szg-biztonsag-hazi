cmake_minimum_required(VERSION 3.17)
project(CaffParser)

set(CMAKE_CXX_STANDARD 14)

add_executable(caffparser main.cpp CaffParser.h CaffParser.cpp WebpEncoder.h WebpEncoder.cpp)
target_link_libraries(caffparser webp webpmux) #-lwebp -lwebpmux
