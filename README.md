
# Backend for BMI calculator website

The backend of the BMI website is built on ASP.NET Web API and includes an authentication system that uses a MySQL database to securely store user credentials. The system supports both imperial and metric measurement units to calculate BMI, allowing users to input their height and weight in either system. The backend processes user input, calculates the BMI, and provides the result to the frontend for display. Additionally, the backend handles user account management, including registration and login.




## Features

* User registration - allowing new users to create an account with a unique username and password.

* User authentication - verifying user credentials during login to ensure only authorized users can access the site.

* BMI calculation - allowing users to calculate their BMI using either imperial or metric measurement systems.

* Saving user measurements - storing user measurement data in a database for future reference and tracking.

* Error handling - returning appropriate error messages when user input is invalid or login credentials are incorrect.

* Salted password hashing - generating a unique salt for each user and using SHA256 hashing to secure their password.

* API endpoints - providing RESTful API endpoints for user registration, login, and BMI calculation.
