#include <iostream>
#include <fstream>
#include <string>
#include <map>

int main() {
	std::ifstream in("Sifre_Drzava.csv");
	if (!in) {
		std::cout << "Unable to open file!\n";
		return 1;
	}
	std::string naziv;
	std::string sifra;
	std::getline(in, naziv); // Clear first row
	
	std::map<std::string, std::string> dict;
	std::pair<std::string, std::string> p;
	
	while (std::getline(in,naziv,';') && std::getline(in,sifra)){
		p.first= sifra;
		p.second= naziv;
		dict.insert(p);
	}
	std::string search;
	std::cout << "Unesite naziv drzave za pretrazit: ";
	std::getline(std::cin, search);
	
	std::map<std::string,std::string>::iterator it = dict.find(search);
	it != dict.end() ? (std::cout << "\nIme:" <<it->second) :std::cout << "\nNema trazene drzave!";

	in.close();
	std::cout << std::endl;
	return 0;
}