#include "HTTP_klijent.h"


HTTP_klijent::HTTP_klijent(std::string url, int port){
	this->url = url;
	this->port = port;
}

HTTP_klijent::HTTP_klijent(std::string url) {
	this->url = url;
	port = 80;
}


std::string HTTP_klijent::get_random_word(){
	char arr[] = {'a','s','d','e','f','g','h','j','k','l'}; //some chars to make output
	std::string random_word="          ";
	srand(static_cast<unsigned int>(time(0)));
	int random_number = (rand() % 10);
	for (int i = 0; i < 10; i++) {
		random_number = (rand() % 10);
		random_word[i] = arr[random_number];
	}
	
	return random_word;
}

int HTTP_klijent::get_port(){
	return port;
}

std::string HTTP_klijent::get_url(){
	return url;
}
