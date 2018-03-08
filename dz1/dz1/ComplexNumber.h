#ifndef __ComplexNumber_H__
#define __ComplexNumber_H__
#include <iostream>

class ComplexNumber {
public:
	
	void set_real(int real);
	void set_imaginary(int imaginary);
	double get();

	ComplexNumber();
	ComplexNumber(int real);
	ComplexNumber(int real, int imaginary);
	ComplexNumber(ComplexNumber k1, ComplexNumber k2);

};

#endif
