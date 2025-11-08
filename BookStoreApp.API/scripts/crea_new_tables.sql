USE BookStoreDb;
GO

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Authors' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE dbo.Authors
    (
        Id INT IDENTITY(1,1) CONSTRAINT PK_Authors PRIMARY KEY,
        FirstName NVARCHAR(50) NOT NULL,
        LastName NVARCHAR(50) NOT NULL,
        Bio NVARCHAR(250) NULL,
        CreatedDate DATETIME2 NOT NULL CONSTRAINT DF_Authors_CreatedDate DEFAULT (SYSUTCDATETIME())
    );
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Books' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE dbo.Books
    (
        Id INT IDENTITY(1,1) CONSTRAINT PK_Books PRIMARY KEY,
        Title NVARCHAR(50) NOT NULL,
        [Year] INT NULL,
        ISBN NVARCHAR(50) NOT NULL CONSTRAINT UQ_Books_ISBN UNIQUE,
        Summary NVARCHAR(250) NULL,
        Image NVARCHAR(50) NULL,
        Price DECIMAL(18,2) NULL,
        AuthorId INT NOT NULL,
        CreatedDate DATETIME2 NOT NULL CONSTRAINT DF_Books_CreatedDate DEFAULT (SYSUTCDATETIME()),
        CONSTRAINT FK_Books_Authors FOREIGN KEY (AuthorId) REFERENCES dbo.Authors(Id)
            ON DELETE CASCADE ON UPDATE CASCADE
    );
END;
