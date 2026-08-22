# LoanApiSimple

This version follows the same simple style as `RespondentDataTracker`:

- Controllers receive HTTP requests.
- `AccountService` contains registration, login and blocking logic.
- `LoanService` contains all loan rules and database logic.
- `AppDbContext` contains `DbSet<User>` and `DbSet<Loan>`.
- DTOs describe the request bodies.
- JWT protects endpoints and checks the `User` or `Accountant` role.
- Handled errors are also written to `Logs/errors.txt` while the API runs.

## Start the project

1. Open `LoanApiSimple.slnx`.
2. Change the SQL Server name in `LoanApi/appsettings.json` if necessary.
3. Open Package Manager Console and run:

   `Add-Migration InitialCreate`

   `Update-Database`

4. Run the API and open `/swagger`.

Run `dotnet test` to execute the three beginner-friendly loan rule tests in
`LoanApi.Tests/LoanServiceTests.cs`.

## Create an accountant

Register a normal user first. In SQL Server, change that user's `Role` column from
`User` to `Accountant`. Log in again to receive an accountant JWT.

## Simple manual test order

1. Register a user.
2. Login and copy the token.
3. Click Swagger's **Authorize** button and enter `Bearer TOKEN`.
4. Create a loan. Its status is automatically `Pending`.
5. View, update and delete your own pending loan.
6. Login as the accountant.
7. Approve or reject a loan.
8. Confirm the user can no longer update or delete that non-pending loan.
9. Block the user and confirm they cannot create a new loan.
