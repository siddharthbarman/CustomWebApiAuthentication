# Overview
This sample API demonstrates how custom authentication can be implemented for a Web API.
The methods in the WeatherForecastController are protected by the authorization attribute.
A custom authentication scheme is used which checks if the request is authenticated.

# Structure
All security related classes are present in the Security folder. 

## The TokenService class 
Is a helper class which is supposed to be used as a singleton. Its main responsibility is 
to validate a user and password combination and generate a token. This token is stored 
along with an associated authentication-ticket. The authentication ticket contains a 
principal and associated claims. This token is sent to the caller. The caller is then supposed 
to always send this token with every request. The server then checks if the token is valid i.e.
if the token is present in an internal dictionary, retrieves the authentication ticket 
which authorizes the request.   

## TokenAuthenticationHandler class
Is part of the custom authentication scheme. This class is called by the ASP.NET Core 
authentication framework and is regsitered in Startup.cs in the ConfigureServices method. 

## WeatherForecastController class
Has its Get() method protected using the 'Authorize' attribute. 

## AuthenticateController class
Contains only a Post method which expects the username and password to be passed in the 
body. It makes use of the TokenService class. If the username and password are accepted
an authentication-ticket is generated and stored against a token. The token is sent as 
reponse-text to the client. The client is responsible for passing this token with every 
API call. 
