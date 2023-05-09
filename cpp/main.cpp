#include <cstring>
#include "thc.h"
using namespace std;
void Usage(char *path);
int main(int argc, char **argv) {
	switch (argc) {
		case 1:
			thLaunch(13); //default touhou if no more touhous
			break;
		case 2:
			if (strcmp(argv[2], "-h") != 0 || strcmp(argv[2], "--help") != 0) thLaunch(argv[1]); 
			else Usage(argv[0]);
			break;
		case 3: 
			thLaunch(argv[1], argv[2]);
			break;
		default:
			Usage(argv[0]);
			break;
	}
	//if (argc == 2) thLaunch(argv[1]); 
	//else if (argc == 3) thLaunch(argv[1], argv[2]);
	//else Usage(argv[0]); //if (argc <= 1 || argc > 3 || (argc >= 2 && ( strcmp(argv[2], "--help") == 0 || strcmp(argv[2], "-h") == 0 ) ) ) Usage(argv[0]);
	return 0;
}

void Usage(char *path) {
	cout << "Usage: " << path << " {th} [lang]\n\t"
		"must be placed when \"thcrap_loader.exe\" is in %PATH% variable. launching choosed touhou with choosed language.\n\t"
		"th - number of touhou. may be choosed like \"th06\" or like \"06\"\n\t. cannot be \"6\" or \"th6\""
		"lang - language for choosed touhou. you can write in if it necessary to you. default=ru.js\n";
}