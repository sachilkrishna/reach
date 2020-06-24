--Reach Tables
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

--CUSTOMER

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
create database reach2
use reach2
create table customerType
(customer_type_id int primary key, customer_type_name varchar(255))

create table address
(customer_id int, address_id int ,constraint pka primary key(customer_id,address_id) ,fname varchar(255), lname varchar(255), address varchar(1000), city varchar(255), state varchar(255),pincode varchar(255))

create table customer
(customer_id int primary key identity(1,1), customer_type_id int references customerType(customer_type_id), password varchar(255), customer_fname varchar(255),customer_lname varchar(255), primary_phone varchar(255), secondary_phone varchar(255), primary_email varchar(255), secondary_email varchar(255), billing_address int references address(address_id), shipping_address int references address(address_id))
