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
