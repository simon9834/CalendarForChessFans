
# ♟️ CalendarForChessFans

**CalendarForChessFans** is a console-based C# calendar and planner tailored specifically for chess enthusiasts. It allows users to manage their events while also offering live chess content, thematic features, and flexible filtering.

---

## 🧩 About the Project

This application is a hobby-focused calendar/planner designed around the theme of chess. It combines essential scheduling tools with live thematic content such as recent games, notifications, and user-defined events.

---

## ✨ Key Features

### ✅ Core Functionality

- 📆 Monthly calendar view in console  
- 📝 Add, search, and remove custom events  
- 💾 Local data storage using JSON  
- ♟️ Thematic content: latest chess games via API  
- 🔔 Notifications for upcoming events  
- 🏷️ Filter events by tags (e.g., tournament, training)  
- 🎨 Themed output formatting (Spectre.Console-based)

### 🔓 Optional Enhancements

- 📡 Integration with Chess APIs (e.g., Lichess, Chess.com)  
- 📤 Event export to JSON  
- 🧩 Mini chess widgets (e.g., recent game printer)

---

## 📂 Project Structure

CalendarForChessFans/
├── Controller.cs         // Main logic and routing
├── Event.cs              // Event class with label enum
├── EventStoring.cs       // Load/save JSON events
├── View.cs               // UI rendering logic
├── TxtFormating.cs       // Console effects and animations
├── ChessApi.cs           // Chess API handler
└── data/                 // Saved data

---

## 🛠 Technologies Used

- **C# (.NET 6 or later)**
- **Spectre.Console** for styled terminal interface
- **System.Text.Json** for data storage
- **HttpClient** for API access

---

## 🚀 Getting Started

### 1. Clone the repository

git clone https://github.com/your-username/CalendarForChessFans.git
cd CalendarForChessFans

### 2. Build and Run

dotnet build
dotnet run

---

## 🧭 Commands & Controls

| Command         | Description                                |
|----------------|--------------------------------------------|
| change date    | Change year and month of calendar          |
| day            | Show specific day and its events           |
| create event   | Add a new event to the calendar            |
| find event     | Search for an event by title               |
| find label     | Filter events by tag/label                 |
| remove event   | Delete an event by title                   |
| help           | Show tutorial                              |
| exit           | Save and close the app                     |

---

## 📊 Sample Event (JSON)

{
  "Title": "Chess Club Tournament",
  "Date": "2025-06-12",
  "Label": "Tournament",
  "Description": "Weekly tournament at local chess club"
}

---

## 🌐 API Integration

This project optionally fetches the latest chess game from a public source.

### Example output:

Latest Game: Magnus Carlsen vs Hikaru Nakamura  
Result: 1-0  
Time Control: Blitz (3+2)

Supported APIs:
- Lichess API (https://lichess.org/api)
- Chess.com Public API (https://www.chess.com/news/view/published-data-api)

Note: Some API endpoints may require authentication or have rate limits.

---

## 🔒 Limitations

- No export to PDF/CSV (only JSON export)
- No icon customization for executable
- Works via console only (no graphical interface)

---

## 📈 Future Improvements

- GUI version (WinForms or MAUI)
- Integration with external calendars (Google, Outlook)
- Visual timeline and heatmap calendar
- Daily chess puzzles or training planner

---

## 📜 License

This project is open-source under the MIT License.

---

## 👤 Author

Made with passion for chess and development.  
Author: Your Name / GitHub handle

---

## 🔗 Useful Links

- Spectre.Console (https://spectreconsole.net/)
- Chess.com API Docs (https://www.chess.com/news/view/published-data-api)
- Lichess API Reference (https://lichess.org/api)
