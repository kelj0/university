#ifndef CVIJET_H_
#define CVIJET_H_
#include <SFML\Graphics.hpp>
#include <thread>
#include <chrono>


using namespace std::chrono_literals;
using std::chrono::system_clock;

class Cvijet{

public:
	Cvijet(sf::RenderWindow* window);
	void draw();

private:
	sf::RenderWindow *Pwindow;
};

#endif