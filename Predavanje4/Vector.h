#pragma once

#include <iostream>

class Vector
{
public:
	Vector();
	Vector(int n);
	Vector(int n, double element);
	void insert(int n, double element); //Insert element "element" on position "n"
	int get_capacity(); //Gets capacity of vec
	double get_elem(int n); // Return element on position "n"
	int get_size();
	~Vector();
private:
	int capacity=0;
	int size=0;
	double *arr;

	void grow();
};

