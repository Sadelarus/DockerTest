---Create database and table
create database if not exists newuser_db;
Use newuser_db;
create table if not exists employee (firstname char(20) primary key, lastname char(20), age_int int, your_number_float float, your_number_bigint bigint);

---Create user account to work with database
FLUSH PRIVILEGES;
ALTER USER 'root'@'localhost' IDENTIFIED BY '123';
GRANT ALL PRIVILEGES ON *.* TO 'root';
CREATE USER IF NOT EXISTS 'newuser' IDENTIFIED BY '123';
ALTER USER 'newuser' IDENTIFIED BY '123';
GRANT All Privileges ON newuser_db.* TO 'newuser';
GRANT All Privileges ON newuser_db.* TO 'newuser'@'%';
