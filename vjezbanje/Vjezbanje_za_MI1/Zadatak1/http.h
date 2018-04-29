#pragma once

#include <iostream>
#include <string>
#include <time.h>

class http
{
public:
	http(std::string ht, int p);
	http(std::string ht);
	std::string get();
	std::string post();
private:
	int port;
	std::string url;
};

