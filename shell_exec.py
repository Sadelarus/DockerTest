#!/bin/env python

import subprocess
import json
import sys

def main():
    states = []
    cmd = "docker container ls --format='{{json .}}'"
    containerId = None
    imageName = "mysql_test_img"
    with subprocess.Popen(cmd,
                          stdout=subprocess.PIPE,
                          stderr=None,
                          shell=True) as process:
        output = process.communicate()[0].decode("utf-8")
        for s in output.split("\n"):
            if len(s) > 0:
                states.append(json.loads(s))
    for stat in states:
        if "Image" in stat.keys() and stat["Image"] == imageName:
            containerId = stat["ID"]
            break;
    if containerId is None:
        print("Error: unable to find a running container based on [{}] image\n".format(imageName))
        return 1
    print("Info: running container based on [{}] image: [{}]\n".format(imageName,
                                                                        containerId))
    cmd = ["docker", "exec", "-ti", containerId, "/usr/local/bin/Create_Db.sh"]
    print("Run: {}\n".format(cmd))
    res = subprocess.run(cmd, capture_output=False)
    print(res)
    return 0

if __name__ == "__main__":
    sys.exit(main())

