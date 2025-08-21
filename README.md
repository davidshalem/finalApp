פרויקט MAUI – הזדהות וניהול מוצרים (MVVM + Shell)

אפליקציית .NET MAUI המדגימה הרשמה/התחברות, דף פרופיל, וניהול מוצרים עם פעולות עריכה ו־מחיקה.
הפרויקט בנוי בתבנית MVVM, משתמש ב־Shell לניווט, ב־Dependency Injection להזרקת שירותים ו־ViewModels, וב־SQLite לשמירת נתונים מקומית.

הערה: בגרסה זו קיימות טבלאות Users ו־Products בלבד. בהמשך ניתן להוסיף גם טבלת Orders.

תכונות מרכזיות

🔐 הרשמה והתחברות (סיסמאות מאוחסנות כ־SHA-256 Hash).

👤 דף פרופיל עם ניהול תמונת פרופיל (אופציונלי – MediaPicker).

📦 מוצרים: תצוגת רשימה (CollectionView) ופעולות עריכה/מחיקה ב־Swipe.

🧠 ארכיטקטורה: MVVM מלא + Shell + DI.

🗄️ מסד נתונים מקומי: SQLite (app.db3) דרך Repository Pattern.

⚙️ Async/await מקצה לקצה – ללא חסימות UI.

🎨 Styles בסיסיים בעיצוב (ניתן להרחבה).

מבנה הפרויקט
.
├── App.xaml(.cs)
├── AppShell.xaml(.cs)
├── MauiProgram.cs
├── Models/
│   ├── User.cs
│   └── Product.cs
├── Service/
│   ├── Database.cs
│   ├── SqlUserRepository.cs
│   ├── SqlProductRepository.cs
│   ├── ILoginService.cs
│   ├── IRegisterService.cs
│   ├── SqlLoginService.cs
│   ├── SqlRegisterService.cs
│   ├── IUserSession.cs
│   ├── UserSession.cs
│   ├── ServiceHelper.cs
│   └── DIPageRouteFactory.cs
├── ViewModels/
│   ├── ViewModelBase.cs
│   ├── LoginPageViewModel.cs
│   ├── RegistrationPageViewModel.cs
│   ├── ProfileViewModel.cs
│   ├── ProductsPageViewModel.cs
│   └── ProductEditViewModel.cs
├── Views/
│   ├── LoginPage.xaml(.cs)
│   ├── RegistrationPage.xaml(.cs)
│   ├── ProfilePage.xaml(.cs)
│   ├── ProductsPage.xaml(.cs)
│   └── ProductEditPage.xaml(.cs)
└── Resources/
    └── Styles/Styles.xaml

דרישות מוקדמות

Visual Studio 2022 (מומלץ 17.8+) עם .NET SDK 8.0 ו־MAUI workload מותקנים.

Android SDK/iOS tooling לפי הצורך.

Windows: תמיכה ב־WinUI 3 (ב־Windows 10/11).

בדיקה מהירה:

dotnet --list-sdks
dotnet workload list


התקנה:

dotnet workload install maui

התקנה והרצה
# שכפול המאגר
git clone <repo-url>
cd <repo-folder>

# שחזור חבילות
dotnet restore

# הרצה מתוך Visual Studio (מומלץ) או מקונסולה
dotnet build


קובץ ה־DB (app.db3) נוצר אוטומטית ב־AppDataDirectory.

זרימת שימוש לדוגמה

הרשמה של משתמש חדש → התחברות.

פרופיל – בדיקה שהמשתמש מחובר.

מוצרים – צפייה ברשימה, החלקה שמאלה על פריט → עריכה/מחיקה.

(אופציונלי) הוספה – שימוש באותו מסך עריכה כאשר אין ProductId.

בעיות נפוצות ופתרונות

MissingMethodException (אין בנאי ריק לעמוד)
יש להשתמש ב־DIPageRouteFactory<TPage> ורישום ServiceHelper.Services = app.Services; אחרי builder.Build().

InvalidCastException בהעברת פרמטרים
השתמשו ב־IQueryAttributable ובדקו פרמטרים כ־int/string (לא int? ב־pattern).

Deadlock / UI קפוא
כל פעולות ה־DB אסינכרוניות (async/await), אל תשתמשו ב־.Result/.Wait().

מפת דרכים (Roadmap)

הוספת מסך Orders (בחירת מוצרים מתוך הרשימה וכמות).

שיפורי UX ו־Validation.

בדיקות יחידה ל־Repositories.

הרחבת Styles ו־Theming.
