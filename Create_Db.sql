---Create database and table
create database if not exists newuser_db;
Use newuser_db;
create table if not exists shops (shop_id int primary key,
                                  name varchar(20) not null);
create table if not exists vendor (vendor_id int primary key,
                                   vendor_part_name varchar(20) not null,
                                   vendor_part_id_full varchar(20) not null);
create table if not exists parts (part_id int primary key,
                                  vendor_part_id int(20) not null,
                                  constraint fk_vendor_type FOREIGN KEY (vendor_part_id) REFERENCES vendor (vendor_id));
create table if not exists prices (part_id_p int not null,
                                   constraint fk_part_type FOREIGN KEY (part_id_p) REFERENCES parts (part_id),
                                   shop_id_p int not null,
                                   constraint fk_shop_type FOREIGN KEY (shop_id_p) REFERENCES shops (shop_id),
                                   price int not null,
                                   amount int not null);

---Create user account to work with database
FLUSH PRIVILEGES;
ALTER USER 'root'@'localhost' IDENTIFIED BY '123';
---GRANT ALL PRIVILEGES ON *.* TO 'root';
CREATE USER IF NOT EXISTS 'newuser' IDENTIFIED BY '123';
ALTER USER 'newuser' IDENTIFIED BY '123';
GRANT All Privileges ON newuser_db.* TO 'newuser';
GRANT All Privileges ON newuser_db.* TO 'newuser'@'%';
