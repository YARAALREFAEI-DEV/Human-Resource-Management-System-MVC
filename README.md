# HRMS - Human Resource Management System

This is a full-featured Human Resource Management System (HRMS) built using ASP.NET MVC framework. 
The project is designed to handle all core HR operations including employee management,leave requests, role-based access, Payroll system and more.

🔷 Project Overview

This project uses the ASP.NET MVC architecture and is divided into multiple Areas for better separation of concerns:

- Admin Area: For managing system-wide configurations, departments, roles, and user access.
- Employee Area: For employees to view and update their profile, apply for leave, and resignation .

- Key Features

- Role-based login and dashboard (Admin / Employee)
- Employee profile management
- Leave request and approval system
- Payroll system management
- Department & designation management
- User authentication using ASP.NET Identity
- Clean and responsive Bootstrap UI
- Organized using MVC Areas and ViewModels

 🔷Technologies Used

- ASP.NET MVC 
- C#
- SQL Server
- Entity Framework
- ASP.NET Identity
- Bootstrap (for UI)
- HTML, CSS, JavaScript, jQuery
- Visual Studio

**Project Structure**

- `Areas/Admin`: All admin-side logic, views, controllers
- `Areas/Employee`: All employee-side functionality
- `Models`: Database models and ViewModels
- `Controllers`: Main controllers (outside areas)
- `Views`: Shared views and layouts
- `App_Data`: Database 

## Roles & Access

- Admin: Full access to manage employees, departments, and Payroll system 
- Employee: Limited access to view and edit their profile, apply for leave, resignation ,grivance.


