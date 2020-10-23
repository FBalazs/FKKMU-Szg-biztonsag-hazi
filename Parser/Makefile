PROG=converter
CC=g++
CPPFLAGS=-I.
LIBS=-lwebp -lwebpmux
OFILES = main.cpp Parser.cpp WebPAnim.cpp

build: 
	$(CC) -o $(PROG) $(CPPFLAGS) $(LIBS) $(OFILES)

clean:
	@rm -f $(PROG)