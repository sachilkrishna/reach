--ADMIN-PC\SQLEXPRESS
        --create database reach
		use reach

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

--Product

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

		----------------------------------
		create table brand
		(brand_id int primary key, brand_name varchar(255), brand_logo varchar(1000),brand_description varchar(2000), brand_website varchar(1000))

		create table category
		(category_id int primary key, category_name varchar(255))
		
		create table product
		(product_id int primary key, brand_id int references brand(brand_id), category_id int references category(category_id),product_name varchar(255),product_images varchar(1000), product_short_decription varchar(1000), product_description varchar(5000), cost_price decimal(10,2), selling_price decimal(10,2) )

		create table stock
		(stock_id int primary key, product_id int references product(product_id), remaining_quantity int, total_sold_quantity int, last_updated datetime)
		
		---------------------------------
		insert into brand ( brand_id, brand_name , brand_logo ,brand_description , brand_website ) values(1, 'Nike','https://upload.wikimedia.org/wikipedia/commons/thumb/a/a6/Logo_NIKE.svg/1200px-Logo_NIKE.svg.png', 'Nike, Inc. is an American multinational corporation that is engaged in the design, development, manufacturing, and worldwide marketing and sales of footwear, apparel, equipment, accessories, and services. The company is headquartered near Beaverton, Oregon, in the Portland metropolitan area.','https://www.nike.com/in')
		insert into brand ( brand_id, brand_name , brand_logo ,brand_description , brand_website ) values(2, 'Adidas','https://upload.wikimedia.org/wikipedia/commons/thumb/2/20/Adidas_Logo.svg/200px-Adidas_Logo.svg.png', 'Adidas AG (German: [ˈʔadiˌdas] AH-dee-DAHS; stylized as ɑdidɑs since 1949[3]) is a multinational corporation, founded and headquartered in Herzogenaurach, Germany, that designs and manufactures shoes, clothing and accessories. It is the largest sportswear manufacturer in Europe, and the second largest in the world, after Nike.','https://www.adidas-group.com/en/')
		insert into brand ( brand_id, brand_name , brand_logo ,brand_description , brand_website ) values(3, 'Puma','https://upload.wikimedia.org/wikipedia/commons/thumb/2/20/Adidas_Logo.svg/200px-Adidas_Logo.svg.png', 'Puma SE, branded as Puma, is a German multinational corporation that designs and manufactures athletic and casual footwear, apparel and accessories, which is headquartered in Herzogenaurach, Bavaria, Germany. Puma is the third largest sportswear manufacturer in the world.','https://in.puma.com')
		select * from brand;

		insert into category values(1,'All')
		insert into category values(2,'Outfits')
		insert into category values(3,'Shoes')
		select * from category;
		
		insert into product values(1,1,3,'Nike Sports 100', 'https://image.cnbcfm.com/api/v1/image/105680013-1547583426762nike1.jpg?v=1547583682&w=678&h=381','Very Good Shoes','it is from the manufacturers of one among the best shoes.just take it',799.50,1000.99)
		insert into product values(2,2,3,'Adidas Sports 100', 'https://n1.sdlcdn.com/imgs/i/z/e/Adidas-Gray-Basketball-Shoes-SDL861211348-1-fc9c8.jpg','All Day I Dream About Sports','it is from the manufacturers of one among the best shoes. Stop dreaming about it',11000.50,14000.99)
		insert into product values(3,3,3,'Puma Sports 100', 'https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/puma-hybrid-runner-fusefit-womens-1220-1549051048.jpg','Run the Streets','it is from the manufacturers of one among the best shoes. Shop all the latest gear from the world of Fashion, Sport, and everywhere in between. ',699.50,999.99)
		select * from product;

		insert into stock values(1,1,11,111,GETDATE())
		insert into stock values(2,2,15,159,GETDATE())
		insert into stock values(3,3,0,210,GETDATE())
		select * from Stock;



select * from brand;
select * from category;
select * from product;
select * from Stock;

-------------------------------------------------------------

		create proc fetchCategory
		as
		begin
		select * from category
		end

		exec fetchCategory

		------------------------------

		create proc getProductByProductID
		(@product_id int )
		as
		begin
		select p.product_id, brand_name , category_name ,product_name ,product_images , product_short_decription , product_description , selling_price, remaining_quantity from product p join category c on c.category_id=p.category_id join brand b on p.brand_id=b.brand_id join stock s on p.product_id=s.product_id where p.product_id=@product_id
		end

		exec getProductByProductID 2

		------------------------------
		create proc getProductByKeyword
		(@keyword varchar(255), @category varchar(255))
		as
		begin
			if @category = 'All'
				begin
				select p.product_id, brand_name , category_name ,product_name ,product_images , product_short_decription , product_description , selling_price, remaining_quantity from product p join category c on c.category_id=p.category_id join brand b on p.brand_id=b.brand_id join stock s on p.product_id=s.product_id where @keyword  like '%'+product_name+'%' or  @keyword like '%'+brand_name+'%' or @keyword like '%'+category_name+'%';
				--select product_id, brand_name , category_name ,product_name ,product_images , product_short_decription , product_description , selling_price from product p join category c on c.category_id=p.category_id join brand b on p.brand_id=b.brand_id where product_name like '%'+@keyword+'%' or brand_name like '%'+@keyword+'%' or category_name like '%'+@keyword+'%';
				end
			else
				begin
				select p.product_id, brand_name , category_name ,product_name ,product_images , product_short_decription , product_description , selling_price, remaining_quantity from product p join category c on c.category_id=p.category_id join brand b on p.brand_id=b.brand_id join stock s on p.product_id=s.product_id where( @keyword  like '%'+product_name+'%' or  @keyword like '%'+brand_name+'%' or @keyword like '%'+category_name+'%')  and category_name=@category;
				--select product_id, brand_name , category_name ,product_name ,product_images , product_short_decription , product_description , selling_price from product p join category c on c.category_id=p.category_id join brand b on p.brand_id=b.brand_id where (product_name like '%'+@keyword+'%' or brand_name like '%'+@keyword+'%' or category_name like '%'+@keyword+'%') and category_name=@category; 
				end
		end

		exec getProductByKeyword 'shoes', 'All'		

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

--CUSTOMER

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

		create table customerType
		(customer_type_id int primary key, customer_type_name varchar(255))


		create table address
		(address_id int primary key ,fname varchar(255), lname varchar(255), address varchar(1000), city varchar(255), state varchar(255),pincode varchar(255), active bit default 1 not null)


		create table customer
		(customer_id int primary key identity(1,1), customer_type_id int foreign key references customerType(customer_type_id) ON DELETE set null ON UPDATE CASCADE, password varchar(255), customer_fname varchar(255),customer_lname varchar(255), primary_phone varchar(255), secondary_phone varchar(255), primary_email varchar(255), secondary_email varchar(255), billing_address int foreign key references address(address_id) ON UPDATE CASCADE, shipping_address int foreign key references address(address_id))

		create table otherAddress
		(other_address_id int primary key identity(1,1), customer_id int foreign key references customer(customer_id) ON DELETE CASCADE ON UPDATE CASCADE, address_id int foreign key references address(address_id) )

		---------------------------------------------------------------------------------
	
		insert into customerType values(1,'Guest')
		insert into customerType values(2,'Registered')
		insert into customerType values(3,'Admin')
		select * from customerType

		
		create proc registerCustomer
		( @customer_type_name varchar(255) , @password varchar(255)= NULL, @customer_fname varchar(255),@customer_lname varchar(255), @primary_phone varchar(255), @secondary_phone varchar(255), @primary_email varchar(255), @secondary_email varchar(255),  @address varchar(1000), @city varchar(255), @state varchar(255),@pincode varchar(255))
		as
		begin
			declare @customer_type_id int;
			set @customer_type_id = (select customer_type_id from customerType where customer_type_name=@customer_type_name);
			declare @address_id int;
			set @address_id= (select count(*) from address)+1;
			insert into address values(@address_id, @customer_fname, @customer_lname, @address, @city, @state, @pincode,1);
			insert into customer ( customer_type_id , password , customer_fname ,customer_lname , primary_phone , secondary_phone , primary_email , secondary_email , billing_address ,shipping_address ) values( @customer_type_id , @password , @customer_fname ,@customer_lname , @primary_phone , @secondary_phone , @primary_email , @secondary_email , @address_id , @address_id )
			declare @customer_id int;
			set @customer_id =( select Top 1 customer_id from customer where primary_email=@primary_email and primary_phone=@primary_phone order by customer_id desc);
			insert into otherAddress(customer_id, address_id) values (@customer_id, @address_id)
			select @customer_id 'customer_id', @address_id 'billing_address', @address_id 'shipping_address';
		end

		exec registerCustomer 'Admin','admin','admin', 'admin','9876543210',null,'admin@admin', null,'haha, xxx,yyy','xxx','yyy','zzz'

		---------------------------------------------------------------------------------

select * from customerType
select * from customer
select * from address
select * from otherAddress

--//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	

	--DELETE FROM address WHERE address_id=1
	--delete from customer where customer_id in (2,1,3)
							-----------------------------------------------------------
							-----------------------------------------------------------
							-----------------------------------------------------------
							-----------------------------------------------------------
							---- Disable the constraints on a table called tableName:
							--ALTER TABLE tableName NOCHECK CONSTRAINT ALL

							---- Re-enable the constraints on a table called tableName:
							--ALTER TABLE tableName WITH CHECK CHECK CONSTRAINT ALL
							-----------------------------------------------------------

							---- Disable constraints for all tables:
							--EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'

							---- Re-enable constraints for all tables:
							--EXEC sp_msforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all'
							-----------------------------------------------------------
							-----------------------------------------------------------
							-----------------------------------------------------------
							-----------------------------------------------------------

		-------------------------------------------------------------------------------

		create proc addAddress
		(@customer_id int, @customer_fname varchar(255),@customer_lname varchar(255), @address varchar(1000), @city varchar(255), @state varchar(255),@pincode varchar(255))
		as
		begin
			declare @address_id int;
			set @address_id= (select count(*) from address)+1;
			insert into address values(@address_id, @customer_fname, @customer_lname, @address, @city, @state, @pincode,1);

			insert into otherAddress(customer_id, address_id) values (@customer_id, @address_id)
		end

		exec addAddress 1,'fn','ln','x','y','z','123'

		-------------------------------------------------------------------------------

		create proc removeAddress
		(@address_id int)
		as
		begin
		update address set active=0 where address_id=@address_id;
		--delete from address where address_id=@address_id;
		end

		exec removeAddress 3
		select * from address


		-------------------------------------------------------------------------------

		create proc fetchAvailableAddress
		(@customer_id int)
		as
		begin

		select a.address_id, fname, lname, address,city,state,pincode from address a join otherAddress oa on a.address_id=oa.address_id join customer c on c.customer_id=oa.customer_id where oa.customer_id=@customer_id and active=1

		end

		exec fetchAvailableAddress 1

		-------------------------------------------------------------------------------

		create proc fetchAddressById
		(@address_id int)
		as
		begin
		select address_id, fname, lname, address,city,state,pincode from address where address_id=@address_id and active=1;
		end

		exec fetchAddressById 1

		select * from address

		---------------------------------------------------------------------------------

		create proc updateShippingAddress
		(@customer_id int, @shipping_address_id int)
		as
		begin
			update customer set shipping_address=@shipping_address_id where customer_id=@customer_id;
		end

		select * from customer
		--//-------------------------------------------------------------------------------

				--create proc verifyLogin
				--(@password varchar(255), @login_email varchar(255))
				--as
				--begin	
				--if exists(select customer_id from customer where primary_email=@login_email and password=@password)
				--	begin
				--	select 1 'status'
				--	end
				--else
				--	begin
				--	select 0 'status'
				--	end
				--end

				--exec verifyLogin 'admin', 'admin@admin.com'


		create proc verifyLogin
		(@password varchar(255), @login_email varchar(255))
		as
		begin	
		select customer_id from customer where primary_email=@login_email and password=@password
		end

		exec verifyLogin 'admin', 'admin@admin'

		---------------------------------------------------------------------------------

		create proc verifyRegisteredEmail
		( @login_email varchar(255))
		as
		begin
			if exists(select customer_id from customer where primary_email= @login_email and customer_type_id in(2,3))
				begin
				select 1 'status'
				end
			else
				begin
				select 0 'status'
				end
		end

		exec verifyRegisteredEmail 'admin@admin'
		--//-------------------------------------------------------------------------------

				--select count(*) (select customer_id from customer where primary_email='admin@admin.com' and password='admin')


				--@customer_type_name  , @password , @customer_fname ,@customer_lname , @primary_phone , @secondary_phone , @primary_email , @secondary_email ,  @address , @city , @state ,@pincode 

		---------------------------------------------------------------------------------

		create proc getCustomerById
		(@customer_id int)
		as
		begin
		select customer_type_name, customer_fname,customer_lname,primary_phone,secondary_phone,primary_email,secondary_email,address,city,state,pincode, shipping_address,billing_address  from customer c join customerType ct on c.customer_type_id= ct.customer_type_id join address a on a.address_id=c.shipping_address where customer_id= @customer_id;
		end

		exec getCustomerById '1'


----------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

--Orders 

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

		create table paymentMode
		(payment_mode_id int primary key identity(1,1), payment_mode_name varchar(255))

		insert into paymentMode values ('Online')
		insert into paymentMode values ('UPI')
		insert into paymentMode values ('COD')

		create table paymentStatus
		(payment_status_id int primary key identity(1,1), payment_status_name varchar(255))

		insert into paymentStatus values('Successful')
		insert into paymentStatus values('Failure')
		insert into paymentStatus values('Pending')


		create table orders
		(order_id int primary key identity(1,1), customer_id int references customer(customer_id),payment_status_id int references paymentStatus(payment_status_id), payment_mode_id int references paymentMode(payment_mode_id), payment_reference varchar(255), payment_amount decimal(10,2),order_date datetime,  shipped_date datetime, expected_delivery_date datetime)

		create table orderStatus
		(order_status_id int primary key identity(1,1), order_status_name varchar(255))

		insert into orderStatus values ('Order Placed')
		insert into orderStatus values ('Waiting to be Dispatched')
		insert into orderStatus values ('Shipped')
		insert into orderStatus values ('Delivery Pending')
		insert into orderStatus values ('Delivered')


		create table orderItem
		(order_item_id int , order_id int references orders(order_id),constraint pk1 primary key(order_id,order_item_id), product_id int references product(product_id),quantity int, list_price decimal(10,2), discount decimal(10,2),order_status_id int references orderStatus(order_status_id)) 

		----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

		create proc addOrder
		(@customer_id int, @payment_status_name varchar(255), @payment_mode_name varchar(255),@payment_reference varchar(255),@payment_amount decimal(10,2),@order_date datetime, @shipped_date datetime,@expected_delivery_date datetime )
		as
		begin
		--to fill orders table
			declare @payment_status_id int; select @payment_status_id= payment_status_id from paymentStatus where payment_status_name=@payment_status_name;
			declare @payment_mode_id int; select @payment_mode_id= payment_mode_id from paymentMode where payment_mode_name=@payment_mode_name;
			insert into orders ( customer_id ,payment_status_id , payment_mode_id , payment_reference , payment_amount, order_date , shipped_date , expected_delivery_date )values (@customer_id,@payment_status_id,@payment_mode_id,@payment_reference,@payment_amount,@order_date,@shipped_date,@expected_delivery_date)
		--to return the order_id
			select  order_id from orders where  customer_id = @customer_id and order_date=@order_date;
		end

		declare @now datetime
		set @now=Getdate()
		exec addOrder '1','Successful','UPI','test',1000.33,@now,@now,@now

select * from orders


		--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		create proc addOrderItem
		(@order_id int, @product_id int, @quantity int, @list_price decimal(10,2),@discount decimal(10,2), @order_status_name varchar(255) )
		as
		begin
			declare @order_status_id int; select @order_status_id= order_status_id from orderStatus where order_status_name=@order_status_name;
			declare @order_item_id int; set @order_item_id=(select count(*) from orderItem where order_id=@order_id) +1;
			insert into orderItem (order_id ,order_item_id, product_id ,quantity , list_price , discount ,order_status_id )  values(@order_id ,@order_item_id, @product_id , @quantity , @list_price ,@discount , @order_status_id)
		end

		exec addOrderItem 1,1,1,1000.33,0,'Order Placed'

select * from orderItem

		--//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
				--create proc getAllOrders
				--as
				--begin
				--select * from orders
				--end

				--exec getAllOrders

		------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		create proc getAllOrderItems
		as
		begin
		select order_id, order_item_id, product_name, oi.product_id, quantity, list_price, discount, order_status_name, oi.order_status_id from orderItem oi join product p on oi.product_id=p.product_id join orderStatus os on oi.order_status_id=os.order_status_id 
		end

		exec getAllOrderItems

		------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		create proc getAllOrders
		as
		begin
		select order_id ,o.customer_id, customer_fname+' '+customer_lname 'customer_name',  payment_mode_name,  payment_status_name, payment_reference, payment_amount,order_date, shipped_date, expected_delivery_date from orders o join  paymentStatus ps on o.payment_status_id=ps.payment_status_id join paymentMode pm on o.payment_mode_id = pm.payment_mode_id join customer c on o.customer_id= c.customer_id
		end

		exec getAllOrders

		------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		create proc getOrderItemsById
		(@order_id int)
		as
		begin
		select order_id, order_item_id, product_name, oi.product_id, quantity, list_price, discount, order_status_name, oi.order_status_id from orderItem oi join product p on oi.product_id=p.product_id join orderStatus os on oi.order_status_id=os.order_status_id  where order_id=@order_id;
		end

		exec getOrderItemsById 13

		----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

		create proc getOrdersByCid
		(@customer_id int)
		as
		begin
		select order_id ,o.customer_id, customer_fname+' '+customer_lname 'customer_name',  payment_mode_name,  payment_status_name, payment_reference, payment_amount,order_date, shipped_date, expected_delivery_date from orders o join  paymentStatus ps on o.payment_status_id=ps.payment_status_id join paymentMode pm on o.payment_mode_id = pm.payment_mode_id join customer c on o.customer_id= c.customer_id where o.customer_id=@customer_id
		end

		exec getOrdersByCid 1004
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

--Rest

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		create table wishList
		(wish_id int primary key, customer_id int  references customer(customer_id) on update cascade on delete cascade, product_id int references product(product_id) on update cascade on delete cascade )

		-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

		create proc addToWishList
		( @customer_id int, @product_id int )
		as
		begin
			if exists(select wish_id from wishList where customer_id=@customer_id and product_id=@product_id)
				begin
					select null;
				end
			else
				begin
					declare @wish_id int 
					set @wish_id= (select count(*) from wishList)+1

					insert into wishList values(@wish_id, @customer_id, @product_id)
				end
		end
		----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

		create proc showWishList
		(@customer_id int)
		as
		begin
			select product_id from wishList where customer_id=@customer_id;
		end

select * from wishList
select * from otherAddress
select * from orders
select * from orderItem
select * from customer
select * from address
select * from customerType

select * from brand;
select * from category;
select * from product;
select * from Stock;
