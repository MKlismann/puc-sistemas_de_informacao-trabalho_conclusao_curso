USE ApiMappingDatabase;


-- Generates a fictitious test mass
-- 1) Register APIs in the table.
INSERT INTO [dbo].[APIs] ([Name] ,[Description] ,[Created] ,[Updated]) VALUES ('API_1', 'This is the API_1 Example', GETDATE(), NULL);
INSERT INTO [dbo].[APIs] ([Name] ,[Description] ,[Created] ,[Updated]) VALUES ('API_2', 'This is the API_2 Example', GETDATE(), NULL);
INSERT INTO [dbo].[APIs] ([Name] ,[Description] ,[Created] ,[Updated]) VALUES ('API_3',						 NULL, GETDATE(), NULL);
INSERT INTO [dbo].[APIs] ([Name] ,[Description] ,[Created] ,[Updated]) VALUES ('API_4', 'This is the API_3 Example', GETDATE(), NULL);
INSERT INTO [dbo].[APIs] ([Name] ,[Description] ,[Created] ,[Updated]) VALUES ('API_5', 'This is the API_4 Example', GETDATE(), NULL);
INSERT INTO [dbo].[APIs] ([Name] ,[Description] ,[Created] ,[Updated]) VALUES ('API_6', 'This is the API_5 Example', GETDATE(), NULL);
INSERT INTO [dbo].[APIs] ([Name] ,[Description] ,[Created] ,[Updated]) VALUES ('API_7',						 NULL, GETDATE(), NULL);



-- 2) Includes dependency scenarios between apis
---- 2.1) APIs that have no dependencies (have no dependencies with other APIs, or have been registered but have not yet consumed their dependencies, or )
------ 'API_1'
------ 'API_6'
------ 'API_7'

---- 2.2) APIs that depend on themselves (for whatever reason)
------ 'API 2' depends on 'API 2'
INSERT INTO [dbo].[APIs_Dependencies] ([Consumer], [Consumed], [Created], [Updated]) VALUES ('API_2', 'API_2', GETDATE(), NULL);

---- 2.3) APIs that depend on more than one other API
------ 'API 3' depends on 'API 2', 'API 4' and 'API 5'
INSERT INTO [dbo].[APIs_Dependencies] ([Consumer], [Consumed], [Created], [Updated]) VALUES ('API_3', 'API_2', GETDATE(), NULL);
INSERT INTO [dbo].[APIs_Dependencies] ([Consumer], [Consumed], [Created], [Updated]) VALUES ('API_3', 'API_4', GETDATE(), NULL);
INSERT INTO [dbo].[APIs_Dependencies] ([Consumer], [Consumed], [Created], [Updated]) VALUES ('API_3', 'API_5', GETDATE(), NULL);

---- 2.4) APIs that are dependent on just another API
------ 'API 4' depends on 'API 6'
INSERT INTO [dbo].[APIs_Dependencies] ([Consumer], [Consumed], [Created], [Updated]) VALUES ('API_4', 'API_6', GETDATE(), NULL);

------ 2.4.1) Outdated dependencies
-------- 'API 5' depends on 'API 7'
INSERT INTO [dbo].[APIs_Dependencies] ([Consumer], [Consumed], [Created], [Updated]) VALUES ('API_5', 'API_7', '2020-09-08 09:00:00.000', '2020-09-08 11:35:02.653');


-- 3) Register APIs_Resources in the table.
INSERT INTO [dbo].[APIs_Resources] ([Api_Name], [Resource], [Created], [Updated]) VALUES ('API_2', 'api_2/resource_1', GETDATE(), NULL);
INSERT INTO [dbo].[APIs_Resources] ([Api_Name], [Resource], [Created], [Updated]) VALUES ('API_2', 'api_2/resource_2', GETDATE(), NULL);
INSERT INTO [dbo].[APIs_Resources] ([Api_Name], [Resource], [Created], [Updated]) VALUES ('API_3', 'api_3/resource_1', GETDATE(), NULL);
INSERT INTO [dbo].[APIs_Resources] ([Api_Name], [Resource], [Created], [Updated]) VALUES ('API_3', 'api_3/resource_2', GETDATE(), NULL);
INSERT INTO [dbo].[APIs_Resources] ([Api_Name], [Resource], [Created], [Updated]) VALUES ('API_4', 'api_4/resource_1', GETDATE(), NULL);
INSERT INTO [dbo].[APIs_Resources] ([Api_Name], [Resource], [Created], [Updated]) VALUES ('API_5', 'api_5/resource_1', GETDATE(), NULL);
INSERT INTO [dbo].[APIs_Resources] ([Api_Name], [Resource], [Created], [Updated]) VALUES ('API_6', 'api_6/resource_1', GETDATE(), NULL);
INSERT INTO [dbo].[APIs_Resources] ([Api_Name], [Resource], [Created], [Updated]) VALUES ('API_7', 'api_7/resource_1', GETDATE(), NULL);



-- 3) Register APIs_Resources_Dependencies in the table.
INSERT INTO [dbo].[APIs_Resources_Dependencies] ([Consumer_Api], [Consumed_Api], [Consumer_Resource], [Consumed_Resource], [Created], [Updated]) 
	VALUES ('API_2', 'API_2', 'api_2/resource_1', 'api_2/resource_2', GETDATE(), NULL);

INSERT INTO [dbo].[APIs_Resources_Dependencies] ([Consumer_Api], [Consumed_Api], [Consumer_Resource], [Consumed_Resource], [Created], [Updated]) 
	VALUES ('API_3', 'API_2', 'api_3/resource_2', 'api_2/resource_2', GETDATE(), NULL);
INSERT INTO [dbo].[APIs_Resources_Dependencies] ([Consumer_Api], [Consumed_Api], [Consumer_Resource], [Consumed_Resource], [Created], [Updated]) 
	VALUES ('API_3', 'API_4', 'api_3/resource_1', 'api_4/resource_1', GETDATE(), NULL);
INSERT INTO [dbo].[APIs_Resources_Dependencies] ([Consumer_Api], [Consumed_Api], [Consumer_Resource], [Consumed_Resource], [Created], [Updated]) 
	VALUES ('API_3', 'API_5', 'api_3/resource_2', 'api_5/resource_1', GETDATE(), NULL);

INSERT INTO [dbo].[APIs_Resources_Dependencies] ([Consumer_Api], [Consumed_Api], [Consumer_Resource], [Consumed_Resource], [Created], [Updated]) 
	VALUES ('API_4', 'API_6', 'api_4/resource_1', 'api_6/resource_1', GETDATE(), NULL);

INSERT INTO [dbo].[APIs_Resources_Dependencies] ([Consumer_Api], [Consumed_Api], [Consumer_Resource], [Consumed_Resource], [Created], [Updated]) 
	VALUES ('API_5', 'API_7', 'api_5/resource_1', 'api_7/resource_1', GETDATE(), NULL);