USE dbPruebaTecnicaABPOS;

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Type NVARCHAR(100) NOT NULL, -- Rol de usuario Admin, Seller o Accountant
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NULL,
    Mail NVARCHAR(256) NOT NULL,
    Password NVARCHAR(256) NOT NULL,
);

CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    Quantity INT NOT NULL,
    Active BIT NOT NULL, -- BIT 0 = Inactivo, BIT 1 = Activo
);
CREATE TABLE Sales (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Client NVARCHAR(100) NOT NULL,
    Description NVARCHAR(256) NOT NULL,
    Contact NVARCHAR(100) NOT NULL,
    TotalPrice DECIMAL(18,2) NOT NULL,
    CreationDate DATETIME NOT NULL,
    PaidDate DATETIME NULL,
    IsPaid BIT NOT NULL, -- BIT 0 = No pagado, BIT 1 = Pagado
);

CREATE TABLE SalesProducts (
    Id INT PRIMARY KEY IDENTITY(1,1),
    SalesId INT NOT NULL,
    ProductsId INT NOT NULL,
    FOREIGN KEY (SalesId) REFERENCES Sales(Id), -- Relación con la tabla Sales
    FOREIGN KEY (ProductsId) REFERENCES Products(Id), -- Relación con la tabla Products
);

DROP TABLE SalesProducts;
DROP TABLE Products;
DROP TABLE Sales;
DROP TABLE Users; -- Se eleiminaron las tablas para que luego Identity las creara de nuevo ya enlazada