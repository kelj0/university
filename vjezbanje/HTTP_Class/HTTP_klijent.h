#pragma once
#include <string>
#include <time.h>
class HTTP_klijent{
public:
	HTTP_klijent(std::string url,int port);
	HTTP_klijent(std::string url);
	std::string get_random_word();
	int get_port();
	std::string get_url();

private:
	std::string url;
	int port;
};

