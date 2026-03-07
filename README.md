# **Currency Number-to-Words Converter**

A full-stack web application that converts numerical currency inputs into their
formal English word equivalents (e.g., `123.45` to
`ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS`).

This project was developed as a technical assessment for TechnologyOne,
prioritizing **unique algorithmic design** and **zero external library
dependencies** for core business logic.

## **Architecture**

- **Frontend:** React with TypeScript (Vite)
- **Backend:** .NET 9 Web API (C\#)
- **AStyling:** Custom Vanilla CSS (Responsive Design)
- **Configuration:** Custom `.env` parsing logic (No external NuGet/NPM packages
  used for logic)

## **Compliance Statement (Requirement 3\)**

In strict accordance with the project instructions, this solution features:

- **No Third-Party NuGet Packages:** All logic, including the environment
  variable loader and the conversion routine, is custom-coded.
- **No CSS Frameworks:** Tailwind CSS was removed in favor of a bespoke Vanilla
  CSS implementation to demonstrate foundational styling skills.
- **Unique Algorithm:** The "Number to Words" logic is an original
  implementation designed to handle specific currency formatting and edge cases.

## **Getting Started**

### **Prerequisites**

- [Node.js](https://nodejs.org/) (v20+)
- [.NET SDK](https://dotnet.microsoft.com/download) (v9.0)

### **1\. Environment Configuration**

The application uses a custom environment variable loader to avoid dependencies
like `DotNetEnv`.

1. Navigate to the `api` and `client` directories.
2. Create a `.env` file in each directory.
3. In the `client/.env`, set `VITE_API_URL=http://localhost:5143` (or your
   specific backend port).
4. In the `api/.env`, set your server configuration as needed.

### **2\. Backend Setup (API)**

```bash
cd api\
dotnet restore\
dotnet run
```

_The server will start at the port defined in your launch settings._

### **3\. Frontend Setup (Client)**

```bash
cd client\
npm install\
npm run dev
```

_Open your browser at the address provided in the terminal._

## **Testing**

This project includes a comprehensive test strategy to ensure
"customer-acceptable" quality.

### **Automated Tests**

To run the C\# xUnit suite for the core conversion algorithm:

```bash
cd api.tests\
dotnet test
```

### **Manual Verification**

1. Enter `123.45` in the UI.
2. **Expected Output:**
   `ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS`
3. Refer to `docs/TestPlan.pdf` for a full list of verified edge cases including
   zero-dollar amounts, cent-only values, and large denominations.

## **Documentation**

Detailed project documentation is provided in the `/docs` directory:

- **`REASONS.md`**: Analysis of the custom algorithm and
  justification for architectural choices.
- **`TEST_PLAN.md`**: Outline of the QA strategy, test cases, and manual
  verification steps.
- **`Initial_Design_Sketches.pdf`**: Scans of hand-written notes documenting the
  initial logic decomposition.
