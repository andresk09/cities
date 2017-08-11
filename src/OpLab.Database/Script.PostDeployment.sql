/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

DECLARE @NewCityId AS INT 

-- New York
INSERT [dbo].[Cities] 
       ([Name], 
        [Description]) 
VALUES (N'New York City', 
        N'The one with that big park.'); 

SET @NewCityId = @@IDENTITY 

INSERT [dbo].[PointsOfInterest] 
       ([Name], 
        [Description],  
        [CityId]) 
VALUES (N'Central Park', 
        N'The most visited urban park in the United States.', 
        @NewCityId); 

INSERT [dbo].[PointsOfInterest] 
       ([Name], 
        [Description], 
        [CityId]) 
VALUES (N'Empire State Building', 
        N'A 102-story skyscraper located in Midtown Manhattan.', 
        @NewCityId); 

-- Paris
INSERT [dbo].[Cities] 
       ([Name], 
        [Description]) 
VALUES (N'Paris', 
        N'The one with that big tower.'); 

SET @NewCityId = @@IDENTITY 

INSERT [dbo].[PointsOfInterest] 
       ([Name], 
        [Description], 
        [CityId]) 
VALUES (N'Eiffel Tower', 
        N'A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel.', 
        @NewCityId); 

INSERT [dbo].[PointsOfInterest] 
       ([Name], 
        [Description], 
        [CityId]) 
VALUES (N'The Louvre', 
        N'The world''s largest museum.', 
        @NewCityId); 
