#include <SFML/Graphics.hpp>
#include "Console.h"


void mouseLocation(sf::Vector2i localPosition)
{
	std::cout<<localPosition.x<<", "<<localPosition.y<<std::endl;
}

int main() {
	srand(time(0));
	
	sf::RenderWindow window(sf::VideoMode(1000, 720), "GAME OF LIFE");
	window.setFramerateLimit(60);
	
	Console console(&window);
	console.setText();
	while (window.isOpen()) {
		sf::Event event;
		while (window.pollEvent(event)) {
			if (event.type == sf::Event::Closed)
				window.close();
		}
		sf::Vector2i localPosition = sf::Mouse::getPosition(window);
		mouseLocation(localPosition);

		console.draw();
	}

	return 0;
}