using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace CybersecurityAssistant
{
    public partial class MainWindow : Window
    {
        private List<string> activityLog = new List<string>();
        private List<CyberTask> tasks = new List<CyberTask>();
        private List<QuizQuestion> quizQuestions;
        private int currentQuizIndex = 0;
        private int score = 0;

        public MainWindow()
        {
            InitializeComponent();
            InitializeQuiz();
            ShowQuizQuestion();
            DisplayAllQuestions();
        }

        private void LogAction(string action)
        {
            if (activityLog.Count >= 10) activityLog.RemoveAt(0);
            activityLog.Add($"{DateTime.Now:T}: {action}");
            ActivityLogList.ItemsSource = null;
            ActivityLogList.ItemsSource = activityLog;
        }

        private void AddTask(string title, string description, DateTime? reminder = null)
        {
            tasks.Add(new CyberTask { Title = title, Description = description, Reminder = reminder });
            LogAction($"Task added: '{title}' {(reminder.HasValue ? $"(Reminder set for {reminder.Value.ToShortDateString()})" : "")}");
            UpdateTaskList();
        }

        private void UpdateTaskList()
        {
            TaskList.ItemsSource = null;
            TaskList.ItemsSource = tasks.Select(t => $"{t.Title} - {t.Description} {(t.Reminder.HasValue ? $"(Remind on {t.Reminder.Value.ToShortDateString()})" : "")}");
        }

        private void InitializeQuiz()
        {
            quizQuestions = new List<QuizQuestion>
            {
                new QuizQuestion {
                    Question = "What should you do if you receive an email asking for your password?",
                    Options = new List<string> { "Reply with your password", "Delete the email", "Report the email as phishing", "Ignore it" },
                    CorrectIndex = 2,
                    Explanation = "Reporting phishing emails helps prevent scams."
                },
                new QuizQuestion {
                    Question = "Which of the following is a strong password?",
                    Options = new List<string> { "12345678", "password123", "MyDog2023", "Xy!9$k@3#Z" },
                    CorrectIndex = 3,
                    Explanation = "Strong passwords use a mix of characters, symbols, and numbers."
                },
                new QuizQuestion {
                    Question = "What is phishing?",
                    Options = new List<string> { "Fishing online", "Tricking someone into giving personal info", "Using fake money", "Installing antivirus" },
                    CorrectIndex = 1,
                    Explanation = "Phishing is a scam to steal sensitive information."
                },
                new QuizQuestion {
                    Question = "Why should you avoid public Wi-Fi for banking?",
                    Options = new List<string> { "It’s too slow", "It’s expensive", "It can be insecure", "It doesn't allow downloads" },
                    CorrectIndex = 2,
                    Explanation = "Public Wi-Fi can be intercepted by attackers."
                },
                new QuizQuestion {
                    Question = "What is two-factor authentication (2FA)?",
                    Options = new List<string> { "Two passwords", "Password and a second form of verification", "Two users login together", "Fingerprint only" },
                    CorrectIndex = 1,
                    Explanation = "2FA increases security using two types of credentials."
                },
                new QuizQuestion {
                    Question = "Which of these is safe to click?",
                    Options = new List<string> { "Unknown link in email", "Link from a trusted contact", "Pop-up ad", "Link with many misspellings" },
                    CorrectIndex = 1,
                    Explanation = "Only click links from known and trusted sources."
                },
                new QuizQuestion {
                    Question = "What should you do if your password is leaked?",
                    Options = new List<string> { "Ignore it", "Change it immediately", "Tell your friends", "Delete your account" },
                    CorrectIndex = 1,
                    Explanation = "Change it immediately and check for any suspicious activity."
                },
                new QuizQuestion {
                    Question = "Which is an example of social engineering?",
                    Options = new List<string> { "Brute force attack", "Phishing email", "Firewall installation", "Malware scan" },
                    CorrectIndex = 1,
                    Explanation = "Phishing is a common form of social engineering."
                },
                new QuizQuestion {
                    Question = "What’s a safe way to store passwords?",
                    Options = new List<string> { "Write them on paper", "Use a password manager", "Save in email", "Use same password everywhere" },
                    CorrectIndex = 1,
                    Explanation = "Password managers securely store and encrypt your credentials."
                },
                new QuizQuestion {
                    Question = "What is malware?",
                    Options = new List<string> { "Useful software", "Computer update", "Malicious software", "Firewall" },
                    CorrectIndex = 2,
                    Explanation = "Malware is software designed to harm or exploit devices."
                }
            };
        }

        private void ShowQuizQuestion()
        {
            if (currentQuizIndex < quizQuestions.Count)
            {
                var q = quizQuestions[currentQuizIndex];
                QuizQuestionText.Text = q.Question;
                QuizOptions.ItemsSource = q.Options;
                QuizOptions.SelectedIndex = -1; // clear previous selection
            }
            else
            {
                MessageBox.Show($"Quiz complete! Your score: {score}/{quizQuestions.Count}", "Quiz Finished");
                LogAction($"Quiz completed. Score: {score}/{quizQuestions.Count}");
                currentQuizIndex = 0;
                score = 0;
                ShowQuizQuestion();
            }
        }

        private void DisplayAllQuestions()
        {
            if (QuizFullQuestionText != null)
            {
                StringBuilder builder = new StringBuilder();
                int number = 1;
                foreach (var q in quizQuestions)
                {
                    builder.AppendLine($"{number}. {q.Question}");
                    number++;
                }
                QuizFullQuestionText.Text = builder.ToString();
            }
        }

        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            int selected = QuizOptions.SelectedIndex;
            if (selected == -1) return;

            var q = quizQuestions[currentQuizIndex];
            if (selected == q.CorrectIndex)
            {
                score++;
                MessageBox.Show("Correct! " + q.Explanation, "Quiz Feedback");
                LogAction($"Quiz: Correct answer for Q{currentQuizIndex + 1}");
            }
            else
            {
                MessageBox.Show("Wrong. " + q.Explanation, "Quiz Feedback");
                LogAction($"Quiz: Incorrect answer for Q{currentQuizIndex + 1}");
            }

            currentQuizIndex++;
            ShowQuizQuestion();
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = TaskTitleBox.Text.Trim();
            string desc = TaskDescriptionBox.Text.Trim();
            DateTime? reminder = ReminderDatePicker.SelectedDate;

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(desc))
            {
                MessageBox.Show("Please fill in both Task Title and Description.", "Input Required", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            AddTask(title, desc, reminder);

            TaskTitleBox.Clear();
            TaskDescriptionBox.Clear();
            ReminderDatePicker.SelectedDate = null;
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string input = UserInputBox.Text.Trim();
            if (string.IsNullOrEmpty(input)) return;

            string intent = DetectIntent(input);

            switch (intent)
            {
                case "task":
                    {
                        // Extract reminder date if present
                        DateTime? reminderDate = ParseReminderDate(input);
                        AddTask("User Task", input, reminderDate);
                        ChatOutput.Text += $"\nBot: Task added. {(reminderDate.HasValue ? $"Reminder set for {reminderDate.Value.ToShortDateString()}." : "Would you like to set a reminder?")}";
                        LogAction($"Task added via chat input {(reminderDate.HasValue ? $"with reminder {reminderDate.Value.ToShortDateString()}" : "without reminder")}");
                        break;
                    }
                case "quiz":
                    ChatOutput.Text += "\nBot: Starting the quiz...";
                    MainTab.SelectedIndex = 2; // switch to Quiz tab
                    break;
                case "log":
                    MainTab.SelectedIndex = 3; // switch to Activity Log tab
                    break;
                default:
                    {
                        var bot = new CybersecurityBot();
                        string response = bot.RespondToUser(input);
                        ChatOutput.Text += $"\nYou: {input}\nBot: {response}";
                        break;
                    }
            }

            UserInputBox.Clear();
        }

        private string DetectIntent(string input)
        {
            input = input.ToLower();

            string[] taskKeywords = { "add task", "create task", "set reminder", "remind me", "schedule task", "task for" };
            string[] quizKeywords = { "quiz", "test", "challenge me", "start quiz", "play quiz", "security questions" };
            string[] logKeywords = { "log", "show activity", "what have you done", "history", "recent actions" };

            if (taskKeywords.Any(k => input.Contains(k))) return "task";
            if (quizKeywords.Any(k => input.Contains(k))) return "quiz";
            if (logKeywords.Any(k => input.Contains(k))) return "log";

            return "chat";
        }

        private DateTime? ParseReminderDate(string input)
        {
            input = input.ToLower();

            if (input.Contains("tomorrow"))
                return DateTime.Today.AddDays(1);

            if (input.Contains("today"))
                return DateTime.Today;

            // Try to find "in X days"
            int inIndex = input.IndexOf("in ");
            if (inIndex >= 0)
            {
                var afterIn = input.Substring(inIndex + 3);
                var parts = afterIn.Split(' ');
                if (int.TryParse(parts[0], out int days))
                {
                    return DateTime.Today.AddDays(days);
                }
            }

            return null;
        }
    }

    public class CyberTask
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Reminder { get; set; }
    }

    public class QuizQuestion
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public int CorrectIndex { get; set; }
        public string Explanation { get; set; }
    }

    public class CybersecurityBot
    {
        private Dictionary<string, List<string>> keywordResponses = new()
    {
        { "password", new List<string> {
            "Use strong, unique passwords. Consider using a password manager.",
            "Never share your password with anyone, not even tech support.",
            "Make sure your passwords are at least 12 characters and include symbols.",
            "Change your passwords regularly to keep accounts secure."
        }},
        { "scam", new List<string> {
            "Watch out for messages that seem too good to be true.",
            "Scammers often impersonate trusted contacts or companies.",
            "Be wary of urgent requests for money or login info.",
            "Check the sender's email address before responding to messages."
        }},
        { "privacy", new List<string> {
            "Limit what you share on social media—privacy starts with you.",
            "Use private browsing modes when on public computers.",
            "Review app permissions and remove unnecessary access.",
            "Enable two-factor authentication for sensitive accounts."
        }},
        { "phishing", new List<string> {
            "Never click on suspicious links in emails or texts.",
            "Phishing often imitates real companies—look for spelling errors or weird URLs.",
            "If an email asks for personal info, verify the sender before replying.",
            "Report phishing emails to your IT department or provider."
        }}
    };

        private Random random = new Random();

        public string RespondToUser(string userInput)
        {
            userInput = userInput.ToLower();

            foreach (var keyword in keywordResponses.Keys)
            {
                if (userInput.Contains(keyword))
                {
                    var responses = keywordResponses[keyword];
                    return responses[random.Next(responses.Count)];
                }
            }

            if (userInput.Contains("more"))
            {
                // Optional: provide a general randomized tip when the user says "more"
                var allTips = keywordResponses.SelectMany(kvp => kvp.Value).ToList();
                return allTips[random.Next(allTips.Count)];
            }

            if (userInput.Contains("worried") || userInput.Contains("scared"))
                return "It's okay to feel that way. I'm here to help!";
            if (userInput.Contains("curious"))
                return "That's great! Curiosity is the start of cybersecurity awareness!";

            return "I didn’t quite understand that. Could you rephrase?";
        }
    }
}
    