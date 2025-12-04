using System;
using System.Collections.Generic;
using System.Linq;

namespace TheCactusApp
{ public enum CactusStatus
    {
        Growing,    
        Grown,      
        Withered   
    }
    
    public enum SessionStatus
    {
        Active,      
        Completed,   
        Interrupted  
    }
    
    public abstract class Cactus
    {
        private string name;
        private int growthTime;
        private CactusStatus status;
        private DateTime startTime;
        private DateTime? endTime;
        
        public string Name
        {
            get { return name; }
            protected set { name = value; }
        }

        public int GrowthTime
        {
            get { return growthTime; }
            protected set { growthTime = value; }
        }

        public CactusStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public DateTime? EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
        
        protected Cactus(string cactusName, int growthTimeMinutes)
        {
            this.name = cactusName;
            this.growthTime = growthTimeMinutes;
            this.status = CactusStatus.Growing;
            this.startTime = DateTime.Now;
            this.endTime = null;
        }
        
        public virtual void Grow()
        {
            this.status = CactusStatus.Growing;
        }

        public virtual void Complete()
        {
            this.status = CactusStatus.Grown;
            this.endTime = DateTime.Now;
        }

        public virtual void Wither()
        {
            this.status = CactusStatus.Withered;
            this.endTime = DateTime.Now;
        }
        
        public abstract string Render();
    }
    
    public class BasicCactus : Cactus
    {
        public BasicCactus(int growthTime) : base("Базовый кактус", growthTime)
        {
        }
        
        public override string Render()
        {
            if (Status == CactusStatus.Grown)
            {
                return @"
    🌵
   /|\
  / | \
    |
  __|__
 |_____|";
            }
            else if (Status == CactusStatus.Withered)
            {
                return @"
    💀
   / \
  /   \
    |
  __|__
 |_____|";
            }
            else
            {
                return @"
    🌱
    |
  __|__
 |_____|";
            }
        }
    }
    
    /// Редкий кактус - создаётся при длинных сессиях (60+ минут)
    public class RareCactus : Cactus
    {
        public RareCactus(int growthTime) : base("Редкий кактус", growthTime)
        {
        }

        public override string Render()
        {
            if (Status == CactusStatus.Grown)
            {
                return @"
    🌸
   🌵🌵
  /|\ /|\
 / | X | \
   |   |
  _|___|_
 |_______|";
            }
            else if (Status == CactusStatus.Withered)
            {
                return @"
    ☠️
   💀💀
  / \ / \
    X X
   |   |
  _|___|_
 |_______|";
            }
            else
            {
                return @"
   🌱🌱
    | |
  __|__|__
 |_______|";
            }
        }
    }
    
    //Вдохновляющий кактус (5+ дней подряд)
    public class EventCactus : Cactus
    {
        public EventCactus(int growthTime) : base("Событийный кактус", growthTime)
        {
        }

        public override string Render()
        {
            if (Status == CactusStatus.Grown)
            {
                return @"
    ⭐
   🌵✨
  /|★|\
 / | | \
   |🌟|
  _|___|_
 |_______|";
            }
            else if (Status == CactusStatus.Withered)
            {
                return @"
    💫
   💀✖
  / \ \
    | |
   |___|
  _|___|_
 |_______|";
            }
            else
            {
                return @"
   ✨🌱
    | |
  __|__|__
 |_______|";
            }
        }
    }
    
    public class CactusGarden
    {
        private List<Cactus> cactuses;
        
        public CactusGarden()
        {
            cactuses = new List<Cactus>();
        }
        
        public void AddCactus(Cactus cactus)
        {
            cactuses.Add(cactus);
        }
        
        public List<Cactus> GetAllCactuses()
        {
            return new List<Cactus>(cactuses);
        }
        
        public List<Cactus> GetAliveCactuses()
        {
            List<Cactus> aliveCactuses = new List<Cactus>();
            
            foreach (Cactus cactus in cactuses)
            {
                if (cactus.Status == CactusStatus.Grown)
                {
                    aliveCactuses.Add(cactus);
                }
            }
            
            return aliveCactuses;
        }

        // Метод получения засохших кактусов
        public List<Cactus> GetWitheredCactuses()
        {
            List<Cactus> witheredCactuses = new List<Cactus>();
            
            foreach (Cactus cactus in cactuses)
            {
                if (cactus.Status == CactusStatus.Withered)
                {
                    witheredCactuses.Add(cactus);
                }
            }
            
            return witheredCactuses;
        }
        
        public int TotalCount
        {
            get { return cactuses.Count; }
        }
    }
    
    public class Statistics
    {
        private int totalFocusedMinutes;
        private int successfulSessionsCount;
        private int failedSessionsCount;
        private int longestSessionMinutes;
        private int streakDays;
        private DateTime? lastSessionDate;
        
        public int TotalFocusedMinutes
        {
            get { return totalFocusedMinutes; }
        }

        public int SuccessfulSessionsCount
        {
            get { return successfulSessionsCount; }
        }

        public int FailedSessionsCount
        {
            get { return failedSessionsCount; }
        }

        public int LongestSessionMinutes
        {
            get { return longestSessionMinutes; }
        }

        public int StreakDays
        {
            get { return streakDays; }
        }

        public DateTime? LastSessionDate
        {
            get { return lastSessionDate; }
        }
        
        public Statistics()
        {
            totalFocusedMinutes = 0;
            successfulSessionsCount = 0;
            failedSessionsCount = 0;
            longestSessionMinutes = 0;
            streakDays = 0;
            lastSessionDate = null;
        }
        
        public void UpdateOnSuccess(int duration)
        {
            totalFocusedMinutes += duration;
            successfulSessionsCount++;
            
            if (duration > longestSessionMinutes)
            {
                longestSessionMinutes = duration;
            }

            UpdateStreak();
        }
        
        public void UpdateOnFail()
        {
            failedSessionsCount++;
            UpdateStreak();
        }
        
        private void UpdateStreak()
        {
            if (lastSessionDate == null)
            {
                streakDays = 1;
            }
            else
            {
                DateTime today = DateTime.Now.Date;
                DateTime lastDate = lastSessionDate.Value.Date;
                
                int daysDifference = (today - lastDate).Days;
                
                if (daysDifference == 0)
                {
                    
                }
                else if (daysDifference == 1)
                {
                   
                    streakDays++;
                }
                else
                {
                    
                    streakDays = 1;
                }
            }
            
            lastSessionDate = DateTime.Now;
        }

       
        public void PrintStatistics()
        {
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║            СТАТИСТИКА                  ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine("  Еее! Ты был сфокусирован целых: {0} минут!", totalFocusedMinutes);
            Console.WriteLine("  Успешных сессий: {0}", successfulSessionsCount);
            Console.WriteLine("  Прерванных сессий: {0}", failedSessionsCount);
            Console.WriteLine("  Самая длинная сессия длилась: {0} минут", longestSessionMinutes);
            Console.WriteLine("  Твой streak: {0} дней 🔥", streakDays);
            
            if (lastSessionDate.HasValue)
            {
                Console.WriteLine("  Последняя сессия: {0}", lastSessionDate.Value.ToString("dd.MM.yyyy HH:mm"));
            }
            
            Console.WriteLine("════════════════════════════════════════\n");
        }
    }


    public class FocusSession
    {

        private int durationMinutes;
        private SessionStatus status;
        private DateTime startTime;
        private DateTime? endTime;
        private Cactus assignedCactus;

        // Свойства
        public int DurationMinutes
        {
            get { return durationMinutes; }
        }

        public SessionStatus Status
        {
            get { return status; }
        }

        public DateTime StartTime
        {
            get { return startTime; }
        }

        public DateTime? EndTime
        {
            get { return endTime; }
        }

        public Cactus AssignedCactus
        {
            get { return assignedCactus; }
        }
        
        public FocusSession(int duration, Cactus cactus)
        {
            durationMinutes = duration;
            status = SessionStatus.Active;
            startTime = DateTime.Now;
            endTime = null;
            assignedCactus = cactus;
        }
        
        public void Start()
        {
            status = SessionStatus.Active;
            assignedCactus.Grow();
        }
        
        public void Interrupt()
        {
            status = SessionStatus.Interrupted;
            endTime = DateTime.Now;
            assignedCactus.Wither();
        }
        
        public void Finish()
        {
            status = SessionStatus.Completed;
            endTime = DateTime.Now;
            assignedCactus.Complete();
        }
    }


    public class SessionManager
    {
        private FocusSession currentSession;
        
        public SessionManager()
        {
            currentSession = null;
        }
        
        public bool IsSessionRunning()
        {
            if (currentSession != null && currentSession.Status == SessionStatus.Active)
            {
                return true;
            }
            return false;
        }
        
        public FocusSession StartNewSession(int duration, int streakDays)
        {
            if (IsSessionRunning())
            {
                throw new InvalidOperationException("Сессия уже запущена!");
            }
            
            Cactus cactus;
            
            if (streakDays >= 1)
            {
                cactus = new EventCactus(duration);
            }
            else if (duration >= 60)
            {
                cactus = new RareCactus(duration);
            }
            else
            {
                cactus = new BasicCactus(duration);
            }
            
            currentSession = new FocusSession(duration, cactus);
            currentSession.Start();
            
            return currentSession;
        }
        
        public FocusSession StopSession(bool wasInterrupted)
        {
            if (!IsSessionRunning())
            {
                throw new InvalidOperationException("Нет активной сессии!");
            }
            
            if (wasInterrupted)
            {
                currentSession.Interrupt();
            }
            else
            {
                currentSession.Finish();
            }
            
            FocusSession completedSession = currentSession;
            currentSession = null;
            
            return completedSession;
        }
        
        public FocusSession GetCurrentSession()
        {
            return currentSession;
        }
    }
    
    // ============================================2
    


    public class User
    {
        // Приватные поля
        private string name;
        private CactusGarden cactusGarden;
        private Statistics statistics;

        // Свойства
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public CactusGarden Garden
        {
            get { return cactusGarden; }
        }

        public Statistics UserStatistics
        {
            get { return statistics; }
        }

        // Конструктор
        public User(string userName)
        {
            name = userName;
            cactusGarden = new CactusGarden();
            statistics = new Statistics();
        }

        // Метод добавления кактуса в сад
        public void AddCactus(Cactus cactus)
        {
            cactusGarden.AddCactus(cactus);
        }

        // Метод просмотра сада кактусов
        public void ViewGarden()
        {
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║         ТВОЙ КАКТУСОВЫЙ САД            ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            List<Cactus> aliveCactuses = cactusGarden.GetAliveCactuses();
            List<Cactus> witheredCactuses = cactusGarden.GetWitheredCactuses();

  
            Console.WriteLine("┌─ Живых кактусов: {0} ─────────────────", aliveCactuses.Count);
            foreach (Cactus cactus in aliveCactuses)
            {
                Console.WriteLine("\n{0} ({1} мин)", cactus.Name, cactus.GrowthTime);
                Console.WriteLine(cactus.Render());
            }

        
            Console.WriteLine("\n└─ Засохших кактусов: {0} ──────────", witheredCactuses.Count);
            
            int displayCount = 0;
            foreach (Cactus cactus in witheredCactuses)
            {
                if (displayCount >= 3)
                {
                    break;
                }
                
                Console.WriteLine("\n{0} ({1} мин)", cactus.Name, cactus.GrowthTime);
                Console.WriteLine(cactus.Render());
                displayCount++;
            }

            if (witheredCactuses.Count > 3)
            {
                Console.WriteLine("\n... и ещё {0} засохших\n", witheredCactuses.Count - 3);
            }
        }
    }
    

    class Program
    {
        static void Main(string[] args)
        {
            // Настройка кодировки для корректного отображения эмодзи
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║                                        ║");
            Console.WriteLine("║          THE CACTUS                    ║");
            Console.WriteLine("║          Помощь для студентов          ║");
            Console.WriteLine("║                                        ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            
            Console.Write("Введите ваше имя: ");
            string userName = Console.ReadLine();
            
            
            User user = new User(userName);
            SessionManager sessionManager = new SessionManager();

            
            bool isRunning = true;

            while (isRunning)
            {
                ShowMainMenu();
                string userChoice = Console.ReadLine();
                
                switch (userChoice)
                {
                    case "1":
                        StartFocusSession(user, sessionManager);
                        break;
                    case "2":
                        user.ViewGarden();
                        break;
                    case "3":
                        user.UserStatistics.PrintStatistics();
                        break;
                    case "4":
                        Console.WriteLine("\nДо встречи, {0}! Удачи с фокусировкой!\n", user.Name);
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("\nНеверный выбор. Попробуйте снова.\n");
                        break;
                }
            }
        }


        static void ShowMainMenu()
        {
            Console.WriteLine("┌─────────────────────────────────────┐");
            Console.WriteLine("│         ГЛАВНОЕ МЕНЮ                │");
            Console.WriteLine("├─────────────────────────────────────┤");
            Console.WriteLine("│ 1. Начать новую сессию              │");
            Console.WriteLine("│ 2. Посмотреть сад кактусов          │");
            Console.WriteLine("│ 3. Показать статистику              │");
            Console.WriteLine("│ 4. Выход                            │");
            Console.WriteLine("└─────────────────────────────────────┘");
            Console.Write("\nВыберите действие: ");
        }
        
        static void StartFocusSession(User user, SessionManager sessionManager)
        {
            Console.Write("\nВведите длительность сессии (в минутах): ");
            string input = Console.ReadLine();
            
            int duration;
            bool isValidInput = int.TryParse(input, out duration);
            
            if (!isValidInput || duration <= 0)
            {
                Console.WriteLine("Некорректная длительность!");
                return;
            }
            
            FocusSession session = sessionManager.StartNewSession(duration, user.UserStatistics.StreakDays);
            
            Console.WriteLine("\n Начинается сессия на {0} минут...", duration);
            Console.WriteLine("Растёт: {0}", session.AssignedCactus.Name);
            Console.WriteLine(session.AssignedCactus.Render());
            Console.WriteLine("\nEnter - завершение, 'q' - прерывание");
            
            Console.WriteLine("\n  Таймер: {0} минут", duration);
            Console.WriteLine("(Это демо-версия,реальное ожидание не выполняется)");
            
            string userInput = Console.ReadLine();
            bool wasInterrupted = (userInput != null && userInput.ToLower() == "q");
            
            FocusSession completedSession = sessionManager.StopSession(wasInterrupted);
            
            if (wasInterrupted)
            {
                Console.WriteLine("\nСессия прервана! Кактус засох...");
                user.UserStatistics.UpdateOnFail();
            }
            else
            {
                Console.WriteLine("\nСессия завершена успешно! Ура - кактус вырос!");
                user.UserStatistics.UpdateOnSuccess(duration);
            }
            Console.WriteLine(completedSession.AssignedCactus.Render());
            user.AddCactus(completedSession.AssignedCactus);
        }
    }
}
