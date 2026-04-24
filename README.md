# 🧠 CyberQuiz

CyberQuiz is a full-stack quiz application built with **ASP.NET Core and Blazor**, designed using a layered architecture to simulate a real-world system.

The application allows users to complete quizzes, track their results, and unlock new content based on performance.

---

## 🚀 Features

- 🔐 User authentication with ASP.NET Identity  
- 📚 Categories and subcategories  
- 🧩 Interactive quiz flow (start → answer → result)  
- 📊 Result tracking per user  
- 🔓 Unlock system (≥ 80% score required to progress)  
- 🤖 AI-based coaching feedback based on quiz results  

---

## 🏗️ Architecture

The project is structured using a **layered architecture**:

```text
CyberQuizGrupp1
│
├── CyberQuizGrupp1        (UI / Blazor)
├── CyberQuizGrupp1.API    (Controllers / Endpoints)
├── CyberQuizGrupp1.BLL    (Business Logic / Services)
├── CyberQuizGrupp1.DAL    (Data Access / EF Core)
├── CyberQuizGrupp1.SHARED (DTOs / Shared Models)
```

## Key principles
Separation of concerns between layers
Thin controllers (logic handled in BLL)
DTO-based communication via SHARED project
Clear flow between UI → API → BLL → DAL

## 🧠 Core Functionality
Quiz Flow
User selects a category
Subcategories are unlocked based on previous results
A quiz attempt is created
User answers questions
Answers are validated and stored
Final result is calculated and saved

## 🔓 Progression System
First subcategory is always available
New subcategories unlock when:
✅ Score ≥ 80%

## 🤖 AI Coaching

The system analyzes user results and generates feedback based on:

- Strengths
- Weaknesses
- Areas to improve

This is handled in the BLL layer, which prepares data and communicates with an AI service.

## 🛠️ Tech Stack
- ASP.NET Core Web API
- Blazor
- Entity Framework Core
- SQL Server
- ASP.NET Identity

## ⚙️ Getting Started
1. Clone the repository
git clone https://github.com/LudvigSHub/CyberQuiz.git

2. Update the database
update-database -Project CyberQuizGrupp1.DAL -StartupProject CyberQuizGrupp1.API

3. Run the application
Set CyberQuizGrupp1 (UI) and CyberQuizGrupp1.API as startup projects
Run both projects simultaneously

## 🎯 Purpose

This project was built to demonstrate:

Layered architecture
Clean separation of concerns
Full-stack development
Realistic application structure
