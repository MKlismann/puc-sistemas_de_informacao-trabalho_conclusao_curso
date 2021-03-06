-- Creates de Database
CREATE DATABASE ApiMappingDatabase;



USE ApiMappingDatabase;



-- For a clean creation, delete the tables and data if it already exists
IF OBJECT_ID ('dbo.APIs_Dependencies') IS NOT NULL
	DROP TABLE APIs_Dependencies;

IF OBJECT_ID ('dbo.APIs_Resources_Dependencies') IS NOT NULL
	DROP TABLE APIs_Resources_Dependencies;

IF OBJECT_ID ('dbo.APIs_Resources') IS NOT NULL
	DROP TABLE APIs_Resources;

IF OBJECT_ID ('dbo.APIs') IS NOT NULL
	DROP TABLE APIs;



-- Creates the table containing information about the APIs
CREATE TABLE APIs
(
	Name		VARCHAR(250) NOT NULL UNIQUE,
	Description	VARCHAR(MAX) NULL,
	Created		DATETIME	 NOT NULL,
	Updated		DATETIME	 NULL,
	CONSTRAINT	PK_API_Name PRIMARY KEY (Name)
);

-- Creates the table containing information about the APIs Dependencies
CREATE TABLE APIs_Dependencies
(
	Consumer VARCHAR(250) NOT NULL,
	Consumed VARCHAR(250) NOT NULL,
	Created DATETIME	  NOT NULL,
	Updated DATETIME	  NULL,
	CONSTRAINT PK_Api_Dependency PRIMARY KEY (Consumer, Consumed),
	CONSTRAINT FK_API_Consumer	 FOREIGN KEY (Consumer) REFERENCES APIs(Name),
	CONSTRAINT FK_API_Consumed	 FOREIGN KEY (Consumed) REFERENCES APIs(Name)
);

-- Creates the table containing information about the APIs Resources
CREATE TABLE APIs_Resources
(
	Api_Name VARCHAR(250) NOT NULL,
	Resource VARCHAR(750) NOT NULL,
	Created DATETIME	  NOT NULL,
	Updated DATETIME	  NULL,
	CONSTRAINT PK_Resource PRIMARY KEY (Resource),
	CONSTRAINT FK_API_Name FOREIGN KEY (Api_Name) REFERENCES APIs(Name),
);

-- Creates the table containing information about the APIs Resources Dependencies
CREATE TABLE APIs_Resources_Dependencies
(
	Consumer_Api		VARCHAR(250) NOT NULL,
	Consumed_Api		VARCHAR(250) NOT NULL,
	Consumer_Resource	VARCHAR(750) NOT NULL,
	Consumed_Resource	VARCHAR(750) NOT NULL,
	Created				DATETIME	 NOT NULL,
	Updated				DATETIME	 NULL,
	CONSTRAINT PK_Resource_Dependency	  PRIMARY KEY (Consumer_Api, Consumed_Api, Consumer_Resource, Consumed_Resource),
	CONSTRAINT FK_Resource_API_Consumer	  FOREIGN KEY (Consumer_Api)	  REFERENCES APIs(Name),
	CONSTRAINT FK_Resource_API_Consumed	  FOREIGN KEY (Consumed_Api)	  REFERENCES APIs(Name),
	CONSTRAINT FK_APIs_Resources_Consumer FOREIGN KEY (Consumer_Resource) REFERENCES APIs_Resources(Resource),
	CONSTRAINT FK_APIs_Resources_Consumed FOREIGN KEY (Consumed_Resource) REFERENCES APIs_Resources(Resource),
);
