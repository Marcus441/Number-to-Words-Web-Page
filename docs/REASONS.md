# **Reasons for Selection & Architectural Analysis**

This document outlines the architectural decisions and algorithmic choices made
for the Currency Number-to-Words Converter.

## **No Dependencies**

In alignment with the technical assessment requirements, this project was built
without external libraries for core business logic.

- Instead of using `DotNetEnv`, I implemented `EnvLoader.cs` to handle
  configuration.
- I pivoted from Tailwind for prototyping to custom CSS to keep dependencies
  lean

## **Algorithm**

The "number to words" conversion uses a triplet-based sliding window**
algorithm.

- The numerical string is split from left to right into groups of three Each
  triplet is processed independently based on their multiple of 10 (Hundreds,
  Tens, Units) and then associated with a scale in the order of 1000 (Thousand,
  Million, etc.).
- This approach is $O(N)$ where $N$ is the number of digits, making it highly
  efficient for the supported range (up to 999 Trillion).
- The algorithm explicitly handles "AND" placement ("ONE MILLION AND FIVE"),
  which is often missed by simpler implementations.
- It manages hyphenation for compound numbers (e.g "TWENTY-THREE") and
  pluralization for currency units (DOLLARS vs DOLLAR).

  ## **3\. Clean Architecture & SOLID**

The backend was refactored into domain-specific namespaces:

- **`api.Services.Numeration`**: Isolates the core math-to-word logic.
- **`api.Services.Validation`**: Implements a "Guard" pattern to ensure the
  service only receives sanitized data.
- Used Primary Constructors to inject interfaces, making the system highly
  testable via Fakes/Mocks. This also keeps any controllers lean.
- Data Transfer Objects are used to ensure the API contract is strictly defined
  and independent of the internal service logic.

  ## **Alternate Solutions & Trade-offs**

During the initial design phase, I evaluated multiple implementation strategies
for constructing the final word string:

- **StringBuilder Approach:** I considered a `StringBuilder`\-focused approach
  to optimize for memory allocations during string concatenation.
- **Final Selection (List-based Join):** I ultimately chose a `List<string>`
  combined with `string.Join` for simplicity and readability.

Given that the output length of a currency conversion is relatively short
(rarely exceeding 30 words), the performance benefits of `StringBuilder` were
negligible compared to the increased complexity of managing delimiters (spaces)
manually. The `List` approach allowed for more elegant handling of "AND"
placement and scale insertions, prioritizing maintainable code over premature
optimization.

## **Frontend Strategy**

The React frontend uses Functional Components and the useState hook. I chose to
manage state locally for the conversion result to keep the UI snappy and avoid
unnecessary global state complexity for a single-purpose utility.

- I consciously chose not to use useEffect, keeping the component logic lean and
  side-effect free. Instead, I utilised only useState to manage the conversion
  results, loading states, and error handling. This ensures a predictable,
  event-driven data flow that is easy to debug.

- While Vite provides a native environment variable parser via import.meta.env,
  it does not perform runtime verification. I implemented a custom validation
  layer in `utils/env.ts` to ensure that critical variables (like VITE_API_URL)
  are not only parsed but strictly verified upon application mount. This
  architectural choice prevents silent failures and malformed URL strings in the
  networking layer.

The UI is fully responsive, ensuring the conversion tool works across desktop
and mobile browsers.
