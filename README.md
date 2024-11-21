# Contract Monthly Claim System (ContractMonthlyClaimSys)

The **Contract Monthly Claim System** is a web-based application built using .NET Core MVC to streamline the process of submitting, approving, and managing monthly claims for Independent Contractor (IC) lecturers. The system provides an intuitive interface for lecturers to submit their claims, upload supporting documents, and track the status of their claims. Additionally, it offers an efficient approval process for Programme Coordinators and Academic Managers to review and approve or reject claims.

## Table of Contents

- [Project Overview](#project-overview)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Running the Application](#running-the-application)
- [Project Structure](#project-structure)


## Project Overview

The **Contract Monthly Claim System** (CMCS) aims to enhance the efficiency and accuracy of claim management for IC lecturers. The system uses the Model-View-Controller (MVC) architecture to separate concerns and promote maintainability. It is designed with modern UI elements, providing a user-friendly experience for all stakeholders.

## Features

- **Submit Claims**: Lecturers can submit their claims with details such as hours worked and hourly rate.
- **Upload Documents**: Lecturers can upload supporting documents (e.g., timesheets, contracts) for their claims.
- **Check Claim Status**: Lecturers can track the status of their submitted claims.
- **Approve Claims**: Programme Coordinators and Academic Managers can view, approve, or reject claims.
- **Modern UI Design**: Uses Tailwind CSS and Google Fonts for a clean and responsive design.

## Technologies Used

- **.NET Core MVC**: For building the web application using the Model-View-Controller pattern.
- **C#**: The primary programming language used for backend logic.
- **HTML/CSS**: For structuring and styling the front-end UI.
- **Tailwind CSS**: A utility-first CSS framework for designing the UI.
- **Google Fonts**: For custom typography.

## Getting Started

### Prerequisites

Before you begin, ensure you have the following installed:

- **Visual Studio 2019/2022** or later with the **ASP.NET and web development** workload.
- **.NET Core SDK** (3.1 or later).

### Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/NevilleKabamba/ST10404539-PROG6212-PART-1.git
   cd ContractMonthlyClaimSys

2. **Open the project in Visual Studio**:

Launch Visual Studio.
Click on File > Open > Project/Solution.
Navigate to the cloned directory and select the ContractMonthlyClaimSys.sln file.

3. **Restore NuGet Packages**:

In Visual Studio, go to Tools > NuGet Package Manager > Manage NuGet Packages for Solution.
Click Restore to install all the required dependencies.

### Running the Application
Build the Project:

Click on Build > Build Solution or press Ctrl + Shift + B to build the project.
Run the Application:

Press F5 or click on Run to start the application.
The application should open in your default web browser. Use the navigation menu to explore the different functionalities.


### Project Structure

The project follows a typical .NET Core MVC structure:

Controllers/: Contains the controller files that handle user requests and application logic.

HomeController.cs: Manages the home page views.
LecturerController.cs: Manages the views related to lecturer actions like submitting claims, uploading documents, and checking status.
ApprovalController.cs: Manages the views related to approving or rejecting claims.
Views/: Contains the Razor view files that define the UI of the application.

Shared/_Layout.cshtml: The main layout file used by all views for consistent styling.
Home/Index.cshtml: The home page view.
Lecturer/SubmitClaim.cshtml: View for submitting a claim.
Lecturer/UploadDocuments.cshtml: View for uploading documents.
Lecturer/CheckStatus.cshtml: View for checking claim status.
Approval/ApproveClaims.cshtml: View for approving or rejecting claims.
wwwroot/: Contains static files like CSS, JavaScript, and images.