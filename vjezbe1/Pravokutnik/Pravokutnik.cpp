#pragma once
#include <iostream>
#include "Pravokutnik.h"
using namespace std;

void Pravokutnik::set_sirina(int x){
	sirina = x;
}

void Pravokutnik::set_visina(int y){
	visina = y;
}

int Pravokutnik::get_sirina(){
	return sirina;
}

int Pravokutnik::get_visina(){
	return visina;
}


