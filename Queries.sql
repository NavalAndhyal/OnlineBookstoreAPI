CREATE TABLE [dbo].[Role]
(
    [RoleId] INT IDENTITY(1,1) PRIMARY KEY,
    [RoleName] NVARCHAR(50) NOT NULL
);

CREATE TABLE [dbo].[User]
(
    [UserId] INT IDENTITY(1,1) PRIMARY KEY,
    [Username] NVARCHAR(50) NOT NULL,
    [Email] NVARCHAR(100) NOT NULL,
    [PasswordHash] NVARCHAR(255) NOT NULL,
    [FullName] NVARCHAR(100),
    [DateOfBirth] DATE,
    [RegistrationDate] DATETIME2 DEFAULT GETDATE(),
    [RoleId] INT FOREIGN KEY REFERENCES [dbo].[Role]([RoleId])
);

INSERT INTO [dbo].[Role] ([RoleName])
VALUES 
('Admin'),
('User'),
('Manager'),
('Customer');


INSERT INTO [dbo].[User2] ([Username], [Email], [PasswordHash], [FullName], [DateOfBirth], [RoleId])
VALUES 
('john_doe', 'john@example.com', 'password_1', 'John Doe', '1990-05-15', 1), -- Admin
('jane_smith', 'jane@example.com', 'password_2', 'Jane Smith', '1985-12-28', 2), -- User
('mike_jones', 'mike@example.com', 'password_3', 'Mike Jones', '1995-08-10', 2), -- User
('emily_taylor', 'emily@example.com', 'password_6', 'Emily Taylor', '1994-07-04', 2), -- User
('chris_anderson', 'chris@example.com', 'password_7', 'Chris Anderson', '1987-11-30', 2), -- User
('lisa_johnson', 'lisa@example.com', 'password_8', 'Lisa Johnson', '1993-01-09', 2), -- User
('matt_thompson', 'matt@example.com', 'password_11', 'Matt Thompson', '1989-10-18', 2), -- User
('jessica_white', 'jessica@example.com', 'password_12', 'Jessica White', '1996-02-14', 2), -- User
('natalie_hall', 'natalie@example.com', 'password_14', 'Natalie Hall', '1997-11-27', 2), -- User
('jacob_king', 'jacob@example.com', 'password_17', 'Jacob King', '1993-09-15', 2), -- User
('emma_roberts', 'emma@example.com', 'password_18', 'Emma Roberts', '1988-03-19', 2), -- User
('sara_green', 'sara@example.com', 'password_21', 'Sara Green', '1992-08-22', 2), -- User
('adam_williams', 'adam@example.com', 'password_22', 'Adam Williams', '1986-04-07', 2), -- User
('ashley_turner', 'ashley@example.com', 'password_23', 'Ashley Turner', '1991-06-30', 2), -- User
('matthew_carter', 'matthew@example.com', 'password_24', 'Matthew Carter', '1984-12-03', 2), -- User
('sophia_brown', 'sophia@example.com', 'password_25', 'Sophia Brown', '1995-03-28', 2), -- User
('nathan_hill', 'nathan@example.com', 'password_26', 'Nathan Hill', '1989-09-09', 2), -- User
('hannah_kelly', 'hannah@example.com', 'password_27', 'Hannah Kelly', '1993-02-12', 2), -- User
('william_evans', 'william@example.com', 'password_28', 'William Evans', '1987-07-15', 2), -- User
('olivia_ward', 'olivia@example.com', 'password_29', 'Olivia Ward', '1994-01-19', 2), -- User
('james_cook', 'james@example.com', 'password_30', 'James Cook', '1985-05-26', 2), -- User
('amelia_morris', 'amelia@example.com', 'password_31', 'Amelia Morris', '1990-11-02', 2), -- User
('daniel_bell', 'daniel@example.com', 'password_32', 'Daniel Bell', '1988-06-17', 2), -- User
('mia_hughes', 'mia@example.com', 'password_33', 'Mia Hughes', '1996-10-24', 2), -- User
('logan_ross', 'logan@example.com', 'password_34', 'Logan Ross', '1983-07-08', 2), -- User
('ava_long', 'ava@example.com', 'password_35', 'Ava Long', '1989-04-14', 2), -- User
('ryan_murphy', 'ryan@example.com', 'password_36', 'Ryan Murphy', '1992-12-27', 2), -- User
('eva_cruz', 'eva@example.com', 'password_37', 'Eva Cruz', '1986-08-31', 2), -- User
('jack_reed', 'jack@example.com', 'password_38', 'Jack Reed', '1991-02-05', 2); -- User
