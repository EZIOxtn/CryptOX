# CryptOX Messenger

CryptOX is a secure messaging application for Windows that uses AES (Advanced Encryption Standard) to ensure highly encrypted communication between users. The encryption key is customized and shared across all users, ensuring secure message exchange. CryptOX consists of two components: the **CryptOX Server** and **CryptOX Messenger** client, communicating via the TCP protocol.

## What does OX stand for?

The "OX" in CryptOX symbolizes **"Optimized Encryption eXchange"**, emphasizing the focus on secure and efficient communication using strong encryption methods.

## Features

- **AES Encrypted Messaging**: Messages are encrypted using AES with a shared, customized key, ensuring privacy and security.
- **Customizable Profiles**: Users can personalize their profile name and picture.
- **Notification System**: Pull notifications for new messages in real-time.
- **TCP Communication**: Secure messaging and notifications are transmitted using the TCP protocol for reliable communication between server and client.

## Project Structure

1. **CryptOX Server**:
   - Manages user authentication, key distribution, and message routing.
   - Handles notifications and profile updates for connected users.
   - Operates over TCP to ensure reliable connections and data transmission.
   
2. **CryptOX Messenger (Client)**:
   - Provides a Windows desktop interface for sending and receiving encrypted messages.
   - Allows users to update their profile name and picture.
   - Retrieves and displays real-time notifications of new messages using pull methods.

## Installation

### Prerequisites

- **Visual Studio** with .NET 6 or higher.
- **Windows 10 or higher** (PC version).
- **TCP Server** setup for hosting the **CryptOX Server**.

### Clone the Repository

```bash
git clone https://github.com/yourusername/CryptOX.git
```

# Server Setup
## Install and Configure the Server:

1. Navigate to the CryptOX-Server directory.
2. Open the solution in Visual Studio.
3. Build the server application.
4. Deploy the server on a machine running Windows and configure it to listen for TCP connections on the desired port.

## Key Distribution:

- The server distributes the shared AES encryption key to all connected users to ensure secure message exchange.

# Client Setup
## Install and Configure CryptOX Messenger:

1. Navigate to the CryptOX-Messenger directory.
2. Open the solution in Visual Studio and build the project.
3. Run the client on Windows. Upon first use, users will be prompted to log in or create an account.

## Profile Customization:

- Users can personalize their profile by setting a custom name and uploading a profile picture.

## Send Encrypted Messages:

- After logging in, users can send and receive AES-encrypted messages through TCP connections.

# How It Works
## Encryption:

- CryptOX uses AES encryption with a shared, customized key for all messages.
- Messages are encrypted on the client-side and transmitted over a secure TCP connection to the server, where they are routed to the appropriate recipient.

## Notifications:

- CryptOX Messenger uses a notification pull system to fetch new messages and alerts from the server, ensuring users are notified in real time.

## Custom Profiles:

- Users can update their profile name and picture, and this information is displayed alongside their messages for easy identification.

# Technologies Used

- **C#**: The primary language for both the server and messenger client.
- **AES Encryption**: Provides strong encryption for all messages.
- **.NET 6**: Framework for building the server and client applications.
- **TCP Protocol**: Ensures reliable, secure connections for message transmission.
- **Windows Forms**: Provides the GUI for the CryptOX Messenger client.

# Future Enhancements

- **Private Chat Rooms**: Support for secure private chat rooms.
- **Key Rotation**: Periodic key updates to enhance security.
- **File Transfer**: Encrypted file sharing between users.
- **Message Archiving**: Store and encrypt past messages securely.

# License

This project is licensed under the MIT License - see the LICENSE file for details.

# Contact

For questions, issues, or contributions, contact me 
