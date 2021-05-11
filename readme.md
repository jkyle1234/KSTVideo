# KST Video
KST Video is a ASP.NET Web Application using [.NET FRAMEWORK (4.8 Recommended)](https://dotnet.microsoft.com/download/dotnet-framework) that allows users (ApplicationUser) to queue up an order to be sent over to KST Video to checkout rentals.


## Solution Layer

    1. Data This is where we setup our database using Entity Framework
         i.  Video
         ii. Genre
         iii BasketLine
         iv. VideoImage
          v. EmailClient
   
    2. Models The messenger that helps the data and presentation tiers communicate. Examples of services include authentication and authorization
         a. Create
         b. Detail
         c. Edit
         d. ListItems
    3. Services View Models represent the data that we want to show on the page. They are useful because you can pull specific properties from multiple tables
  
    4. API file This is the API part of our application. You might also see this referred to as the presentation tier.
   

## Directions for running the app locally

    ### registering
    1. Run the application, and it should open your browser.(if not make sure webAPI is startup project )
    2. Register a user
    
|   	| Key             	| Value    	| Type   	|
|---	|-----------------	|----------	|--------	|
| 1 	| Email            	| Required 	| string 	|
| 2 	| Password        	| Req      	| str    	|
| 3 	| ConfirmPassword 	| Req      	| str    	|
| 4 	| FirstName           	| Req      	| str    	|
| 5 	| LastName          	| Req      	| str    	|




### Required Key and value 

#### 1. Video
        
| Name      	| Type    	| information 	         |
|-----------	|---------	|------------------------|
| Name 		| string  	| Required               |
| RentalPrice  	| decimal  	| Required               |
| ImageName  	| string	| Required               |
        
#### 2. Genre
| Name  	| Type   	| information                 	         |
|-------	|--------	|----------------------------------------|
| Name		| string 	| Required  	                         |
|       	|        	|                                        |
|       	|        	|                                        |
#### 3. BasketLine
| Name         	| Type    	| information                            |
|--------------	|---------	|----------------------------------------|
| ID         	| integer  	| Required  	                         |
| BasletID     	| integer 	| Required                               |
| VideoID  	| integer 	| Required                               |
| DateAdded   	| datetime  	| Required                               |
       
#### 4. EmailClient
| Name         	| Type    	| information 	|
|--------------	|---------	|-------------	|
| SMTPAddress 	| string	| Required    	|
| FromAddress   | string  	| Required    	|
| Password     	| string  	| Required    	|
| Port    	| int	  	| Required    	|
