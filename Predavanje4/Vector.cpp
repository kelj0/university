#include "Vector.h"


Vector::Vector()
{
	arr = new double[0];
	size = 0;
	capacity = 0;
}

Vector::Vector(int n)
{
	arr = new double[n];
	capacity = n;
}

Vector::Vector(int n, double element)
{
	arr = new double[n];
	capacity = n;
	size = n;
	for (int i = 0; i < size;i++) {
		arr[i] = element;
	}
}

void Vector::grow() 
{
	int temp_cap = capacity;
	capacity = int(capacity + capacity * 0.33);
	if (temp_cap == capacity) { capacity++; }
	
	double *arr_temp=new double[capacity];
	for (int i = 0; i < temp_cap; i++) {
		arr_temp[i] = arr[i];
	}
	delete[] arr;
	arr = arr_temp;
}

void Vector::insert(int n, double element)
{
	if (capacity == size)
		grow();

	double *temp_arr = new double[capacity];
	int realarrcounter = 0;
	for (int i = 0; i < capacity; i++) {
		if (i == n) {
			temp_arr[i] = element;
		}
		else {
			temp_arr[i] = arr[realarrcounter];
			realarrcounter++;
		}
	}
	size++;
	delete[] arr;
	arr = temp_arr;

}

int Vector::get_capacity() { return capacity; }


double Vector::get_elem(int n){ return arr[n]; }

int Vector::get_size()
{
	return size;
}

Vector::~Vector()
{
	delete[] arr;
}
