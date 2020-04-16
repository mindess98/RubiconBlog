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

-- Remove any existing data
DELETE FROM PostsTags

DELETE FROM Posts

DELETE FROM Tags


INSERT INTO Tags (Name)
VALUES ('Lifestyle'),
       ('Automotive'),
       ('Tech'),
       ('Travel'),
       ('Sports'),
       ('Business'),
       ('Finance'),
       ('Economy')


INSERT INTO Posts (Slug, Title, [Description], Body)
VALUES ('lorem-ipsum-dolor-sit-amet', 'Lorem ipsum dolor sit amet', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.'),
       ('ut-enim-ad-minim-veniam', 'Ut enim ad minim veniam', 'Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.', 'Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.')


INSERT INTO PostsTags (PostId, TagId)
VALUES (1, 1),
       (1, 2),
       (1, 3),
       (1, 6),
       (2, 2),
       (2, 8)
