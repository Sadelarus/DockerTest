#!/bin/sh -fx

mkdir -p root-mysql/var/lib/mysql
sudo docker-compose -f ./docker-compose-mysql.yaml build
sudo docker-compose -f ./docker-compose-mysql.yaml run mysql_test
