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

USE [CompraVentaDivisas]
GO

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