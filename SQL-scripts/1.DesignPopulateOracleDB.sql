----------------VENDORS-TABLE-----------------------
--create VENDORS table
--create a check constraint for the IS_DELETED column
CREATE TABLE VENDORS 
(
  ID INT NOT NULL, 
  NAME VARCHAR2(50) NOT NULL, 
  CREATED_ON TIMESTAMP NOT NULL, 
  MODIFIED_ON TIMESTAMP, 
  IS_DELETED CHAR(1) DEFAULT 0 NOT NULL, 
  CONSTRAINT VENDORS_PK PRIMARY KEY (ID),
  CONSTRAINT IS_DELETED_CHK CHECK (IS_DELETED = '1' OR IS_DELETED = '0')
);

--create sequence and trigger to auto increment the primary key in VENDORS
CREATE SEQUENCE  VEND_SEQ MINVALUE 10 MAXVALUE 9999999999999999999999999999 INCREMENT BY 10;

create or replace TRIGGER VEND_BIR
BEFORE INSERT ON VENDORS
FOR EACH ROW

BEGIN
  SELECT VEND_SEQ.NEXTVAL
  INTO   :new.id
  FROM   dual;
END;

--insert sample data into VENDORS
insert into VENDORS(NAME, CREATED_ON) VALUES('Zagorka SA', CURRENT_TIMESTAMP);
insert into VENDORS(NAME, CREATED_ON) VALUES('Kamenitza SA', CURRENT_TIMESTAMP);
insert into VENDORS(NAME, CREATED_ON) VALUES('Bio Bulgaria Ltd.', CURRENT_TIMESTAMP);
insert into VENDORS(NAME, CREATED_ON) VALUES('Mondelez Bulgaria Ltd.', CURRENT_TIMESTAMP);
insert into VENDORS(NAME, CREATED_ON) VALUES('Intersnack Bulgaria Ltd.', CURRENT_TIMESTAMP);


----------------MEASURES-TABLE-----------------------
--create table MEASURES
CREATE TABLE MEASURES 
(
  ID INT NOT NULL, 
  NAME VARCHAR2(50) NOT NULL, 
  ABBREV VARCHAR2(10) NOT NULL, 
  CREATED_ON TIMESTAMP NOT NULL, 
  MODIFIED_ON TIMESTAMP, 
  IS_DELETED CHAR(1) DEFAULT 0 NOT NULL, 
  CONSTRAINT MEASURES_PK PRIMARY KEY (ID),
  CONSTRAINT MES_IS_DELETED_CHK CHECK (IS_DELETED = '1' OR IS_DELETED = '0')  
);

--create sequence and trigger to auto increment the primary key in MEASURES
CREATE SEQUENCE  MES_SEQ MINVALUE 100 MAXVALUE 9999999999999999999999999999 INCREMENT BY 100;

create or replace TRIGGER MES_BIR
BEFORE INSERT ON MEASURES
FOR EACH ROW

BEGIN
  SELECT MES_SEQ.NEXTVAL
  INTO   :new.id
  FROM   dual;
END;

--insert data
insert into MEASURES(NAME, ABBREV, CREATED_ON ) VALUES('milliliter', 'ml', CURRENT_TIMESTAMP);
insert into MEASURES(NAME, ABBREV, CREATED_ON) VALUES('liter', 'l', CURRENT_TIMESTAMP);
insert into MEASURES(NAME, ABBREV, CREATED_ON) VALUES('gram', 'gr', CURRENT_TIMESTAMP);
insert into MEASURES(NAME, ABBREV, CREATED_ON) VALUES('kilogram', 'kg', CURRENT_TIMESTAMP);
insert into MEASURES(NAME, ABBREV, CREATED_ON) VALUES('piece', 'pc', CURRENT_TIMESTAMP);


----------------PRODUCTS-TABLE-----------------------
--create PRODUCTS table
CREATE TABLE PRODUCTS 
(
  ID INT NOT NULL, 
  NAME VARCHAR2(50) NOT NULL, 
  VENDOR_ID INT NOT NULL, 
  MEASURE_ID INT NOT NULL, 
  PRICE NUMBER NOT NULL, 
  CREATED_ON TIMESTAMP NOT NULL, 
  MODIFIED_ON TIMESTAMP, 
  IS_DELETED CHAR(1) DEFAULT 0 NOT NULL,
  CONSTRAINT PRODUCTS_PK PRIMARY KEY(ID),
  CONSTRAINT PROD_IS_DELETED_CHK CHECK (IS_DELETED = '1' OR IS_DELETED = '0')
);

ALTER TABLE PRODUCTS
ADD CONSTRAINT FK_PRODUCTS_VENDORS
  FOREIGN KEY (VENDOR_ID)
  REFERENCES VENDORS(ID);

ALTER TABLE PRODUCTS
ADD CONSTRAINT FK_PRODUCTS_MEASURES
  FOREIGN KEY (MEASURE_ID)
  REFERENCES MEASURES(ID);
  
--create sequence and trigger to auto increment the primary key in PRODUCTS
CREATE SEQUENCE PROD_SEQ;

CREATE OR REPLACE TRIGGER PROD_BIR
BEFORE INSERT ON PRODUCTS
FOR EACH ROW

BEGIN
  SELECT PROD_SEQ.NEXTVAL
  INTO   :new.id
  FROM   dual;
END;