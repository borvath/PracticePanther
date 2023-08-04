# PracticePanther
Client-Project-Employee Management App using .NET MAUI for COP 4870

In this assignment, you will translate your console application into a MAUI application. The basic requirements are:

The application must provide a list of clients.
The application must allow the creation of a client.
The application must allow updating a client.
The application must allow deleting a client.
The application must allow adding a project to a client.
The application must allow updating a project.
The application must allow deleting a project.
Once selected, a client record must show all of the basic information on a client including the projects for that client.
The application must allow a project to be closed
The application must allow a client to be closed, but only once all of the projects on that client have also been closed.
The application must use the MVVM architectural pattern. Submissions that use the Name property of UI elements will not receive credit.

You will also need to create a model class called Employee to store team members. That model class should have the following public properties:
Name
Rate (decimal type is best)
Id

You will also need to create another model class called Time to represent a time entry to explain some work done on a project. That model class should have the following public properties:
Date
Narrative
Hours
ProjectId
EmployeeId

Your UX should also incorporate a way to perform CRUD on central lists of Employee and Time objects. This functionality can be on the main screen of the application or separated into different views.

-------------------------------------------------------------------------------------------------------------------------------------------------------------

Expand the Time functionality of your application by implementing a simplified billing process. The requirements are:

The application must support the creation of a bill that includes time entries for at least one project (the choice to produce bills for multiple projects at once is up to your discretion).
You must use a Bill model class to support this functionality, but the only requirements for that bill model is that it includes:
A TotalAmount property that shows the amount of the bill as calculated by multiplying the rate of the employee on a time entry by the number of hours on that time entry and then summing all such time entries on the project(s).
A DueDate property that shows the date the bill is due
You must have the ability to show all bills for a project when showing the details of a project.
You must have the ability to show all bills for all projects on a client when showing the details of a client.

-------------------------------------------------------------------------------------------------------------------------------------------------------------

Now that we have discussed WebAPI and how to use it to stand up your own REST API, you will build a REST API for one of the basic entities for your application. To get full credit, you must implement the following for either Projects, Clients, Employees, Time Entries (Time), or Bills:

A server-side list that contains the object that were once managed entirely on the client side in programming assignments 1, 2, and 3.
A GET method that returns an object by id
A GET method that returns a list of objects with an optional string filter for searching
A GET or DELETE method that removes an object from the list by Id
A POST method that supports adding new and updating existing objects in the list
Integration of these features into your existing application
To demonstrate functionality, you should provide a video that shows these steps:

Create an object from the client application
Show that the object was added to the server side application via a web browser and your GET method
Show that upon relaunching the client-side application that the object is still there
Show that you can delete an object and that it is deleted on the server
Show that you can update an object and that it is deleted on the server

-------------------------------------------------------------------------------------------------------------------------------------------------------------

For the final project in the course, you will add persistence to your application. The only requirement is that all of the CRUD functionality we have discussed is implemented for at least one of the entities (Client, Project, Time, Employee, Bill) such that when both the client and server-side code is turned off and then back on data retains its state. That is, if I add a client, edit a client, or delete a client, those changes are reflected in the data even after I stop both instances of Visual Studio. For a "light touch" approach you can use Filebase.cs Download Filebase.cs. Alternatively, you can use a database platform like MSSQL, MySql, MongoDB Atlas, or Firebase.

For this assignment you MUST supply a video showing off your work for the semester. If you are unable to finish persistence, that is OK, show me what you have and you will be given partial credit commensurate with the amount you have accomplished.
