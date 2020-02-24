# WhatToWatch.API
A ASP Net Core 2.1 Web API which serves as the backend service for the WhatToWatch Android Application.

### Motivation
WhatToWatch is a mobile app that is designed to reduce the streamer's difficulty in searching the right movie to watch. Many kinds of research have found that people 20% time searching rather than enjoying the content.

### Tech/framework used
* ASP Net Core 2.1
* AWS Lambda
* <b>Built with</b> - [Visual Studio](https://visualstudio.microsoft.com)

### Project Files

* Controllers\ValuesController - example Web API controller
* Controllers\MovieController - Place where magic happens

### AWS Tips
  #### Note the following will work only with ASP Net Core 2.x and not with the latest 3.x
  * Waiting for an official support for 3.x from AWS.
  
Once you have edited your template and code you can deploy your application using the [Amazon.Lambda.Tools Global Tool](https://github.com/aws/aws-extensions-for-dotnet-cli#aws-lambda-amazonlambdatools) from the command line.

Install Amazon.Lambda.Tools Global Tools if not already installed.
```
    dotnet tool install -g Amazon.Lambda.Tools
```

Deploy application
```
    cd "WhatToWatch.API/src/WhatToWatch.API"
    dotnet lambda deploy-serverless
