# anomaly detector web service

### The purpose:
This web service's purpose is to detect anomalies in given data, based on normal data previouosly uploaded.
The web service is accessible for user's browser as well for automated services via http request.

### General operation:
To detect anomalies in client's data, client (user via browser or service via http request) has to upload 'csv' file that contains normal data, and 'csv' file of data to be checked for anomalies (by comparison with the normal data).

'csv' files has to contain features' names.

### The web service's design pattern:
The web service was designed with MVC architecture.
The Model responsible for learning the normal data and detecting anomalies in the given data, which its get as an input.
The view is the service's front end, users upload to it their files, and it shows them the result of the anomalies detection.
The Controller is the connector between the Model and the View.
Files uploaded to the View by user are processed in the Controller to be a valid input for the Model to process, and the Model pass detection result to the Controller which then pass it to the View.

### Developing tools:
The service's back end was written in C# using ASP.NET Framework platform.
The service's front end was written in JavaScript.

## General structure of the folders:
1. **flight-inspection-app**:
    - Model:
      - contains Anomaly Detector's classes.
    - Controllers:
      - file "AnomalyDetectorController" - HTTP requests API ???
    - API:
		????


## Necessary installations to work with the code:
1. ??????

## App installation and using instructions:

### Installation instructions:
???

### Using instructions:

Via browser - enter the adress "localhost:8080", choose to 'csv' files (of normal data and to be detected data) and choose the anomalies detection algorithm.

Via HTTP requests - send HTTP POST to adress "localhost:8080". request has to contain he desired anomalies detection algorithm and the 2 'csv' files.


## Link to video for demo of using:
????

## Developed by:
* Yuval Tal
* David Emanuel
* Yaniv Rotics
* Dov Moshe

## Downloads:
???
