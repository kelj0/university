#include "ComplexNumber.h"



void ComplexNumber::set_real(int real) {
	std::cout << "Setting real a->" << real << std::endl;
};

void ComplexNumber::set_imaginary(int imaginary) {
	std::cout << "Setting imaginary a->" << imaginary << std::endl;
};

double ComplexNumber::get() {
	std::cout << "Getting..\n";
	return 10;
};

ComplexNumber::ComplexNumber(){};
ComplexNumber::ComplexNumber(int real){};
ComplexNumber::ComplexNumber(int real,int imaginary){};
ComplexNumber::ComplexNumber(ComplexNumber k1,ComplexNumber k2){};
