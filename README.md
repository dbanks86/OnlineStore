# OnlineStore

A mock online store implemented using the n-tier architecture pattern

- OnlineStoreModels
  - Contains classes mapped to database using object relational mapping (ORM)
    - Entity Framework

- OnlineStoreDataAccessLayer
  - Perform CRUD operation on database
  
- OnlineStoreServices
  - Call methods of OnlineStoreDataAccessLayer and provides Data Transfer Objects (DTO) that can be used across various user interfaces (UI)
  
- OnlineStoreMVC
  - User interface (UI) that uses the MVC framework of ASP.NET
  
- OnlineStoreWebForms
  - User interface that uses ASP.NET Web Forms
