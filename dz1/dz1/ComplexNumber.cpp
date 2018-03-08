#include "ComplexNumber.h"

void ComplexNumber::set_real(){std::cout << "Setting real\n";}

void ComplexNumber::set_real(int a) {
	std::cout << "Setting real a->" << a << std::endl;
};

void ComplexNumber::set_imaginary(int a) {
	std::cout << "Setting imaginary a->" << a << std::endl;
};

double ComplexNumber::get() {
	std::cout << "Getting..\n";
	return 10;
};

ComplexNumber::ComplexNumber(){};
ComplexNumber::ComplexNumber(int real){};
ComplexNumber::ComplexNumber(int real,int imaginary){};
ComplexNumber::ComplexNumber(ComplexNumber k1,ComplexNumber k2){};
