# BudgetQuest 💰
> **Your Money, Your Quest** — A gamified personal budget tracker for Android.

---

## Module Information

| Field | Detail |
|---|---|
| **Module** | PROG7313 — Programming 3C  |
| **Institution** | The Independent Institute of Education (IIE) — EMERIS |
| **Lecturer** | Uzono Ezekiel |
| **Assessment** | Portfolio of Evidence (POE) — Part 2: App Prototype |
| **Group Members** | ST10404539|
| **Submission** | 29 April 2026 |

---

## 📹 Demonstration Video

**[▶ Watch the Part 2 Demo Video on YouTube](https://youtu.be/REPLACE_WITH_YOUR_LINK)**

> The video demonstrates all Part 2 features with a voiceover walkthrough, running on the Android Emulator (API 34). Duration: approximately 4 minutes.

---

## 📱 Download APK

The latest debug APK is available as a **GitHub Actions artifact**:
1. Go to the [Actions tab](../../actions/workflows/android_ci.yml)
2. Click the most recent successful ✅ workflow run
3. Scroll to **Artifacts** → download `BudgetQuest-...-debug-APK`

---

## App Overview

BudgetQuest is a personal budget tracking Android application built natively in Kotlin. It allows users to register, log in, manage expense categories, log expenses with optional receipt photos, set monthly min/max budget goals per category, and view spending summaries with an animated donut chart — all stored locally using Room (SQLite).

---

## ✅ Features — Part 2

| ID | Feature | Status |
|---|---|---|
| REQ-01 | User Registration & Login (SHA-256 local auth) | ✅ |
| REQ-02 | Category Management (create / edit / delete + colour picker) | ✅ |
| REQ-03 | Expense Entry (amount, date, start/end time, description, category) | ✅ |
| REQ-04 | Receipt Photo (camera + gallery, compressed ≤500 KB) | ✅ |
| REQ-05 | Monthly Budget Goals — total + per-category min/max | ✅ |
| REQ-06 | Expense List with date range filter + photo thumbnail viewer | ✅ |
| REQ-07 | Category Spending Totals with animated donut chart | ✅ |
| REQ-11 | Local Room Database — 4 entities, FK constraints | ✅ |



## 🧪 Automated Testing — GitHub Actions

Workflow: `.github/workflows/android_ci.yml`

Every push/PR to `main`/`master` runs two jobs automatically:

```
Push to main
    │
    ▼
[Job 1: test]           [Job 2: build]  (only runs if tests pass)
./gradlew test    →     ./gradlew clean
                        ./gradlew lint
                        ./gradlew assembleDebug
                              │
                              ▼
                        Upload APK artifact (30-day retention)
```

**28 unit tests across 4 test classes:**

| Test Class | Tests | Covers |
|---|---|---|
| `PasswordUtilsTest` | 8 | SHA-256 hashing, password validation (REQ-01) |
| `DateUtilsTest` | 7 | Month/day boundaries, leap years (REQ-06/07) |
| `BudgetGoalTest` | 6 | Min/max range validation (REQ-05) |
| `ExpenseTest` | 7 | Amount, description, photo URI (REQ-03/04) |

Run locally: `./gradlew test`
Report: `app/build/reports/tests/testDebugUnitTest/index.html`

**References:**
- GitHub Marketplace, 2025. *Automated build Android app with GitHub Action.* Available at: https://github.com/marketplace/actions/automated-build-android-app-with-github-action [Accessed 03 November 2025].
- IMAD5112, 2025. *GitHub Actions build.yml reference.* Available at: https://github.com/IMAD5112/Github-actions/blob/main/.github/workflows/build.yml [Accessed 03 November 2025].

---

## 🏗 Architecture

**Pattern:** MVVM — ViewModel + LiveData + Kotlin Coroutines + Repository  
**Navigation:** Jetpack Navigation Component (single Activity)  
**Local DB:** Room/SQLite — User, Category, Expense, BudgetGoal entities  
**Charts:** MPAndroidChart 3.1.0  
**Images:** Glide 4.16.0 + CameraX

```
data/db/          ← Room DAOs + BudgetQuestDatabase
data/repository/  ← UserRepository, CategoryRepository, ExpenseRepository, BudgetGoalRepository
domain/model/     ← User, Category, Expense, BudgetGoal (Room entities)
ui/auth/          ← LoginFragment, RegisterFragment, AuthViewModel
ui/dashboard/     ← DashboardFragment, DashboardViewModel
ui/expenses/      ← ExpenseListFragment, AddExpenseFragment, PhotoViewerFragment, ExpenseViewModel
ui/categories/    ← CategoriesFragment, CategoryAdapter, CategoryViewModel
ui/totals/        ← TotalsFragment, TotalsAdapter, TotalsViewModel
ui/goals/         ← GoalsFragment, GoalsViewModel
util/             ← SessionManager, PasswordUtils, DateUtils, CurrencyUtils, PhotoUtils
```

---

## 🗄 Database Schema

```
users         id · username(UNIQUE) · email · passwordHash · createdAt
categories    id · userId(FK→users) · name · colorHex · iconName
expenses      id · userId(FK) · categoryId(FK) · amount · description
              date · startTime · endTime · photoUri · createdAt
budget_goals  id · userId(FK) · categoryId(nullable FK) · month · year
              minAmount · maxAmount
```

---

## 🔧 Setup

```bash
git clone https://github.com/YOUR_USERNAME/BudgetQuest.git
```
Open in Android Studio → Gradle sync → Run on Pixel 6 emulator (API 34).
See `SETUP_WINDOWS.md` for Windows-specific instructions.

---

## 📚 References

Android Developers, 2024a. *Room persistence library.* [online] Available at: <https://developer.android.com/training/data-storage/room> [Accessed 15 January 2025].

Android Developers, 2024b. *CameraX overview.* [online] Available at: <https://developer.android.com/training/camerax> [Accessed 15 January 2025].

Android Developers, 2024c. *Kotlin coroutines on Android.* [online] Available at: <https://developer.android.com/kotlin/coroutines> [Accessed 15 January 2025].

Android Developers, 2024d. *Navigation component.* [online] Available at: <https://developer.android.com/guide/navigation> [Accessed 15 January 2025].

Cleevio s.r.o., 2024. *Spendee — product features.* [online] Available at: <https://www.spendee.com/features> [Accessed 12 January 2025].

GitHub Marketplace, 2025. *Automated build Android app with GitHub Action.* [online] Available at: <https://github.com/marketplace/actions/automated-build-android-app-with-github-action> [Accessed 03 November 2025].

Google, 2023. *Material Design 3 guidelines.* [online] Available at: <https://m3.material.io> [Accessed 14 January 2025].

Google, 2024. *Firebase Firestore documentation.* [online] Available at: <https://firebase.google.com/docs/firestore> [Accessed 15 January 2025].

Hamari, J., Koivisto, J. and Sarsa, T., 2014. Does gamification work? In: *Proc. 47th HICSS,* pp.3025–3034.

IMAD5112, 2025. *GitHub Actions build workflow reference.* [online] Available at: <https://github.com/IMAD5112/Github-actions/blob/main/.github/workflows/build.yml> [Accessed 03 November 2025].

JUnit, 2024. *JUnit 4 testing framework.* [online] Available at: <https://junit.org/junit4/> [Accessed 01 April 2026].

Old Mutual, 2024. *22seven rebrands to Vault22.* [online] Available at: <https://www.oldmutual.co.za/news/22seven-rebrands-to-vault22/> [Accessed 15 January 2025].

Philipp, P., 2024. *MPAndroidChart.* [online] GitHub. Available at: <https://github.com/PhilJay/MPAndroidChart> [Accessed 15 January 2025].

South African Revenue Service (SARS), 2024. *Keeping records for tax purposes.* [online] Available at: <https://www.sars.gov.za/individuals/record-keeping> [Accessed 14 January 2025].

Thaler, R.H. and Benartzi, S., 2004. Save More Tomorrow. *Journal of Political Economy,* 112(S1), pp.S164–S187.

Vault22, 2024. *Vault22 — financial fitness platform.* [online] Available at: <https://www.vault22.io> [Accessed 16 January 2025].

---
*© 2026 Group ST10404539 · ST10445158 · ST10440725 · ST10356880 — IIE EMERIS*
