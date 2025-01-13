# Socket IO Implementation in C#

## Description
This project implements a simple **Socket.IO-style architecture** using **C#**. It features a client-server communication system where multiple clients can exchange messages through a central server.

The server broadcasts messages sent by one client to all other connected clients, emulating the core functionality of **Socket.IO**.

---

## Prerequisites
Before running this application, ensure you have the following installed:

1. **.NET SDK 6 or later**
   - [Download .NET SDK](https://dotnet.microsoft.com/download)

2. **Visual Studio Code** or any text editor with C# support.

3. (Optional) **Git**, if you plan to clone this repository.

## How to Run the Application

### 1. Clone the Repository
Clone this repository to your local machine:
```bash
git clone https://github.com/alfadexters/Socket.IO-with-C-.git
cd Socket.IO-with-C-
```

### 2. Set Up the Server

1. Navigate to the server directory:
```bash
cd server
```
2. Restore dependencies and run the server:
```bash
dotnet restore
dotnet run
```
3. The server will listen for connections on port 8080.

### 3. Set Up the Client
1. Open a new terminal and navigate to the client directory
```bash
cd client
```
2. Restore dependencies and run the client:
```bash
dotnet restore
dotnet run
```
3. The client will connect to the server at 127.0.0.1:8080 (localhost by default).
4. Open additional terminals and repeat the above steps to run multiple client instances.

---

## How It Works
### Server
- Listens for incoming connections on port `8080`.
- Manages multiple clients simultaneously using threads.
- Broadcasts received messages to all other connected clients.

### Client
- Connects to the server.
- Sends messages to the server.
- Receives messages broadcast by the server from other clients.

## Technologies Used
- **C#**: Programming language.
- **.NET Core**: Framework for building console applications.
- **Sockets**: Network communication for client-server interaction.
## Example Output

### Server:
```plaintext
Server is running on port 8080...
New client connected!
Received: Hello from Client 1
Received: Hi from Client 2
```
### Client 1:
```plaintext
Connecting to server...
Connected to server!
Hello from Client 1
Message from server: Hi from Client 2
```
### Client 2:
```plaintext
Connecting to server...
Connected to server!
Hi from Client 2
Message from server: Hello from Client 1
```
---

## Testing
### 1. Multiple Clients
Connect several clients to the server and verify:
- Messages sent by one client are received by all other connected clients.
- The server logs each connection and message received.

### 2. Client Disconnection
Close one or more clients and ensure the server continues to function for the remaining connected clients.





