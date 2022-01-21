#include "snow_file_io.h"

char* snow_file_read_all_text(const char* filename, int* length)
{
	FILE *f = fopen(filename,"rb");
	char *text = (char *) malloc(1 << 20);
	*length = f ? (int) fread(text, 1, 1<<20, f) : -1;

	return text;
}