--The SQL commands for e-commerce application using angular, sql-server, asp.net core web api.
/* PRODUCTS */
CREATE DATABASE PRODUCTS_DB;

USE PRODUCTS_DB;

--DROP TABLE Products;

--ALTER TABLE Products ALTER COLUMN [description] VARCHAR(MAX);

CREATE TABLE Products(
  id VARCHAR(250) PRIMARY KEY,
  title VARCHAR(255),
  [description] VARCHAR(MAX),
  price MONEY,
  category VARCHAR(30),
  stock INT,
  imageUrl VARCHAR(255)
);

--Inserting product
CREATE PROCEDURE sp_insert_products(
	@id VARCHAR(250),
	@title VARCHAR(255),
	@description VARCHAR(MAX),
	@price MONEY,
	@category VARCHAR(30),
	@stock INT,
	@img VARCHAR(255)
)
AS BEGIN
	INSERT INTO Products VALUES (@id, @title, @description, @price, @category, @stock, @img);
END;

EXEC sp_insert_products '3','Chart', 'Map that have treasure location', 58749654.99, 'Treasure', 1, '/assets/products/prod3.jpg';

--Updating product
CREATE PROCEDURE sp_update_products(
	@id VARCHAR(250),
	@title VARCHAR(255),
	@description VARCHAR(MAX),
	@price MONEY,
	@category VARCHAR(30),
	@stock INT,
	@img VARCHAR(255)
)
AS BEGIN
	UPDATE Products
	SET
	title = @title,
	[description] = @description,
	price = @price,
	category = @category,
	stock = @stock,
	imageUrl = @img
	WHERE id = @id;
END;

EXEC sp_update_products '1','Mobile', 'A good mobile', 19999.99, 'Unisex', 23, '/assets/products/prod1.jpg';

--Deleting product
CREATE PROCEDURE sp_delete_products(@id VARCHAR(250))
AS BEGIN
	DELETE FROM Products WHERE id = @id;
END;

EXEC sp_delete_products 2;

CREATE PROCEDURE sp_select_products
AS BEGIN
	SELECT * FROM Products;
END;

--UPDATE Products SET price = price/10;

EXEC sp_select_products;
SELECT * FROM Products;

/* USERS */
DROP TABLE [User]
DROP TABLE [Address]
--User table
CREATE TABLE [User](
	id INT IDENTITY(1,1) PRIMARY KEY ,
	username VARCHAR(16) UNIQUE,
	email VARCHAR(100) UNIQUE,
	[password] VARCHAR(255),
	phone VARCHAR(15),
	gender VARCHAR(10) CHECK (gender IN ('male', 'female', 'others')),
	age INT CHECK(age BETWEEN 18 AND 90),
	[role] VARCHAR(10) CHECK([role] IN ('admin', 'user'))
);


--User Insert procedure
CREATE PROCEDURE sp_insert_user(@username VARCHAR(16), @email VARCHAR(100), @pwd VARCHAR(255), @phone VARCHAR(15), @gender VARCHAR(10), @age INT, @role VARCHAR(10))
AS BEGIN
	INSERT INTO [User](username, email, [password], phone, gender, age, [role]) VALUES(@username, @email, @pwd, @phone, @gender, @age, @role)
END;


--Address table
CREATE TABLE [Address] (
	addressId INT IDENTITY(100,1) PRIMARY KEY,
	userId INT,
	addressType VARCHAR(20) CHECK(addressType IN ('home', 'work', 'other')),
	street VARCHAR(100),
	city VARCHAR(50) NOT NULL,
	[state] VARCHAR(50) NOT NULL,
	zipcode VARCHAR(10) NOT NULL,
	country VARCHAR(50) NOT NULL,
	FOREIGN KEY (userId) REFERENCES [User](id)
);

CREATE PROCEDURE sp_insert_user_address (
	@userId INT,
	@addressType VARCHAR(20),
	@street VARCHAR(100),
	@city VARCHAR(50),
	@state VARCHAR(50),
	@zipcode VARCHAR(10),
	@country VARCHAR(50)
	)
AS BEGIN
	INSERT INTO [Address](userId, addressType, street, city, [state], zipcode, country)
	VALUES (@userId, @addressType, @street, @city, @state, @zipcode, @country);
END;

EXEC sp_insert_user 'Kirankumar', 'kiran123@gmail.com', 'QAWSEDqawsed', '+91 7485964578', 'male', 31, 'admin';
EXEC sp_insert_user 'Charankumar', 'charan123@gmail.com', 'QAWSEDqawsed', '+91 6485964578', 'male', 45, 'user';

CREATE PROCEDURE sp_select_user

--CREATE PROCEDURE sp_select_userdetails
--AS BEGIN
--	SELECT id, username, email, gender, age, phone, addressType, street, city, [state], zipcode, country  FROM [User] AS U INNER JOIN [Address] AS A ON U.id = A.userId;
--END;

--EXEC sp_select_userdetails;

SELECT * FROM [User];
SELECT * FROM [Address];

CREATE PROCEDURE sp_update_user (
	@id INT,
	@username VARCHAR(16),
	@email VARCHAR(100),
	@pwd VARCHAR(255),
	@phone VARCHAR(15),
	@gender VARCHAR(10),
	@age INT,
	@role VARCHAR(10)
)

AS BEGIN
    UPDATE [User] SET
		username = @username,
		email = @email,
		[password] = @pwd,
		phone = @phone,
		gender = @gender,
		age = @age,
		[role] = @role
	WHERE id = @id;
END;

EXEC sp_update_user 1, 'Kirankumar', 'kiran@gmail.com', 'QAwsedrf', '+91 5689745896', 'male', 32, 'admin';

CREATE PROCEDURE sp_delete_user (@id INT)
AS BEGIN
	DELETE FROM [User] WHERE id = @id;
END;

EXEC sp_delete_user 1;