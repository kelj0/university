#include "ComplexNumber.h"

void set_real(){std::cout << "Setting real\n";}

void set_real(int a) {
	std::cout << "Setting real a->" << a << std::endl;
};

void set_imaginary(int a) {
	std::cout << "Setting imaginary a->" << a << std::endl;
};

double get() {
	std::cout << "Getting..\n";
	return 10;
};