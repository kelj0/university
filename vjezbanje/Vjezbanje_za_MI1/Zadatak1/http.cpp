#include "http.h"



http::http(std::string ht,int p)
{
	url=ht;
	port = p;
	srand(static_cast<unsigned int>(time(0)));
}

http::http(std::string ht)
{
	url = ht;
	port = 80;
	srand(static_cast<unsigned int>(time(0)));
}

std::string http::get()
{
	std::string str="          ";
	//33-126
	
	for (int i = 0; i < 10;++i) {
		str[i] = (char)((rand() % (126 - 33)) + 33);
	}
	return str;
}

std::string http::post()
{
	std::string str = "          ";
	//33-126

	for (int i = 0; i < 10; ++i) {
		str[i] = (char)((rand() % (126 - 33)) + 33);
	}
	return str;
}


