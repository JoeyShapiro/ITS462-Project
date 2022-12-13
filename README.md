# ITS462-Project

Final project for ITS 462

## Basic Use
This Stack allows for a user to view all devices from scraped sites (Best Buy & Newegg) and display detailed information about them.
The API is a core .NET app that allows for interaction with a database. The client is a Windows Form that allows the user to interact
with this database in a safe and secure way.

## Database
The database is MySQL and is stored in a docker container. It contains one table with the following structure:
```sql
create table devices
(
    id            int auto_increment
        primary key,
    computer_type enum ('desktop', 'laptop', 'tablet', 'phone', 'unspecified') default 'unspecified' not null,
    vendor        varchar(64)                                                                        not null,
    model         varchar(64)                                                                        not null,
    price         double                                                                             not null,
    link          varchar(256)                                                                       not null,
    description   varchar(1024)                                                                      null,
    specs         varchar(1024)                                                                      null,
    constraint id
        unique (id)
);
```
<br>
The database user is only allowed to interact with a few stored procedures. These allow for a safe and standardized way to interact
with the data.

## RESTful Server
The server is written in .NET core. It has the following API Calls that allow for interaction:
```
/api/project/GetDevices - Gets all the devices in the database
/api/project/GetDeviceDetails?id= - Gets all the details (every col of the DB) for a given device id
/api/project/GetFilters - gets all available filters (columns in DB)
/api/project/GetFilteredDevices?filter=&chosen= - given a filter and a chosen search, will get all items that meet these requirements
/api/project/Scrape?super_secret_passphrase= - Given a passphrase, will scrape the websites (bestbuy, newegg) and place it in the DB (CLEARS DB)
```
<br>
It should be noted, that each api call uses stored procedures. It is strongly reccommended that any future changes, should use the 
given procedures, if a new statement is needed, then a new procedure should be created.

## Client
The client allows for an easy way for a user to interact with the server. It should be noted that the user can interact with the API 
directly, or create their own client application.<br>
<br>
Looking at the app and going from top-left to bottom-right:<br>
**Filter**: is a dropdown that selects the `filter` in the API Calls<br>
**Search**: selects `chosen` for the API Calls<br>
**Get Device by Filter**: runs the api call `GetFilteredDevices`<br>
**ID**: Allows to search by ID. It will return a detailed list<br>
**Get All Devices**: retrieves all the devices with no filter<br>
**Print To PDF**: prints the list view to a pdf with details for each item on its own page<br>
**Scrape Data**: allows for scraping the database<br>
<br>
**List View Box**: list of all returned items (id, model, price)
**TextArea**: detailed report of item (everything from its row)