#ifndef __Console__
#define __Console__

#include <SFML\Graphics.hpp>
#include <iostream>
#include <thread>
#include <chrono>
#include <vector>
#include <time.h>
#include <string>
#include <fstream>

using namespace std::chrono_literals;
using std::chrono::system_clock;


class Console
{
public:


	Console(sf::RenderWindow *window);
	void draw();
	void create_if_click();
	void setShapes();
	void setText();
	int gen = 0;
	bool startWhiteboard = false;
	void clearAndMakeEmptyBoard();
	sf::RectangleShape whiteBoard;
	sf::RectangleShape start;
	sf::RectangleShape backButton;
	std::vector<std::vector<sf::RectangleShape>> mainArray;
private:
	sf::RenderWindow *pWindow;
	//Main vectors,arrays..
	bool isAlive[50][100];

	std::vector<sf::RectangleShape> tempArray;
	int howManyLifes[50][100];
	int mode = 0;
	int temporarytimekeeper = 0;


	//I changed colors during programing and i didnt want to change all blueLifes so i just change his color to grey :P

	//LIFE's
	sf::RectangleShape temp;
	sf::RectangleShape greenLife;
	sf::RectangleShape redLife;
	sf::RectangleShape blueLife;
	sf::RectangleShape noLife;
	sf::RectangleShape line;


	//FOR TEXT AND CLICKING
	sf::RectangleShape greenSquare;
	sf::RectangleShape redSquare;
	sf::RectangleShape blueSquare;
	sf::RectangleShape gun;

	sf::Font font;
	sf::Text greenSquareT;
	sf::Text redSquareT;
	sf::Text blueSquareT;
	
	sf::Text started;
	sf::Text whiteBoardT;
	sf::Text startT;
	sf::Text genT;
	std::string genSTR;
	
	sf::Text instructionsT;
	sf::Text backButtonT;

	sf::Text gunT;
	
	

	bool randomAlive();
	void fillArray0();
	void fillArray();
	void drawArray();
	void drawSecondState();
	void nearLives();
	void drawEverything();
	void drawGun();
};

#endif