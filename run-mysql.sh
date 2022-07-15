#!/bin/sh -fx

docker-compose -f ./docker-compose-mysql.yaml build
docker-compose -f ./docker-compose-mysql.yaml run mysql_test
