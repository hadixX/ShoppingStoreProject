# Shopping Store Project

It's a Ecommerce Web Application that allow users to add multiple items to carts and place the orders. 

## Table of Contents

- [Installation](#installation)
- [Usage](#usage)
- [Unit Test](#unittest)

# Installation

1)- Clone the Repo project.
2)- Navigate to the Project Directory: Move into the directory of the cloned repository: cd ShoppingStore.
3)- open the project using Visual Studio for better usage, and run "dotnet build" in PM Console or in navigation menu Build-> Build Solution.
4)- Go to PM Conslole and run "update-database"
5)- Go To Solution Explorer -> ShoppingStore -> Common -> Extentions -> ServiceExtensions.cs file, make sure to uncomment this line of code:
/* services.AddTransient<DbInitializer>();
  services.BuildServiceProvider().GetService<DbInitializer>().Initialize().Wait();*/
6)- go back to PM Console and run "update-database" and run the project in navigation menu Debug -> start debugging or without debugging.

Note: make sure you do step 4 and 5 before run the project.

# Usage

## Users Role
In this project there are two main roles(Administrator and Customer), the app provide default admin user:
username: admin@example.com
password: Admin123!

## Login and Register
users can login or resgiter if not having an account (by default the account role is customer).
Admin users can create,update,delete and view items. also and view all orders users while customer user can vieew its own orders.

## make an order
1)- make sure to have an items created in items page by login as admin and go to items page and click on create item and fill the form ( for imageURL place a url link for the item photo from any website or google image (its optional) ).
2)- users can add multiaple items to the cart.
3)- after adding items go and click on cart icon to view the users items of choice.
4)- users can remove items from the basket by clicking on red icon button and can see the totla price.
5)- click on place order button to submmit the order.
6)- users can access thier order by clicking on orders menu.

## Filters and pagination
- in home page and items page, users and filter the items list by typing items name or description.
- users can choose page size (5,10,20) items per page.

# Unit Test
-the testing unit it test the itemsController and OrderController. 
-Testing tools are Moq and NUnit.

  
