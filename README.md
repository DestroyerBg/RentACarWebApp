# 🚗 RentACarWebApp

ASP.NET Core MVC application for renting cars with user authentication, booking system, reviews, and an admin panel.

---

## 📸 Screenshots

*Coming soon: UI previews of the homepage, booking form, and admin dashboard.*

---

## 🔧 Technologies used

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core
- Identity
- SQL Server
- Bootstrap 5

---

## 🧑‍💻 Features

- 🔐 User registration and login with roles (User/Admin)
- 🚘 Browse and rent available cars
- 📅 Create and view reservations
- 💬 Leave reviews for cars
- 🛠️ Admin panel: manage cars, users, and bookings

---

## 🚀 Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/DestroyerBg/RentACarWebApp.git
cd RentACarWebApp
```

### 2. Configure `appsettings.json`

Add your database connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "your_connection_string"
}
```

### 3. Apply migrations and run

> ℹ️ The application is configured to **automatically apply any pending EF Core migrations on startup**.

You can still apply them manually if needed:

```bash
dotnet ef database update
```

Then run the application:

```bash
dotnet run
```

---

## 📋 Roadmap

- [ ] Stripe integration for payments
- [ ] Filtering and search for cars
- [ ] REST API for mobile apps
- [ ] UI/UX improvements

---

## 🤝 Contributing

Pull Requests are welcome! For major changes, please open an issue first.

---

## 📄 License

MIT – Use, improve, share. Just don't forget to give credit. 😎

---

![.NET](https://img.shields.io/badge/.NET%20Core-8.0-blue)
![License](https://img.shields.io/badge/license-MIT-green)