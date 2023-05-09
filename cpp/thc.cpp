#include <iostream>
#include <string.h>
#include "stdio.h"
#include "stdlib.h"
#include "thc.h"
using namespace std;
void thLaunch(int numOfTh) { thLaunch(intToCharptr(numOfTh)); }
void thLaunch(int numOfTh, char *lang) { thLaunch(intToCharptr(numOfTh), lang); }
void thLaunch(int numOfTh, string lang) { thLaunch(intToCharptr(numOfTh), lang.c_str()); }
void thLaunch(char *nameOfTh) { thLaunch(nameOfTh, "ru.js"); }
void thLaunch(char *nameOfTh, char *lang) {
	char start[40];
	if (atoi(nameOfTh) == NULL) snprintf(start, sizeof(start), ".\\thcrap_loader.exe %s %s", nameOfTh, lang);
	else snprintf(start, sizeof(start), ".\\thcrap_loader.exe th%s %s", nameOfTh, lang);
	system(start);

	free(nameOfTh);
	free(lang);
} //пока что запускаться не может.
void thLaunch(char *nameOfTh, string lang) { thLaunch(nameOfTh, lang.c_str()); }
void thLaunch(string nameOfTh) { thLaunch(nameOfTh.c_str()); }
void thLaunch(string nameOfTh, char *lang) { thLaunch(nameOfTh.c_str(), lang); }
void thLaunch(string nameOfTh, string lang) { thLaunch(nameOfTh.c_str(), lang.c_str()); }
char *intToCharptr(int num) {
	char *outp;
	snprintf(outp, sizeof(outp), "%d", num);
	return outp;
}