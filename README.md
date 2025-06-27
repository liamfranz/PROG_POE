# Cybersecurity Chat Assistant

A WPF-based interactive application that educates users about cybersecurity through a chat interface, task reminders, a quiz mini-game, and an activity log. This assistant is designed to help users learn good cybersecurity practices while providing a friendly, educational, and engaging experience.

---

## 🧠 Features

### 💬 Chat Assistant
- Responds to user input using basic keyword-based NLP.
- Offers cybersecurity tips related to passwords, phishing, scams, and privacy.
- Provides empathetic replies for emotional cues (e.g., "worried", "curious").

### 📝 Task Assistant
- Allows users to create custom tasks with optional reminders.
- Supports natural language commands (e.g., "remind me in 2 days").
- Displays task list with title, description, and reminder date.

### 🎯 Cybersecurity Quiz
- 10 multiple-choice questions on basic cybersecurity awareness.
- Instant feedback after each answer with explanations.
- Tracks user score and shows total upon completion.

### 📜 Activity Log
- Maintains a log of the 10 most recent actions taken within the app.
- Automatically logs actions like task creation, quiz attempts, and chat triggers.

---

## 🛠️ How It Works

### Intent Detection
User messages are parsed for intent using keyword detection to trigger the appropriate feature:

- **Tasks**: `"add task"`, `"remind me"`, `"create task"`
- **Quiz**: `"start quiz"`, `"challenge me"`, `"security questions"`
- **Log**: `"show activity"`, `"log"`, `"history"`

If none match, the assistant replies with a relevant cybersecurity response based on keywords (e.g., `"password"`, `"phishing"`).

### Reminder Parsing
Supports:
- `today`
- `tomorrow`
- `in X days` (e.g., “remind me in 3 days”)

---

## 🧩 Structure

### `MainWindow.xaml.cs`
Main WPF code-behind file. Key components:
- `InitializeQuiz()`: Loads quiz questions.
- `AddTask()`: Adds a task with optional reminder.
- `SendButton_Click()`: Main entry for chat/command input.
- `DetectIntent()`: Determines which feature the input triggers.
- `LogAction()`: Updates the activity log list.
- `ParseReminderDate()`: Converts user input into a `DateTime?`.

### Classes
- **`CyberTask`**: Represents a task with title, description, and optional reminder.
- **`QuizQuestion`**: Stores a quiz question, its options, correct index, and explanation.
- **`CybersecurityBot`**: Handles basic NLP and randomized keyword responses.

---

## 🧪 Sample Quiz Topics

1. Password security
2. Phishing identification
3. Public Wi-Fi safety
4. Two-factor authentication
5. Social engineering
6. Malware awareness

---

## 🚀 Getting Started

### Requirements
- Windows OS
- .NET Desktop Runtime
- Visual Studio or any IDE that supports WPF (.NET Core or .NET Framework)

### Build and Run
1. Clone or download the project.
2. Open the `.sln` file in Visual Studio.
3. Build the project.
4. Run the application.

---

## 📌 Usage Examples

**Chat:**  
- "Remind me to update antivirus in 3 days" → Adds a task with a reminder.  
- "Start quiz" → Begins cybersecurity quiz.  
- "What is phishing?" → Returns educational info about phishing.

---

## 🔐 Educational Goal

This app is ideal for:
- Beginners learning about cybersecurity.
- Classroom use in digital literacy courses.
- Home users looking to improve cyber hygiene.

---


