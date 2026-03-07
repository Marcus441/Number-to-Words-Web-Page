# **Test Plan**

## **1\. Objective**

The goal of this test plan is to ensure the "Number to Words" web application
accurately converts numerical inputs into the specified English currency format,
remains stable under edge-case scenarios, and provides a seamless user
experience across different environments.

## **2\. Scope of Testing**

### **2.1 Backend (C\# / .NET Core)**

- Isolated verification of the `NumberToWordsService` algorithm.
- Ensuring the `NumberToWordsValidator`correctly identifies malformed or out of
  range inputs.
- Verifying the `ConverterController` successfully coordinates with services and
  returns the correct DTOs.

### **2.2 Frontend (React / Vite)**

- Testing the "Fail-Fast" mechanism in `env.ts` to ensure the app halts if
  configuration is missing.
- Verifying the `useState` logic correctly handles UI transitions (Loading \-\>
  Success/Error) without side-effect complications.
- Ensuring the UI remains functional on mobile, tablet, and desktop viewports.

## **3\. Test Scenarios**

### **3.1 Core Conversion Logic (Backend)**

Tests conducted using xUnit in the `tests/` project.

| ID    | Input             | Expected Output String                                    | Logic Verified                              |
| ----- | ----------------- | --------------------------------------------------------- | ------------------------------------------- |
| BT-01 | `123.45`          | ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS | Basic conversion and "AND" placement.       |
| BT-02 | `0`               | ZERO DOLLARS AND ZERO CENTS                               | Minimum boundary handling.                  |
| BT-03 | `1.00`            | ONE DOLLAR AND ZERO CENTS                                 | Singular currency unit (DOLLAR).            |
| BT-04 | `1,000,005`       | ONE MILLION AND FIVE DOLLARS AND ZERO CENTS               | Large scale transition with internal zeros. |
| BT-05 | `999999999999.99` | NINE HUNDRED AND NINETY-NINE BILLION...                   | Maximum scale (Trillion boundary).          |

### **3.2 Input Validation & Security**

| ID    | Scenario                         | Expected Result                                        |
| ----- | -------------------------------- | ------------------------------------------------------ |
| VS-01 | Negative input (`-10`)           | `400 Bad Request` \- "Negative numbers not supported." |
| VS-02 | Non-numeric input (`abc`)        | `400 Bad Request` \- "Invalid numerical format."       |
| VS-03 | Over-scale input (1 Quadrillion) | `400 Bad Request` \- "Value exceeds supported range."  |

### **3.3 Frontend Integrity**

| ID    | Scenario               | Expected Result                                                                 |
| ----- | ---------------------- | ------------------------------------------------------------------------------- |
| FT-01 | Missing `VITE_API_URL` | Application displays a clear configuration error on mount.                      |
| FT-02 | Rapid Submission       | Submit button is disabled while `isLoading` is true to prevent race conditions. |
| FT-03 | API Downtime           | UI displays a user-friendly error message from the caught promise.              |

## **4\. Test Environment & Tools**

- xUnit with `FakeNumberToWordsService` for controller isolation.
- `api.http` for manual endpoint verification (executable via Neovim plugins
  like kulala.nvim or rest.nvim).
- Verified on Chrome, Firefox, and Mobile.
- GitHub Actions workflows (`dotnet.yml`, `frontend.yml`) running tests on every
  push.

## **5\. Success Criteria**

1. All xUnit tests in `tests/` pass with 100% success rate.
2. The input "123.45" produces the exact string: "ONE HUNDRED AND TWENTY-THREE
   DOLLARS AND FORTY-FIVE CENTS".
3. The frontend fails immediately and visibly if environment variables are
   missing (verifying the `env.ts` custom parser logic).
4. The application handles the maximum 15-digit trillion range without overflow
   errors.
