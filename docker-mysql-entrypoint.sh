#!/bin/sh -fx
echo USER: ${USER}
chown -R ${USER} /var/run/mysqld/ /var/lib/mysql/

####exec sudo -u ${USER} /bin/bash --login

if [ ! -f /var/lib/mysql/.db-ready ]; then
    sudo -u ${USER} mysql_install_db --defaults-file=/etc/mysql/mysql.conf --user=${USER} --datadir=/var/lib/mysql/
    sudo -u ${USER} touch /var/lib/mysql/.db-ready
fi

sudo -u ${USER} \
    /usr/libexec/mysqld \
        --defaults-file=/etc/mysql/mysql.conf   \
        --datadir=/var/lib/mysql/               \
        --socket=/var/run/mysqld/mysqld.socket  \
        --pid-file=/var/run/mysqld/mysqld.pid
