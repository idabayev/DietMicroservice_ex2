# DietMicroservice_ex2
Ex 2 in Cloud Computing course
# To RUN THE EXERCISE
docker-compose up

and if you want to rebuild 
docker-compose up --build

# Meals Microservice
For the Meals part, in Ex1 we used this batch to run the exercise:

```
set LISTETING_PORT=%1
IF NOT DEFINED LISTETING_PORT SET "LISTETING_PORT=8000"
docker build -t meals_api_s_i_m .
docker run -ti -p %LISTETING_PORT%:80 meals_api_s_i_m
```

But we got it wrong in the listening port (which is the second port in the port forwarding) so the last line should be:

```
set FORWARDING_PORT=%1
IF NOT DEFINED FORWARDING_PORT SET "FORWARDING_PORT=8000"
docker build -t meals_api_s_i_m .
docker run -ti -p %FORWARDING_PORT%:8000 meals_api_s_i_m
```

# Mongo DB for the Data
We do need to store the data in a MongoDB server. We should check if we can do it from the c# code of the meals and how. 

# Diet Part
Write a diet microservice that gets GET commands from the meals API, and returns the diet plans that exists in its DB.
It only need to support valid inputs. POST commands gets two possible answers:
- Diet Successfully created
- Diet exist

# Reverse Proxy
Which will recieve only GET requests for dishes, meals, and diet; This server will route the requests to the relevant service

# Docker Compose file 
Write a Docker compose file that will build everything.

# Submission
We need to name the zip file in this format -
Ex2_Shahar.Ehrenhalt_Ida.Bayevsky_Meital.Avitan
