## comments on AnomalyDetectorController:

**This file can be invoked in two ways:**
1. from the index.html form at the wwwroot directory when the user
    press the submit button then a post http request to `/localhost:8080/detect`
    is issued, and as a result the part of code that starts with 
    `[HttpPost("detect")]` is invoked. 
2. when the user issues a post `/localhost:8080/` request using any software.
    In this case the part of code that starts with `[HttpPost]` is invoked.

**No matter whether the code is invoked using option 1 or option 2 
the treatment of both cases is the same and is described below:**
1. The type of model is taken from the html form from the item named
    `"anomlyModels"`. There are exactly two possible values that can
    be in this item: regression or hybrid. The server currently does not
    support other type of models.
2. The name of the CSV train file is taken from the item named 
    `"trainFile"`.
3. The name of the CSV test file is taken from the item named 
    `"testFile"`.
4. An asynchronous task is created to contact the server. This task transfers
    the two files and the type of model to the server, and returns
    a json object which contains the anomaly report. The anomaly reports
    return from the task is then returned to the invoker of this AnomalyDetectorController program. 

**An invoker of this AnomalyDetectorController program should follow the following rules:**
1. There should be an item of an html form behind the invoker with name
    `"anomlyModels"` and this item can have only two possible values:
    regression or hybrid.
2. There should be an item of an html form behind the invoker with name
    `"trainFile"` and the value of this item should be a train CSV
    file. 
3. There should be an item of an html form behind the invoker with name
    `"testFile"` and the value of this item should be a test CSV
    file. 
4. The trainFile and the testFile described in steps 2 and 3 above should
    follow the following rules:
    4.1) The number of columns of trainCSV must be equal to the number of columns of testCSV.
    4.2) The number of rows of trainCSV must be equal to the number of rows of testCSV.
    4.3) The first row of trainCSV file must be equal the first row of testVSV file.  The first row in each           
    file contains the feature names and therefore the equality is necessary since both files                     
    represents the same features. 
    4.4) In each file the first row does not contain repeated features names. This is necessary since each            
    column represents a different feature, and the first row of each file represent the feature            
    names. 

**The format of the json object returned by this program is as described by the following example:**
```js
{"reports":{"A":[{"start":50,"end":50,"withFeature":"B"}],"C":[{"start":86,"end":86,"withFeature":"D"}]}}
```

The json object start with the string "reports: followed by a list of anomalies for each of the
features. In the example above the list for feature A consists of one element, which describes
the line on which the anomaly starts, the line at which it ends and the reason for the anomaly. 
In the example above the anomaly of feature A starts at line 50 and ends at line 50 and the
reason for the anomaly is its correlation with feature B. If there were more anomalies for
feature A on other lines, these anomalies would be seen as more elements in the
list of the anomalies of feature A.
