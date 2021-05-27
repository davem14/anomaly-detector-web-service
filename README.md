# anomaly detector web service

### The purpose:
This web service purpose is to detect anomalies in given data, based on normal data previouosly uploaded.
The web service is accessible for user's browser as well for automated services via http request.

### General operation:
To detect anomalies in client's data, client (user via browser or service via http request) has to upload 'csv' file that contains normal data, and 'csv' file of data to be checked for anomalies (by comparison with the normal data).

'csv' files has to contain features' names.

### The web service's design pattern:
The web service was designed with MVC architecture. The model's name is ??? and is used by all View Model parts. The 'model' notifies each time a new data line is red, and each 'view model' registered to get a notification can retrieve the current line. The model can receive commands using methods it exposes. It can change the inner state by Properties.
In addition to the 'model', each part of the app has 'view model' and 'model'. The 'view model' receives notifications from the 'model' and passes on the needed data to the 'view' by using 'Data Context' and 'binding'.

### Developing tools:
The service's back end was written in C# using ASP.NET Framework platform.
The service's front end was written in JavaScript.

## General structure of the folders:
1. **flight-inspection-app**:
    - Model:
      - file "Flight_Model" - this is the model in the architecture MVVM
    - Controllers:
      - file "VM_Login" -  the view model of the view login.
      - file "VM_PlayBar" - the view model of the view play bar.
      - file "VM_Details" - the view model of the view details.files. correlation_classes, statistics - help with drawing graphs.
    - API:
      - file "login"
      - file "playBar"
      - file "details"
      - file "joystick"
      - file "graph"
      - file "MainWindow"


## Necessary installations to work with the code:
1. ??????

## App installation and using instructions:

### Installation instructions:
???

### Using instructions:
**prior to the app execution**:
???

**after starting the app run:**
1. Enter 'csv' file that contains the flight data that you want to investigate, and also 'xml' file that matches the csv file.
2. Open the app 'FlightGear' and press 'Fly' and wait until the airplane will be displayed, then press 'continue'.
>Note: It's possible running the app without the airplane's display (FlightGear app), but if you choose to do so you will not be able to start the airplane's display during running.



## Link to video for demo of using:
????

## Developed by:
* Yuval Tal
* David Emanuel
* Yaniv Rotics
* Dov Moshe

## Downloads:
* FlightGear
???
