#include <iostream>
#include <fstream>
#include "Pravokutnik.h"
using namespace std;
	
void upis_u_file(Pravokutnik p[]) {

	ofstream wFile("Pravokutnik.txt");
	if (!wFile) {
		cout << "Cannot open file!\n";
	}
	for (int i = 0; i < 5; i++) {
		wFile << "P(" << p[i].get_sirina() << ", " << p[i].get_visina() << ") = " << p[i].get_sirina()*p[i].get_visina() << endl;
	}
	wFile.close();
}

int main() {
	Pravokutnik p[5];

	for (int i = 0; i < 5; i++) {
		int x;
		int y;
		cout << "Sirina " << i + 1 << " pravokutnika:";
		cin >> x;
		cout << endl;
		p[i].set_sirina(x);
		cout << "Visina " << i + 1 << " pravokutnika:";
		cin >> y;
		cout << endl;
		p[i].set_visina(y);
	}
	upis_u_file(p);
	
	return 0;
}