#pragma once
#include <iostream>
#include "Pravokutnik.h"
using namespace std;

void Pravokutnik::get_sirina(int x){
	sirina = x;
}

void Pravokutnik::get_visina(int y){
	visina = y;
}

void Pravokutnik::upis_u_polje(Pravokutnik p[]){
	for (int i = 0; i < 5; i++) {
		int x;
		int y;
		cout << "Sirina " << i + 1 << " pravokutnika:";
		cin >> x;
		cout << endl;
		p[i].sirina=x;
		cout << "Visina " << i + 1 << " pravokutnika:";
		cin >> y;
		cout << endl;
		p[i].visina=y;
	}
}


