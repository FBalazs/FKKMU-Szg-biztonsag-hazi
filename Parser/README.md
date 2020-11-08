# caffparser

The parser can convert .caff files into .webp files.

## Compile

You can compile the software using the command `make clean all`.

## Usage

`caffparser [-i <path-to-input-file>] [-o <path-to-output-file>] [-m <path-to-input-metafile> | -pm]`

## Options

`-i <path-to-input-file>` or `--in <path-to-input-file>` or `--input <path-to-input-file>`   
If present, the input is read from the given file, if not, the input is read from the standard input. Note that the standard input was only tested on Ubuntu 20.04 and no other platforms.

`-o <path-to-output-file>` or `--out <path-to-output-file>` or `--output <path-to-output-file>`   
If present, the output is written to the given file, if not, the output is written directly to the standard output. Note that the standard output was only tested on Ubuntu 20.04 and no other platforms.

`-m <path-to-input-metafile>` or `--meta <path-to-input-metafile>`   
If present, the CAFF metadata (credits, captions, tags) is written in the given file. This option is incompatible with `-pm`.

`-pm` or `--printmeta`   
If present, the CAFF metadata (credits, captions, tags) is written on the standard output. This option is incompatible with `-m`.
