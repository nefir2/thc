#include "stdio.h"
#include "windows.h"

int main(int argc, char **argv) {
	int argl = argc - 1; //args without path to this program
	if (argl == 2) system(sprintf(".\\thcrap_loader.exe th%d %s", argv[1], argv[2]));
	else if (argl == 1 && argv[1] ) system(sprintf(".\\thcrap_loader.exe th%d ru.js", argv[1]));
	else if (argl == 0) printf("input a number of touhou game.");
	return 0;
}
