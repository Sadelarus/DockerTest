#!/bin/sh -fx
###exec /bin/bash --login

chown mysql:mysql /var/lib/mysql/
if [ ! -f /var/lib/mysql/.db-ready ]; then
    sudo -u mysql mysql_install_db --user=mysql --datadir=/var/lib/mysql/
    sudo touch /var/lib/mysql/.db-ready
fi

sudo -u mysql mariadbd




