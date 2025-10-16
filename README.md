# 💬 Real-Time Chat System (SignalR + Clean Architecture + CQRS)

## 📘 Overview
This project is a **Real-Time Chat System** built using **ASP.NET Core SignalR**.  
It allows users to send and receive **instant messages** with real-time updates.  
The application follows **Clean Architecture** and the **CQRS (Command Query Responsibility Segregation)** pattern, ensuring a clean, scalable, and maintainable codebase.

Additionally, it includes a **file upload feature** that enables users to share images or documents within the chat.

---

## 🚀 Features
- Real-time messaging using **SignalR**
- One-to-one and group chat support
- File upload and sharing (images, PDFs, etc.)
- Clean Architecture structure
- CQRS pattern (separate commands and queries)
- Entity Framework Core integration
- Scalable and modular design

---

## 🧱 Technologies Used
- **.NET 9**
- **ASP.NET Core Web API**
- **SignalR** for real-time communication
- **Entity Framework Core** for database access
- **CQRS + Mediator Pattern**
- **SQL Server / SQLite** (configurable)

---


## ⚡ SignalR Hub Example
```csharp
public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
```

---

## 📂 File Upload
- Users can upload images or documents to share in chat.
- Files are validated and stored on the server.
- Each uploaded file returns a metadata response (file name, URL, size, etc.).

---

## 🧠 CQRS Example
- **Commands** handle write operations (e.g., SendMessageCommand)
- **Queries** handle read operations (e.g., GetChatHistoryQuery)
- This separation improves scalability and testability.

---

## 🏁 Getting Started
1. Clone the repository  
   ```bash
   git clone https://github.com/AZHAR-GHAFFAR/ChatSystem_Using_SignalR.git
   ```
2. Update ConnectionString Inside the appSetting.json file   
 
3. Run Migration 
   ```
   Add-Migration InitialCreate -StartupProject Project.WebAPI
   ```
3. Update the database  
   ```
   Update-Database -StartupProject Project.WebAPI
   ```
4. Run the application  
   ```
   dotnet run
   ```
5. Open in browser  
   ```
   CHAT BOARD ROUTE
   https://localhost:7202/index.html
   SWAGGER ROUTE
   https://localhost:7202/swagger/index.html
   ```
---

## 🧑‍💻 Author
**Azhar Ghaffar**  
💼 GitHub: [AzharGhaffar](https://github.com/AZHAR-GHAFFAR)

---

## 📄 License
This project is open-source and available under the **MIT License**.
