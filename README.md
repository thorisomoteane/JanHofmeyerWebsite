# Jan Hofmeyer Community Services - Public Website

A community services website built with ASP.NET Core MVC (.NET 8) that showcases programs, events, and services for the Jan Hofmeyer Community Center in South Africa.

## Features

### Public Pages
- **Home** - Overview with statistics and featured projects
- **About** - Organization information, mission, vision, and team
- **Projects** - Active and planned community programs
- **Events** - Upcoming events with registration capability
- **Gallery** - Photo and video gallery of community activities
- **Contact** - Contact forms for inquiries and volunteer applications
- **Review** - Community feedback and testimonials
- **Donate** - Donation information and banking details

### Key Functionality
- Event registration system
- Contact message submission
- Volunteer application processing
- Review submission with moderation
- Azure Blob Storage integration for media files
- Responsive design with Bootstrap 5

## Technology Stack

- **Framework**: ASP.NET Core MVC (.NET 8)
- **Database**: Azure SQL Database
- **Storage**: Azure Blob Storage
- **Frontend**: Bootstrap 5, Font Awesome
- **ORM**: Entity Framework Core
- **Hosting**: Azure App Services

## Prerequisites

- .NET 8 SDK
- Visual Studio 2022
- Azure subscription (for deployment)
- SQL Server (LocalDB for development)

## Installation

1. Clone the repository
   
    "Bash"
    git clone [repository-url] 
    cd JanHofmeyerWebsite
   
3. Update connection strings in appsettings.json:
    
     {
      "ConnectionStrings": {
      "DefaultConnection": "Your-Connection-String"
    },
      "AzureStorage": {
      "ConnectionString": "Your-Storage-Connection-String",
      "ContainerName": "janhofmeyer-media"
    }
  }
     
4. Run database migrations
   
    "Bash"
     dotnet ef database update
   
6. Build and run:

  "Bash"
  dotnet build
  dotnet run


