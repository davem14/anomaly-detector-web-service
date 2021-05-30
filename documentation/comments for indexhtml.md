## index.html comments:

This file is invoked when the user write localhost:8080 in his browser.
As a result, the ASP system activates this index.html file which is on the `'wwwroot'` directory. 

This file presents a form for the user to select two CSV files and a model type. 

One CSV file, called trainFile contains data with no anomalies. 
The other CSV file, called testFile contains data with possible anomalies.
The trainFile will be used by the server to train the model and the testFile will be used
by the server to test the model and report anomalies if exists. The reported anomalies of the
server will be presented at the item in this file with name "ad".

**The train and the test files that the user select must fulfil the following rules:**
1. The number of columns of trainCSV must be equal to the number of columns of testCSV.
2. The number of rows of trainCSV must be equal to the number of rows of testCSV.
3. The first row of trainCSV file must be equal the first row of testVSV file. The first row in each file contains
    the feature names and therefore the equality is necessary since both files represents the same
    features. 
4. In each file the first row does not contain repeated features names. This is necessary since each column  
    represents a different feature, and the first row of each file represent the feature names. 

The model types of the user select must be regression of hybrid. Currently the server does not support
other types of models. 

When the user selects the submit button an http post request to /locahost:8080/detect is activated an
as a result, control flow pass to the AnomalyDetectorController.cs program which takes the files and the
model type from this index.html form and communicate with the server, and returns the anomaly detection report
as a json file which is parsesed and presented at this index.html file at the item named "ad".
