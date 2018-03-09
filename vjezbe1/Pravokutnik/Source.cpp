#include <iostream>
#include <fstream>
#include "Pravokutnik.h"
using namespace std;

int main() {
	Pravokutnik p[5];

	p->upis_u_polje(p);
	
	ofstream wFile("Pravokutnik.txt");
	if (!wFile) {
		cout << "Cannot open file!\n";
	}
	for (int i = 0; i < 5; i++) {
		wFile << "P(" << p[i].sirina << ", " << p[i].visina << ") = " << p[i].sirina*p[i].visina << endl;
	}
	wFile.close();

	return 0;
}