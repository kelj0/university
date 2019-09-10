CREATE TABLE PINCODE(
    IDPincode integer primary key,
    Pincode text
);
CREATE TABLE [STATE](
    IDState integer primary key,
    [State] text
);
CREATE TABLE CITY(
    IDCity integer primary key,
    City text,
    StateID integer,
    PincodeID integer,
	foreign key(StateID) references [STATE](IDState),
	foreign key(PincodeID) references PINCODE(IDPincode)
);
CREATE TABLE STREET(
    IDStreet integer primary key, 
    Street text,
    CityID integer,
	foreign key(CityID) references CITY(IDCity)
);
CREATE TABLE DOORNO(
    IDDoorno integer primary key,
    Doorno text
);
CREATE TABLE MARTIALSTATUS(
    IDMartialStatus integer primary key,
    MartialStatus text
);
CREATE TABLE BLOODTYPE(
    IDBloodType,
    BloodType text
);
CREATE TABLE FOODTYPE(
    IDFoodType integer primary key,
    FoodType text
);
CREATE TABLE DIABETICTYPE(
    IDDiabeticType integer primary key,
    DiabeticType text
);
CREATE TABLE SEX(
    IDSex integer primary key,
    Sex text
);
CREATE TABLE PERSON(
	IDPerson integer primary key,
	Name text,
	BriefStatement text,
	SexID integer,
	DateOfBirth date,
	Telephone_1 text,
	Telephone_2 text,
	NextOfKinName text
	NextOfKinRelationshipWithPatient text,
	NextOfKintegerelephone_1 text,
	NextOfKintegerelephone_2 text,
	NextOfKinCityID integer,
	NextOfKinStateID integer,
	NextOfKinStreetID integer,
	NextOfKinDoornoID integer,
	NextOfKinPincodeID integer,
	PermanentCityID integer,
	PermanentStateID integer,
	PermanentStreetID integer,
	PermanentDoornoID integer,
	PermanentPincodeID integer,
	PresentCityID integer,
	PresentStateID integer,  
	PresentStreetID integer, 
	PresentDoornoID integer, 
	PresentPincodeID integer,
	MartialStatusID integer,
	NumberOfDependents integer,
	Height integer,
	Weight integer,
	BloodTypeID integer,
	Occupation text,
	GorssAnualIncome integer,
	Vegetarian text,
	Smoker text,
	AvgNumberOfCigarettesPerDay integer,
	Alcoholic text,
	AvgNumberOfDrinksPerDay integer,
	Stimulants text,
	InfoAboutStimulants text,
	AvgCoffieTeePerDay integer,
	AvgSoftDrinksPerDay integer,
	RegularMeals text,
	PredominantlyFoodTypeID integer,
	HistoryOfPreviousTreatment text,
	PhysicianTreated text,
	HospitalTreated text,
	Diabetic text,
	DiabeticTypeID integer,
	Hypertensive text,
	CardiacCondition text,
	RespiratoryCondition text,
	DigestiveCondition text,
	OrthopedicCondition text,
	MuscularCondition text,
	NeurologicalCondition text,
	KnownAlergies text,
	KnownReactionToDrugs text,
	MajorSurgeries text,
	foreign key(SexID) references SEX(IDSex),
	foreign key(NextOfKinCityID) references CITY(IDCity),
	foreign key(NextOfKinStateID) references [STATE](IDState),
	foreign key(NextOfKinStreetID) references STREET(IDStreet),
	foreign key(NextOfKinDoornoID) references DOORNO(IDDoorno),
	foreign key(NextOfKinPincodeID) references PINCODE(IDPincode),
	foreign key(PermanentCityID) references CITY(IDCity),
	foreign key(PermanentStateID) references [STATE](IDState),
	foreign key(PermanentStreetID) references STREET(IDStreet),
	foreign key(PermanentDoornoID) references DOORNO(IDDoorno),
	foreign key(PermanentPincodeID) references PINCODE(IDPincode),
	foreign key(PresentCityID) references CITY(IDCity),
	foreign key(PresentStateID) references [STATE](IDState),
	foreign key(PresentStreetID) references STREET(IDStreet),
	foreign key(PresentDoornoID) references DOORNO(IDDoorno),
	foreign key(PresentPincodeID) references PINCODE(IDPincode),
	foreign key(MartialStatusID) references MARTIALSTATUS(IDMartialStatus),
	foreign key(BloodTypeID) references BLOODTYPE(IDBloodType),
	foreign key(PredominantlyFoodTypeID) references FOODTYPE(IDFoodType),
	foreign key(DiabeticTypeID) references DIABETICTYPE(IDDiabeticType)
);

INSERT INTO PINCODE VALUES(null,'10001');
INSERT INTO PINCODE VALUES(null,'10002');
INSERT INTO PINCODE VALUES(null,'10003');
INSERT INTO PINCODE VALUES(null,'10004');
INSERT INTO PINCODE VALUES(null,'10005');
INSERT INTO [STATE] VALUES(null,'State1');
INSERT INTO [STATE] VALUES(null,'State2');
INSERT INTO [STATE] VALUES(null,'State3');
INSERT INTO CITY VALUES(null,'CITY1',1,1);
INSERT INTO CITY VALUES(null,'CITY2',1,1);
INSERT INTO CITY VALUES(null,'CITY3',2,2);
INSERT INTO CITY VALUES(null,'CITY4',2,3);
INSERT INTO CITY VALUES(null,'CITY5',3,4);
INSERT INTO CITY VALUES(null,'CITY6',3,5);
INSERT INTO STREET VALUES(null,'Street1',1);
INSERT INTO STREET VALUES(null,'Street2',2);
INSERT INTO STREET VALUES(null,'Street3',3);
INSERT INTO STREET VALUES(null,'Street4',4);
INSERT INTO STREET VALUES(null,'Street5',5);
INSERT INTO STREET VALUES(null,'Street6',6);
INSERT INTO DOORNO VALUES(null,'1A');
INSERT INTO DOORNO VALUES(null,'12B');
INSERT INTO DOORNO VALUES(null,'255');
INSERT INTO DOORNO VALUES(null,'321');
INSERT INTO DOORNO VALUES(null,'213B');
INSERT INTO DOORNO VALUES(null,'1231A');
INSERT INTO MARTIALSTATUS VALUES(null,'Married');
INSERT INTO MARTIALSTATUS VALUES(null,'Single');
INSERT INTO MARTIALSTATUS VALUES(null,'Divorced');
INSERT INTO MARTIALSTATUS VALUES(null,'Widowed');
INSERT INTO BLOODTYPE VALUES(null,'A-');
INSERT INTO BLOODTYPE VALUES(null,'A+');
INSERT INTO BLOODTYPE VALUES(null,'B-');
INSERT INTO BLOODTYPE VALUES(null,'B+');
INSERT INTO BLOODTYPE VALUES(null,'AB-');
INSERT INTO BLOODTYPE VALUES(null,'AB+');
INSERT INTO BLOODTYPE VALUES(null,'O-');
INSERT INTO BLOODTYPE VALUES(null,'O+');
INSERT INTO FOODTYPE VALUES(null,'Home food');
INSERT INTO FOODTYPE VALUES(null,'Outside food');
INSERT INTO DIABETICTYPE VALUES(null,'Type 1');
INSERT INTO DIABETICTYPE VALUES(null,'Type 2');
INSERT INTO SEX VALUES(null,'Male');
INSERT INTO SEX VALUES(null,'Female');

INSERT INTO PERSON VALUES(null,'Peric Middle Pero','My leg hurts',1,'1990-01-01 10:00:00','123456789','987654321','Mother1','Mother','120948213','102948123',1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,0,170,70,1,'Student',30,'n','n',0,'y',5,'n',null,2,1,'y',1,'Broken leg','n','y','n',null,'n','n','n','n','n','n','n',null,null);
INSERT INTO PERSON VALUES(null,'Ana Middle Anic','My arm hurts',2,'1970-01-01 10:00:00','123456789','987654321','Husband1','Husband','120948213','102948123',3,2,3,3,3,3,2,3,3,3,3,2,3,3,3,1,1,160,50,2,'Seller',70,'n','n',0,'n',null,'n',null,5,1,'y',1,'Broken leg','n','y','n',null,'n','n','n','n','n','n','n',null,null);