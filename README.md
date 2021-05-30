# anomaly detector web service

### The purpose:
This web service's purpose is to detect anomalies in given data, based on normal data previouosly uploaded.
The web service is accessible for user's browser as well for automated services via http request.

### General operation:
To detect anomalies in client's data, client (user via browser or service via http request) has to upload 'csv' file that contains normal data, and 'csv' file of data to be checked for anomalies (by comparison with the normal data).

'csv' files has to contain exactly the same features' names (with no repetition) and same amount of rows.

### The web service's design pattern:
The web service was designed with MVC architecture.
The Model responsible for learning the normal data and detecting anomalies in the given data, which its get as an input.
The view is the index.html that is viewed by user's browser, to which the user uploads files and chooses an algorithm. After detection, detected anomoalies is shown.
The Controller is the connector between the Model and the View.
The Controller invokes the Anmoalies Detection process in the Model, and the Model pass detection result to the Controller which then pass it to the View.

### Developing tools:
The service's back end was written in C# using ASP.NET Framework platform.
The service's front end was written in JavaScript.

## General structure of the folders:
- **anomaly-detector-web-service:**
    - Model:
      - contains Anomaly Detectors' classes (Regression and Hybrid).
    - Controllers:
      - file "AnomalyDetectorController" - checks HTTP POST request and pass it to the API, then returns JSON with detected anomalies. [Documentation](documentation/comments_on_AnomalyDetectorController.md)
    - API:
      - Invokes the Anomalies Detection Algorithm in the Model.
    - wwwroot:
      - Contains the static files like 'index.html' and css files.


## Necessary installations to work with the code:
Visual Studio with ASP.NET platform.

## App installation and using instructions:

### Installation instructions:
1. Install the program 'Visual Studio'.
2. Inside the Visual Studio installation, Install the necessary Workload: 'ASP.NET and web development'.


### Using instructions:
1. Clone the Git project.
2. Open the directory 'anomaly-detector-web-service' with Visual Studio.
3. Click on the button 'IIS Express' at the top of the screen.

Now the server is running.

5. Via browser - enter the adress "localhost:8080", choose to 'csv' files (of normal data and to be detected data) and choose the anomalies detection algorithm.
6. Via HTTP requests - send HTTP POST to adress "localhost:8080". request has to contain he desired anomalies detection algorithm and the 2 'csv' files.

## More documentation:
- [Controller](documentation/comments_on_AnomalyDetectorController.md)
- [UML diagram](documentation/UML.png)


## Link to video for demo of using:
https://www.youtube.com/watch?v=-x3X8kHBVfo

## Developed by:
* Yuval Tal
* David Emanuel
* Yaniv Rotics
* Dov Moshe

## Downloads:
https://visualstudio.microsoft.com/
