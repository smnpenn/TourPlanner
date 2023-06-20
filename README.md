# TourPlanner
This is our Semester project for SWEN2 at the FH Technikum Wien

# Requirements

- Docker Desktop


# Installation

The first step needed to get ElasticSearch running is to set `vm.max_map_count=262144` in order to get ElasticSearch running properly.

1.) Open CMD in Windows

2.) Type the following: `wsl -d docker-desktop`

3.) Then type: `sysctl -w vm.max_map_count=262144`

Now we can use the docker-compose file to setup our containers for the application.

# Docker Compose setup

1.) Run `docker compose up` in the location where the compose file is located.

# Credentials for DBConfig
When starting the ElasticSearch-Container for the first time, you will get a window which displays the password and the HTTP CA certificate:

![image](https://github.com/smnpenn/TourPlanner/assets/80070874/9a0ec9ac-768d-451b-b5d5-29689b1e10ab)

These credentials are required so that our application can communicate the ElasticSearch. You need to add your credentials to the dbconfig.json file.

1.) `defaultindex` -> change this to tours-v1

2.) `fingerprint` -> change this to the CA Fingerprint from the ES console in docker

3.) `elasticpassword` -> elastic password from ES console in docker

4.) `elasticuser` -> elastic username from ES console in docker

# Setup Mapping Kibana
We need to setup the mapping in Kibana in order to get the FuzzySearch working correctly.


1.) Type `localhost:5601` into your browser of choice. Enter you credentials

2.) Navigate to the DevTools in Kibana

![image](https://github.com/smnpenn/TourPlanner/assets/80070874/7b13d508-412e-4ca8-97ad-8ac94e1aa3ec)



3.) Enter the following in the console to setup the mapping:
![image](https://github.com/smnpenn/TourPlanner/assets/80070874/4dbefba4-dc3c-4acd-b4bb-45ca523f63cb)


4.) The setup is now ready and the application should now be able to use ElasticSearch. 
