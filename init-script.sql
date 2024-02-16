CREATE DATABASE CompraVentaDivisas;
GO

USE [CompraVentaDivisas]
GO

/****** Object:  Table [dbo].[Client]    Script Date: 14/2/2024 19:12:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Client](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

----------------------------------------------------------------------------

USE [CompraVentaDivisas]
GO

/****** Object:  Table [dbo].[Currency]    Script Date: 14/2/2024 19:13:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Currency](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Symbol] [nvarchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

----------------------------------------------------------------------------

USE [CompraVentaDivisas]
GO

/****** Object:  Table [dbo].[ExchangeRate]    Script Date: 14/2/2024 19:13:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ExchangeRate](
	[Id] [uniqueidentifier] NOT NULL,
	[BuyValue] [decimal](18, 2) NULL,
	[SellValue] [decimal](18, 2) NULL,
	[Date] [datetime] NULL,
	[Type] [nvarchar](50) NULL,
	[CurrencyId] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ExchangeRate]  WITH CHECK ADD FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Currency] ([Id])
GO

----------------------------------------------------------------------------

USE [CompraVentaDivisas]
GO

/****** Object:  Table [dbo].[Transaction]    Script Date: 14/2/2024 19:14:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Transaction](
	[Id] [uniqueidentifier] NOT NULL,
	[AmountOperated] [decimal](18, 2) NULL,
	[AmountInPesos] [decimal](18, 2) NULL,
	[Date] [datetime] NULL,
	[Type] [nvarchar](50) NULL,
	[CurrencyId] [uniqueidentifier] NULL,
	[ExchangeRateId] [uniqueidentifier] NULL,
	[ClientId] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([Id])
GO

ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Currency] ([Id])
GO

ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD FOREIGN KEY([ExchangeRateId])
REFERENCES [dbo].[ExchangeRate] ([Id])
GO

USE [CompraVentaDivisas]
GO

/****** Object:  StoredProcedure [dbo].[spClient_GetMontlhyPurchaseAmount]    Script Date: 14/2/2024 19:18:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Agustin Prieto>
-- Create date: <Create Date,,>
-- =============================================
CREATE PROCEDURE [dbo].[spClient_GetMontlhyPurchaseAmount]
    @ClientId uniqueidentifier
AS
BEGIN
    DECLARE @StartDate DATETIME
    DECLARE @EndDate DATETIME
    DECLARE @MonthlyPurchaseAmount DECIMAL(18, 2)

    -- Obtener la fecha de inicio del mes actual
    SET @StartDate = DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0)
    -- Obtener la fecha de finalización del mes actual
    SET @EndDate = DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)

    -- Seleccionar la suma del monto operado dentro del mes actual para el cliente dado
    SELECT @MonthlyPurchaseAmount = ISNULL(SUM(AmountOperated), 0)
    FROM [dbo].[Transaction]
    WHERE ClientId = @ClientId
        AND Date >= @StartDate
        AND Date < @EndDate;

    -- Validar si no hay transacciones para el cliente dado y devolver 0 en ese caso
    IF @MonthlyPurchaseAmount IS NULL
    BEGIN
        SET @MonthlyPurchaseAmount = 0
    END

    -- Devolver el monto mensual de compra
    SELECT @MonthlyPurchaseAmount AS MonthlyPurchaseAmount
END

GO

-----------------------------------------------------------------------

USE [CompraVentaDivisas]
GO

/****** Object:  StoredProcedure [dbo].[spTransaction_GetAll]    Script Date: 14/2/2024 19:19:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spTransaction_GetAll]
AS
BEGIN
	SELECT  trans.Id,
			trans.AmountOperated,
			trans.AmountInPesos,
			trans.Date,
			trans.Type,
			trans.CurrencyId,
			curr.Name,
			curr.Symbol,
			trans.ExchangeRateId,
			exch.BuyValue,
			exch.SellValue,
			exch.Date,
			exch.Type,
			--exch.CurrencyId,
			trans.ClientId,
			client.Name 
	FROM [dbo].[Transaction] trans
	JOIN [dbo].[Currency] curr on trans.CurrencyId = curr.Id
	JOIN [dbo].[ExchangeRate] exch on trans.ExchangeRateId = exch.Id
	JOIN [dbo].[Client] client on trans.ClientId = client.Id;
END
GO

-----------------------------------------------------------------------

USE [CompraVentaDivisas]
GO

/****** Object:  StoredProcedure [dbo].[spTransaction_GetById]    Script Date: 14/2/2024 19:19:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Agustin Prieto>
-- Create date: <Create Date,,>
-- =============================================
CREATE PROCEDURE [dbo].[spTransaction_GetById]
	@Id uniqueidentifier
AS
BEGIN
	SELECT  trans.Id,
			trans.AmountOperated,
			trans.AmountInPesos,
			trans.Date,
			trans.Type,
			trans.CurrencyId,
			curr.Name,
			curr.Symbol,
			trans.ExchangeRateId,
			exch.BuyValue,
			exch.SellValue,
			exch.Date,
			exch.Type,
			--exch.CurrencyId,
			trans.ClientId,
			client.Name

			FROM [dbo].[Transaction] trans
			JOIN [dbo].[Currency] curr on trans.CurrencyId = curr.Id
			JOIN [dbo].[ExchangeRate] exch on trans.ExchangeRateId = exch.Id
			JOIN [dbo].[Client] client on trans.ClientId = client.Id
		WHERE trans.Id = @Id;
END
GO

-----------------------------------------------------------------------

USE [CompraVentaDivisas]
GO

/****** Object:  StoredProcedure [dbo].[spTransaction_Insert]    Script Date: 14/2/2024 19:19:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Agustin Prieto>
-- Create date: <Create Date,,>
-- =============================================
CREATE PROCEDURE [dbo].[spTransaction_Insert]
	@Id uniqueidentifier,
	@AmountOperated decimal(18,2), 
	@AmountInPesos decimal(18,2),
	@Date datetime,
	@Type nvarchar(50),
	@CurrencyId uniqueidentifier,
	@ExchangeRateId uniqueidentifier,
	@ClientId uniqueidentifier
AS
BEGIN
	INSERT INTO [dbo].[Transaction] (Id, AmountOperated, AmountInPesos, Date, Type, CurrencyId, ExchangeRateId, ClientId)
	VALUES (@Id, @AmountOperated, @AmountInPesos, @Date, @Type, @CurrencyId, @ExchangeRateId, @ClientId);
END
GO

-----------------------------------------------------------------------

/****** Object:  StoredProcedure [dbo].[spTransaction_Update]    Script Date: 14/2/2024 19:19:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Agustin Prieto>
-- Create date: <Create Date,,>
-- =============================================
CREATE PROCEDURE [dbo].[spTransaction_Update]
	@Id uniqueidentifier,
	@AmountOperated decimal(18,2), 
	@AmountInPesos decimal(18,2),
	@Date datetime,
	@Type nvarchar(50),
	@CurrencyId uniqueidentifier,
	@ExchangeRateId uniqueidentifier,
	@ClientId uniqueidentifier
AS
BEGIN
	UPDATE [dbo].[Transaction] 
	SET AmountOperated = @AmountOperated,
		AmountInPesos = @AmountInPesos,
		Date = @Date,
		Type = @Type,
		CurrencyId = @CurrencyId,
		ExchangeRateId = @ExchangeRateId,
		ClientId = @ClientId
	WHERE Id = @Id;
END
GO

-----------------------------------------------------------------------

USE [CompraVentaDivisas]
GO

/****** Object:  StoredProcedure [dbo].[spTransaction_Delete]    Script Date: 14/2/2024 19:20:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Agustin Prieto>
-- Create date: <Create Date,,>
-- =============================================
CREATE PROCEDURE [dbo].[spTransaction_Delete]
	@Id uniqueidentifier
AS
BEGIN
	DELETE
	FROM [dbo].[Transaction]
	WHERE Id = @Id;
END
GO

-----------------------------------------------------------------------

USE [CompraVentaDivisas]
GO

/****** Object:  StoredProcedure [dbo].[spTransaction_GetAll]    Script Date: 16/2/2024 17:24:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spTransaction_GetByClientId]
	@ClientId uniqueidentifier
AS
BEGIN
	SELECT  trans.Id,
			trans.AmountOperated,
			trans.AmountInPesos,
			trans.Date,
			trans.Type,
			trans.CurrencyId,
			curr.Name,
			curr.Symbol,
			trans.ExchangeRateId,
			exch.BuyValue,
			exch.SellValue,
			exch.Date,
			exch.Type,
			trans.ClientId,
			client.Name 
	FROM [dbo].[Transaction] trans
	JOIN [dbo].[Currency] curr on trans.CurrencyId = curr.Id
	JOIN [dbo].[ExchangeRate] exch on trans.ExchangeRateId = exch.Id
	JOIN [dbo].[Client] client on trans.ClientId = client.Id
	WHERE trans.ClientId = @ClientId;
END
GO


