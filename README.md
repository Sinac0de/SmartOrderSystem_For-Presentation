# 📉 SmartOrderSystem: Technical Debt & Static Analysis Demo

![.NET](https://img.shields.io/badge/.NET-10.0-blue.svg)
![SonarQube](https://img.shields.io/badge/SonarQube-Static_Analysis-4E9BCD.svg)
![Architecture](https://img.shields.io/badge/Architecture-Clean%20vs%20Toxic-red.svg)

## 📖 Overview
**SmartOrderSystem** is an educational software engineering project designed to practically demonstrate the accumulation, detection, and resolution of **Technical Debt** and **Design Smells**. 

This repository serves as a live sandbox for **SonarQube Static Code Analysis**, showcasing the transition from a "Toxic Architecture" (Big Ball of Mud) to a "Clean Architecture" using enterprise refactoring patterns.

## 🎓 Academic Context (Course Concepts)
This project is strictly engineered based on advanced software engineering principles, covering:

1. **Essential vs. Accidental Complexity (Fred Brooks):** Demonstrating how deeply nested procedural code (Arrow Anti-Pattern) exponentially increases accidental complexity without adding business value.
2. **The Four Elements of Simple Design & DRY:** Showcasing catastrophic Code Duplication (Shotgun Surgery risk) and resolving it via behavioral abstractions.
3. **Balanced Coupling & System Entropy:** Highlighting the dangers of high Integration Strength combined with high Distance (Distributed Monolith behavior), and mitigating it via Dependency Inversion.
4. **Abstraction Smells:** Identifying and refactoring "Refused Bequest" and violations of the Liskov Substitution Principle (LSP).
5. **False Positives in Static Analysis:** Demonstrating that Technical Debt management is an interactive human-machine process by intentionally flagging mock test credentials.

---

## 🏗️ Repository Structure & The PR Journey

This repository is best reviewed through its **Pull Requests**. The architectural evolution is divided into two distinct states:

### 🔴 State 1: The Toxic Architecture (`main` branch)
Engineered to trigger catastrophic metrics across all SonarQube dashboards.
* **The God Object:** A single `GodProcessor.cs` handling business rules, database I/O, and external state manipulation (High Cognitive Complexity).
* **Security Blockers:** Hardcoded PostgreSQL credentials, MD5 weak cryptography, and critical SQL Injection vulnerabilities.
* **Anemic Domain Model:** Pure data-clump entities suffering from Primitive Obsession and Magic Numbers.
* **Massive Duplication:** Invoice and tax calculations strictly copy-pasted, driving duplication metrics above 30%.

### 🟢 State 2: The Refactored Architecture (`feature/refactor-architecture` branch)
The resolution phase, bringing SonarQube metrics back to a Grade-A maintainability rating.
* **Strategy Pattern:** Eradicated tax calculation duplication by introducing `ITaxCalculationStrategy`.
* **Rich Domain Model:** Encapsulated state mutations inside the `Order` entity, eliminating Feature Envy.
* **Dependency Injection (IoC):** Broke tight physical coupling by injecting `IDataService`, securing the persistence layer with parameterized Npgsql queries.
* **Interface Segregation:** Resolved the Refused Bequest smell in payment processors by splitting monolithic abstractions.

---

## 📊 SonarQube Execution Guide (Local Setup)
To replicate the static analysis on your local machine using the .NET CLI:

```bash
# 1. Initialize the scanner
dotnet sonarscanner begin /k:"SmartOrderSystem" /d:sonar.host.url="http://localhost:9000" /d:sonar.token="YOUR_TOKEN"

# 2. Build the project forcefully (No-Incremental)
dotnet build SmartOrderSystem.sln --no-incremental

# 3. End analysis and push report
dotnet sonarscanner end /d:sonar.token="YOUR_TOKEN"
