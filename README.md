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



Expand the Time functionality of your application by implementing a simplified billing process. The requirements are:

The application must support the creation of a bill that includes time entries for at least one project (the choice to produce bills for multiple projects at once is up to your discretion).
You must use a Bill model class to support this functionality, but the only requirements for that bill model is that it includes:
A TotalAmount property that shows the amount of the bill as calculated by multiplying the rate of the employee on a time entry by the number of hours on that time entry and then summing all such time entries on the project(s).
A DueDate property that shows the date the bill is due
You must have the ability to show all bills for a project when showing the details of a project.
You must have the ability to show all bills for all projects on a client when showing the details of a client.
