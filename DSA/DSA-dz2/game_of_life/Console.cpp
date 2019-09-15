#include "Console.h"

Console::Console(sf::RenderWindow * window)
{
	pWindow = window;
	font.loadFromFile("arial.ttf");
	if (!font.loadFromFile("arial.ttf")) {
		std::cout << "\nSOMETHING IS WRONG WITH LOADING FONT(6 line in Console.cpp)\n";
	}

}

void Console::drawEverything() {

	pWindow->draw(line);
	pWindow->draw(greenSquare);
	pWindow->draw(blueSquare);
	pWindow->draw(redSquare);
	pWindow->draw(greenSquareT);
	pWindow->draw(blueSquareT);
	pWindow->draw(redSquareT);
	pWindow->draw(whiteBoard);
	pWindow->draw(whiteBoardT);
	pWindow->draw(genT);
	pWindow->draw(madeByT);
	if (startWhiteboard) {
		pWindow->draw(start);
		pWindow->draw(startT);
		pWindow->draw(backButton);
		pWindow->draw(backButtonT);
		pWindow->draw(instructionsT);
		pWindow->draw(gun);
		pWindow->draw(gunT);
		whiteBoardT.setString("CLEAR");
		whiteBoardT.setPosition(sf::Vector2f(475, 85));
	}
}

void Console::draw()
{
	whiteBoardT.setString("WHITEBOARD");
	whiteBoardT.setPosition(sf::Vector2f(450,85));
	pWindow->clear(sf::Color(0, 0, 0));

	genSTR = "Gen:" + std::to_string(gen);
	genT.setString(genSTR);

	
	drawArray();
	drawEverything();
	pWindow->display();
	
	create_if_click();

	sf::sleep(sf::Time(sf::milliseconds(10)));


	create_if_click();

	pWindow->clear(sf::Color(0, 0, 0));
	drawEverything();
	
	drawSecondState();
	pWindow->display();
	
	create_if_click();	

	sf::sleep(sf::Time(sf::milliseconds(10)));

	create_if_click();

}

bool Console::randomAlive()
{
	int randn = rand() % 4;
	if (randn == 3)
		return true;
	return false;
}

void Console::drawArray() 
{
	if (!gen == 0) {
		fillArray();
	}
	else
		fillArray0();
	gen++;

	int y = 210;
	for (int i = 0; i < 50; ++i) {
		int x = 0;
		for (int j = 0; j < 100; ++j) {
			mainArray[i][j].setPosition(x,y);
			pWindow->draw(mainArray[i][j]);
			x += 10;
		}
		y += 10;
	}
	nearLives();
}

void Console::fillArray0()
{
	setShapes();
	for (unsigned int y = 0; y < 50; ++y) {
		mainArray.push_back(tempArray);
		for (unsigned int x = 0; x < 100; ++x) {
			if (randomAlive()) {
				mainArray[y].push_back(greenLife);
				isAlive[y][x] = 1;
			}
			else {
				mainArray[y].push_back(noLife);
				isAlive[y][x] = 0;
			}
		}
	}
	nearLives();
}

void Console::fillArray() 
{
	for (unsigned int y = 0; y < 50; ++y) {
		for (unsigned int x = 0; x < 100; ++x) {
			if (isAlive[y][x]) {
				if (mainArray[y][x].getFillColor() == redLife.getFillColor()) {
					mainArray[y][x] = noLife;
					isAlive[y][x] = false;
				}
				else{
					if (howManyLifes[y][x] == 2 || howManyLifes[y][x] == 3) {
						mainArray[y][x] = greenLife;
						isAlive[y][x] = true;
					}
					else if (howManyLifes[y][x] < 2 || howManyLifes[y][x]>3) {
						mainArray[y][x] = redLife;
						isAlive[y][x] = true;
					}
				}
			}
			else {
				if (mainArray[y][x].getFillColor() == blueLife.getFillColor()) {
					mainArray[y][x] = greenLife;
					isAlive[y][x] = true;
				}
				else if (howManyLifes[y][x] == 3) {
					mainArray[y][x] = blueLife;
					isAlive[y][x] = false;
				}
				else {
					mainArray[y][x] = noLife;
					isAlive[y][x] = false;
				}
			}
		}
	}
	nearLives();
}

void Console::drawSecondState() 
{
	fillArray();
	int y = 210;
	for (int i = 0; i < 50; ++i) {
		int x = 0;
		for (int j = 0; j < 100; ++j) {
			mainArray[i][j].setPosition(x,y);
			pWindow->draw(mainArray[i][j]);
			x += 10;
		}
		y += 10;
	}
	nearLives();
}

void Console::setShapes()
{
	// CELLS
	greenLife.setSize(sf::Vector2f(8,8));
	greenLife.setFillColor(sf::Color(100, 250, 50));
	greenLife.setOutlineThickness(2);
	greenLife.setOutlineColor(sf::Color(77, 77, 0));

	redLife.setSize(sf::Vector2f(8, 8));
	redLife.setFillColor(sf::Color(100, 251, 50));
	redLife.setOutlineThickness(2);
	redLife.setOutlineColor(sf::Color(100,0,0));

	blueLife.setSize(sf::Vector2f(9, 9));
	blueLife.setOutlineThickness(1);
	blueLife.setFillColor(sf::Color(20, 20, 50));
	blueLife.setOutlineColor(sf::Color(100, 100, 100));

	noLife.setSize(sf::Vector2f(10,10));
	noLife.setFillColor(sf::Color(0,0,0));

	// Info
	greenSquare.setSize(sf::Vector2f(8, 8));
	greenSquare.setFillColor(sf::Color(100, 250, 50));
	greenSquare.setOutlineThickness(2);
	greenSquare.setOutlineColor(sf::Color(77, 77, 0));
	greenSquare.setPosition(sf::Vector2f(17,29));

	redSquare.setSize(sf::Vector2f(8, 8));
	redSquare.setFillColor(sf::Color(100, 250, 50));
	redSquare.setOutlineThickness(2);
	redSquare.setOutlineColor(sf::Color(100, 0, 0));
	redSquare.setPosition(sf::Vector2f(17,69));

	blueSquare.setSize(sf::Vector2f(9, 9));
	blueSquare.setFillColor(sf::Color(20, 20, 20));
	blueSquare.setOutlineThickness(1);
	blueSquare.setOutlineColor(sf::Color(100, 100, 100));
	blueSquare.setPosition(sf::Vector2f(17,109));

	line.setSize(sf::Vector2f(1000,5));
	line.setFillColor(sf::Color(250,250,250));
	line.setPosition(0,200);

	// Buttons
	whiteBoard.setSize(sf::Vector2f(100,30));
	whiteBoard.setFillColor(sf::Color(255, 255, 255));
	whiteBoard.setOutlineThickness(5);
	whiteBoard.setOutlineColor(sf::Color(77, 77, 0));
	whiteBoard.setPosition(sf::Vector2f(450,80));

	start.setSize(sf::Vector2f(50, 20));
	start.setFillColor(sf::Color(0, 170, 0));
	start.setOutlineThickness(1);
	start.setOutlineColor(sf::Color(255, 255, 255));
	start.setPosition(sf::Vector2f(475, 130));

	backButton.setSize(sf::Vector2f(50,30));
	backButton.setPosition(sf::Vector2f(370,100));
	backButton.setFillColor(sf::Color(200, 0, 0));
	backButton.setOutlineThickness(2);
	backButton.setOutlineColor(sf::Color(204, 204, 0));

	gun.setSize(sf::Vector2f(250,40));
	gun.setFillColor(sf::Color(236, 231, 19));
	gun.setOutlineThickness(1);
	gun.setOutlineColor(sf::Color(102, 51, 0));
	gun.setPosition(700,130);
}

void Console::nearLives() 
{
	if (sf::Mouse::isButtonPressed(sf::Mouse::Left) && whiteBoard.getGlobalBounds().contains((sf::Vector2f)sf::Mouse::getPosition(*pWindow))) {
		startWhiteboard = true;
		gen = 0;
	}
	for (unsigned int y = 0; y < 50; ++y) {
		for (unsigned int x = 0; x < 100; ++x) {
			int Lifes = 0;
			if (!(x == 0 || x == 99 || y == 0 || y == 49)) {
				for (int i = -1; i < 2; ++i) {
					for (int j = -1; j < 2; ++j) {
						if (i == 0 && j == 0) {
							continue;
						}
						else if (mainArray[y+i][x+j].getFillColor() == redLife.getFillColor() || mainArray[y+i][x+j].getFillColor() == greenLife.getFillColor()) {
							++Lifes;
						
						}
					
					}
				}
				howManyLifes[y][x] = Lifes;
			}
			else
				howManyLifes[y][x] = 0;
		}
	}
}

void Console::setText() 
{
	greenSquareT.setString("-Living cell");
	redSquareT.setString("-Dies in next gen");
	blueSquareT.setString("-Born in next gen");
	startT.setString("START");
	whiteBoardT.setString("WHITEBOARD");
	backButtonT.setString("BACK");
	started.setString("RUNNING");
	instructionsT.setString("Create new life - Left mouse button\n\"Kill\" life - Right mouse button");
	gunT.setString("Press me to draw GUN!");
	madeByT.setString("Made by Karlo");

	greenSquareT.setFont(font);
	redSquareT.setFont(font);
	blueSquareT.setFont(font);
	greenSquareT.setCharacterSize(20);
	redSquareT.setCharacterSize(20);
	blueSquareT.setCharacterSize(20);


	greenSquareT.setFillColor(sf::Color(255,255,255));
	redSquareT.setFillColor(sf::Color(255, 255, 255));
	blueSquareT.setFillColor(sf::Color(255, 255, 255));

	greenSquareT.setPosition(sf::Vector2f(30,20));
	redSquareT.setPosition(sf::Vector2f(30, 60));
	blueSquareT.setPosition(sf::Vector2f(30,100));

	
	startT.setPosition(sf::Vector2f(475,130));
	startT.setFillColor(sf::Color(0,0,0));
	startT.setCharacterSize(15);
	startT.setFont(font);

	whiteBoardT.setPosition(sf::Vector2f(450,85));
	whiteBoardT.setFillColor(sf::Color(0,0,0));
	whiteBoardT.setCharacterSize(15);
	whiteBoardT.setFont(font);

	genT.setCharacterSize(20);
	genT.setFillColor(sf::Color(255,255,255));
	genT.setFont(font);
	genT.setPosition(sf::Vector2f(30,140));

	backButtonT.setCharacterSize(15);
	backButtonT.setFillColor(sf::Color(255,255,255));
	backButtonT.setFont(font);
	backButtonT.setPosition(sf::Vector2f(375,105));

	started.setCharacterSize(20);
	started.setFillColor(sf::Color(255, 255, 255));
	started.setFont(font);
	started.setPosition(sf::Vector2f(600, 100));

	instructionsT.setCharacterSize(20);
	instructionsT.setFillColor(sf::Color(255, 255, 255));
	instructionsT.setFont(font);
	instructionsT.setPosition(sf::Vector2f(600, 20));

	gunT.setPosition(715, 135);
	gunT.setCharacterSize(20);
	gunT.setFont(font);
	gunT.setFillColor(sf::Color(0,0,0));

	madeByT.setPosition(sf::Vector2f(250, 20));
	madeByT.setCharacterSize(20);
	madeByT.setFont(font);
	madeByT.setFillColor(sf::Color(255,255,255));
}

void Console::clearAndMakeEmptyBoard()
{

	blueLife.setOutlineColor(sf::Color(255, 255, 255));
	if (mode==0) {
		
		pWindow->clear(sf::Color(0, 0, 0));;
		//Set empty drawing board
		int y = 210;
		for (int i = 0; i < 50; ++i) {
			int x = 0;
			for (int j = 0; j < 100; ++j) {
				mainArray[i][j] = noLife;
				mainArray[i][j].setPosition(x, y);
				isAlive[i][j] = false;
				x += 10;
			}
			y += 10;
		}
		drawEverything();
			
		pWindow->display();

		++mode;
	}//if

	// Catch clicks and draw it 
	else if(mode==1) {
		pWindow->clear(sf::Color(0,0,0));
			
		create_if_click();
			
		int y = 210;
		for (int i = 0; i < 50; ++i) {
			int x = 0;
			for (int j = 0; j < 100; ++j) {
				mainArray[i][j].setPosition(x, y);
				pWindow->draw(mainArray[i][j]);
				x += 10;
			}
			y += 10;
		}
		drawEverything();
		pWindow->display();

		if (sf::Mouse::isButtonPressed(sf::Mouse::Left) && start.getGlobalBounds().contains((sf::Vector2f)sf::Mouse::getPosition(*pWindow)))
			mode = 2;
		if (sf::Mouse::isButtonPressed(sf::Mouse::Left) && whiteBoard.getGlobalBounds().contains((sf::Vector2f)sf::Mouse::getPosition(*pWindow))) {
			startWhiteboard = false;
			mode = 0;
		}
		if (sf::Mouse::isButtonPressed(sf::Mouse::Left) && gun.getGlobalBounds().contains((sf::Vector2f)sf::Mouse::getPosition(*pWindow))) {
			startWhiteboard = true;
			mode = 3;
			gen = 0;
		}
		if (sf::Mouse::isButtonPressed(sf::Mouse::Left) && backButton.getGlobalBounds().contains((sf::Vector2f)sf::Mouse::getPosition(*pWindow))) {
			startWhiteboard = false;
			gen = 0;
			mode = 0;
		}
	}//else if
		
	//Start automated process
	else if (mode == 2) {
		
		pWindow->clear(sf::Color(0, 0, 0));

		genSTR = "Gen:" + std::to_string(gen);
		genT.setString(genSTR);

		sf::sleep(sf::Time(sf::milliseconds(10)));
		create_if_click();

		nearLives();
		fillArray();
		int y = 210;
		for (int i = 0; i < 50; ++i) {
			int x = 0;
			for (int j = 0; j < 100; ++j) {
				mainArray[i][j].setPosition(x, y);
				pWindow->draw(mainArray[i][j]);
				x += 10;
			}
			y += 10;
		}
		drawEverything();
		pWindow->draw(started);
		++gen;
		pWindow->display();
		if (sf::Mouse::isButtonPressed(sf::Mouse::Left) && gun.getGlobalBounds().contains((sf::Vector2f)sf::Mouse::getPosition(*pWindow))) {
			startWhiteboard = true;
			mode = 3;
			gen = 0;
		}
		if (sf::Mouse::isButtonPressed(sf::Mouse::Left) && whiteBoard.getGlobalBounds().contains((sf::Vector2f)sf::Mouse::getPosition(*pWindow))) {
			startWhiteboard = false;
			mode = 0;
		}
		if (sf::Mouse::isButtonPressed(sf::Mouse::Left) && backButton.getGlobalBounds().contains((sf::Vector2f)sf::Mouse::getPosition(*pWindow))) {
			startWhiteboard = false;
			gen = 0;
			mode = 0;
		}
	} //else if
	else if (mode==3) {
		drawGun();
		mode = 1;
	}
} // FOO

void Console::create_if_click()
{

	if (sf::Mouse::isButtonPressed(sf::Mouse::Left)) {
		for (unsigned int y = 0; y < 50; ++y) {
			for (unsigned int x = 0; x < 100; ++x) {
				if (mainArray[y][x].getGlobalBounds().contains((sf::Vector2f)sf::Mouse::getPosition(*pWindow))) {
					mainArray[y][x] = blueLife;
					isAlive[y][x] = false;
				}
			}
		}
	}
	else if (sf::Mouse::isButtonPressed(sf::Mouse::Right)) {
		
		for (unsigned int y = 0; y < 50; ++y) {
			for (unsigned int x = 0; x < 100; ++x) {
				if (mainArray[y][x].getGlobalBounds().contains((sf::Vector2f)sf::Mouse::getPosition(*pWindow))) {
					mainArray[y][x] = noLife;
				
					isAlive[y][x] = false;
				}
			}
		}
	}
}

void Console::drawGun() {
	std::ifstream out("koordinate.txt");
	if (!out) {
		std::cout << "Txt file not loaded (koordinate.txt),line 473 -Console.cpp";
	}
	int tX;
	int tY;
	out >> tX;
	out >> tY;
	for (unsigned int y = 0; y < 50; ++y) {
		for (unsigned int x = 0; x < 100; ++x) {
			if(y==tY&&x==tX){
				mainArray[y][x] = greenLife;
				isAlive[y][x]=true;
				out >> tX;
				out >> tY;
			}
			else {
				mainArray[y][x] = noLife;
				isAlive[y][x]=false;
			}
		}
	}
	out.close();
}
