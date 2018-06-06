drop table crypto
CREATE TABLE crypto ( 
id varchar(50) NOT NULL, 
name varchar(50) NOT NULL,
source varchar(50) NOT NULL,
timestamp  varchar(50),
price decimal(16,8),
volume_24h  varchar(50) NOT NULL,
change_7d  varchar(50) NOT NULL,
CONSTRAINT ct_primarykey PRIMARY KEY(id,name,timestamp,source)
)

Declare @JSON varchar(max)

SELECT @JSON = BulkColumn
FROM OPENROWSET (BULK 'C:\Users\Danalscy\crypto\crypto\spiders\data\coinmarketcap_05_06.JSON', SINGLE_CLOB) as j

insert into crypto
SELECT * FROM OPENJSON (@JSON) 
With (
id varchar(50), 
name varchar(50),
source varchar(50),
timestamp  varchar(50),
price  decimal(16,8),
volume_24h  varchar(50),
change_7d  varchar(50)
) 

Go
Declare @JSON1 varchar(max)
SELECT @JSON1 = BulkColumn
FROM OPENROWSET (BULK 'C:\Users\Danalscy\crypto\crypto\spiders\data\bitinfocharts_05_06.JSON', SINGLE_CLOB) as j

insert into crypto
SELECT * FROM OPENJSON (@JSON1) 
With (
id varchar(50), 
name varchar(50),
source varchar(50),
timestamp  varchar(50),
price  money,
volume_24h  varchar(50),
change_7d  varchar(50)
) 
where volume_24h != 'Low Vol' 


Go
select * from crypto c




 