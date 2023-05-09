#include "stdio.h"
#include "stdlib.h"

int main(int argc, char **argv) {
	int argl = argc - 1; //args length. args without path to this program
	char *launch;
	if (argl == 2) {
		snprintf(launch, 40 * sizeof(char), ".\\thcrap_loader.exe th%s %s", argv[1], argv[2]);
		system(launch);
	}
	else if (argl == 1 && argv[1] ) {
		snprintf(launch, 40 * sizeof(char), ".\\thcrap_loader.exe th%s ru.js", argv[1]);
		system(launch);
	}
	else if (argl <= 0 || argl >= 3 ) {
		printf("write a number of touhou game.");
		return 0;
	}
	return 0;
}
