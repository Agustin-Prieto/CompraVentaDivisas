# CompraVentaDivisas

### Descripción

Descripción breve del sistema:

El sistema es una plataforma de registro y seguimiento de transacciones de compra y venta de monedas extranjeras. Permite gestionar varias monedas, cada una con su nombre, símbolo y tipos de cambio diarios para compras y ventas. Además, las monedas pueden tener diferentes tipos de cambio dependiendo del origen, como oficial, contado con liquidación, dólar MEP, dólar Soja, entre otros.

Las principales funcionalidades del sistema incluyen:

- Registro de transacciones de compra y venta, que incluyen el monto operado y la persona que realizó la transacción.
- Control de límites mensuales: Se garantiza que las transacciones de compra no superen los 200 USD o su equivalente en pesos al tipo de cambio oficial dentro de un mes.
- Restricción de operaciones los fines de semana: No se permite realizar compra y venta de moneda los días sábado y domingo.
- Consulta de todos los movimientos de un cliente en particular, lo que facilita el seguimiento y la gestión de las transacciones de cada cliente.

El sistema proporciona una solución integral para el registro y seguimiento de las transacciones de compra y venta de monedas, cumpliendo con los requisitos de control y restricciones establecidos.

### Diagrama de clases

```mermaid
classDiagram
    class Client {
        << (PK) >>
        Id: uniqueidentifier
        Name: nvarchar(100)
    }
    
    class Currency {
        << (PK) >>
        Id: uniqueidentifier
        Name: nvarchar(100)
        Symbol: nvarchar(10)
    }
    
    class ExchangeRate {
        << (PK) >>
        Id: uniqueidentifier
        BuyValue: decimal(18, 2)
        SellValue: decimal(18, 2)
        Date: datetime
        Type: nvarchar(50)
        CurrencyId: uniqueidentifier
    }
    
    class Transaction {
        << (PK) >>
        Id: uniqueidentifier
        AmountOperated: decimal(18, 2)
        AmountInPesos: decimal(18, 2)
        Date: datetime
        Type: nvarchar(50)
        CurrencyId: uniqueidentifier
        ExchangeRateId: uniqueidentifier
        ClientId: uniqueidentifier
    }
    
    Client "1" -- "0..*" Transaction
    Currency "1" -- "0..*" ExchangeRate
    Currency "1" -- "0..*" Transaction
    ExchangeRate "1" -- "0..*" Transaction