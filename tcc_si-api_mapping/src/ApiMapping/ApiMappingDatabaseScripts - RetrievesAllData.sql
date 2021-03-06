USE ApiMappingDatabase;



-- Retrieves the information stored in database
USE ApiMappingDatabase;

-- Retrieve API's information.
SELECT
	[Name], [Description], [Created], [Updated]
FROM
	[ApiMappingDatabase].[dbo].[APIs];

-- Retrieve API's resources information.
SELECT
	[Api_Name], [Resource], [Created], [Updated]
FROM 
	[ApiMappingDatabase].[dbo].[APIs_Resources]

-- Retrieve API's direct dependencies information.
SELECT
	[Consumer], [Consumed], [Created], [Updated]
FROM 
	[ApiMappingDatabase].[dbo].[APIs_Dependencies]

-- Retrieve API's resources dependencies information.
SELECT 
	[Consumer_Api], [Consumed_Api], [Consumer_Resource], [Consumed_Resource], [Created], [Updated]
FROM 
	[ApiMappingDatabase].[dbo].[APIs_Resources_Dependencies]