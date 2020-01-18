#include <SFML/Graphics.hpp>
#include "Cvijet.h"


//void paint(sf::Vector2i localPosition){std::cout<<localPosition.x<<", "<<localPosition.y<<std::endl;}

int main(){
	sf::RenderWindow window(sf::VideoMode(400, 400), "Hello, SFML world!");
	window.setFramerateLimit(60);
	Cvijet cvijet(&window);
	int counter = 0;
	while (window.isOpen()) {
		sf::Event event;
		while (window.pollEvent(event)) {
			if (event.type == sf::Event::Closed)
				window.close();
		}
		//To get coordinates
		//sf::Vector2i localPosition = sf::Mouse::getPosition(window);
		//paint(localPosition);
		window.clear();
		cvijet.draw();
	
	}

	return 0;
}