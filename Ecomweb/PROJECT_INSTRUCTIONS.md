Here's the improved `README.md` file, incorporating the new content while maintaining the existing structure and information:


# Ecomweb - E-Commerce Web API Project

A modern ASP.NET Core 6 e-commerce REST API with Next.js frontend, featuring JWT authentication, Google OAuth 2.0, product management, shopping cart, and order processing.

## ?? Quick Start

### Prerequisites
- .NET 6 SDK
- Node.js 16+ (for Next.js frontend)
- SQLite (included with .NET)

### Setup

# Backend
cd Ecomweb
dotnet restore
dotnet ef database update
dotnet run

# Frontend
cd ../nextjs-app
npm install
npm run dev


API runs on `https://localhost:7077`  
Frontend runs on `http://localhost:3000`

---

## ?? Full Documentation

This project is documented in detail in the `PROJECT_INSTRUCTIONS.md` file, which includes:

- **Tech Stack** - All libraries, frameworks, and dependencies
- **Architecture & Design Patterns** - System design and implementation patterns
- **Project Conventions & Best Practices** - Coding standards, naming conventions, error handling, security
- **Database Schema** - Entity relationships, table structures, constraints
- **AI Agent Workflow** - How to work with the project, code review checklist, debugging
- **Feature Roadmap & TODO List** - Completed features, planned features, technical debt

**Start with `PROJECT_INSTRUCTIONS.md` before making any changes to the codebase.**

---

## ?? Tech Stack Overview

### Backend
- **Framework:** ASP.NET Core 6
- **Database:** SQLite + Entity Framework Core 6
- **Authentication:** JWT + Google OAuth 2.0
- **Key Libraries:** 
  - Microsoft.EntityFrameworkCore
  - Microsoft.AspNetCore.Authentication.JwtBearer
  - Google.Apis.Auth
  - System.IdentityModel.Tokens.Jwt

### Frontend
- **Framework:** Next.js (React)
- **Auth:** @react-oauth/google
- **HTTP:** Fetch API

---

## ??? Architecture

The project follows a layered architecture with clear separation of concerns:


Controllers (API Endpoints)
    ?
Services (Business Logic)
    ?
Data Access (EF Core DbContext)
    ?
SQLite Database


Design patterns: Repository (EF Core), Dependency Injection, DTO, Service Locator, Middleware.

---

## ?? Database Schema

### Main Entities
- **Users** - User accounts with JWT/OAuth authentication
- **Products** - Product catalog with images and inventory
- **Carts** - Shopping cart items (temporary)
- **Orders** - Customer orders (persistent)
- **OrderItems** - Line items in orders with historical pricing
- **Categories** - Product categories (partially implemented)

See `PROJECT_INSTRUCTIONS.md` for full ERD and detailed schema.

---

## ?? Authentication

### JWT (Traditional)
1. Register: `POST /api/user`
2. Login: `POST /api/auth/login` with username/password
3. Receive JWT token valid for 30 days
4. Send in `Authorization: Bearer {token}` header

### Google OAuth
1. Frontend performs Google Sign-In (browser)
2. Frontend sends Google ID token to `POST /api/auth/google`
3. Backend verifies token with Google
4. Backend creates/updates local user
5. Backend issues JWT token

---

## ?? API Endpoints

### Products
- `GET /api/product` - List products (search, filter, sort, paginate)
- `GET /api/product/{id}` - Get product detail
- `POST /api/product` - Create product [admin]
- `PUT /api/product/{id}` - Update product [admin]
- `DELETE /api/product/{id}` - Delete product [admin]

### Users
- `POST /api/user` - Register user
- `POST /api/user/admin` - Create admin [public, should restrict]
- `GET /api/user` - List users [authorized]
- `GET /api/user/{id}` - Get user [authorized]
- `PUT /api/user/{id}` - Update user [authorized]
- `DELETE /api/user/{id}` - Delete user [authorized]

### Authentication
- `POST /api/auth/login` - Login with credentials
- `POST /api/auth/google` - Google OAuth sign-in
- `GET /api/auth/me` - Get current user [authorized]

### Cart
- `GET /api/cart/{userId}` - Get user's cart [authorized]
- `POST /api/cart` - Add to cart [authorized]
- `PUT /api/cart/{id}` - Update cart item [authorized]
- `DELETE /api/cart/{id}` - Remove from cart [authorized]

See `PROJECT_INSTRUCTIONS.md` for response formats and detailed documentation.

---

## ? Features

### ? Implemented
- User authentication (JWT + password hashing with HMACSHA512 + salt)
- Google OAuth 2.0 integration (ID token flow)
- Product catalog with search, filtering, and sorting
- Pagination support
- Shopping cart management
- User profile management
- Product image upload
- Role-based access control (user/admin)
- CORS support

### ?? In Development / Planned
See `PROJECT_INSTRUCTIONS.md` ? "Feature Roadmap & TODO List" for comprehensive feature list with priority and status.

Key upcoming features:
- Order creation and management
- Order status tracking
- Product reviews and ratings
- User password reset
- Advanced search
- Email notifications
- Payment integration
- Analytics dashboard
- Docker & CI/CD

---

## ??? Development

### Prerequisites
- Visual Studio 2022 or VS Code
- .NET 6 SDK
- Git

### Running Locally

# Build
dotnet build

# Migrate database
dotnet ef database update

# Run with auto-reload
dotnet watch run

# Create new migration
dotnet ef migrations add MigrationName

# Revert to previous migration
dotnet ef database update PreviousMigrationName


### Configuration
- **JWT Settings:** `appsettings.json` ? `Jwt` section
- **Google OAuth:** `appsettings.json` ? `Google` section
- **Database:** `appsettings.json` ? `ConnectionStrings`

**Never commit secrets.** Use `dotnet user-secrets` for development:

dotnet user-secrets init
dotnet user-secrets set "Google:ClientSecret" "your-secret"


---

## ?? Code Standards

### Naming
- **Classes/Methods:** PascalCase
- **Properties:** PascalCase
- **Private fields:** `_camelCase`
- **Local variables:** `camelCase`
- **Constants:** PascalCase or UPPER_SNAKE_CASE

### Practices
- Always use `async/await` for I/O operations
- Use `AsNoTracking()` for read-only queries
- Validate input at controller level
- Use DTOs for API contracts
- Return appropriate HTTP status codes
- Use `[Authorize]` for protected endpoints
- XML comments on public methods
- Avoid hardcoded values (use configuration)

See `PROJECT_INSTRUCTIONS.md` for detailed conventions.

---

## ?? Security Features

- ? Password hashing with HMACSHA512 + salt
- ? JWT-based stateless authentication
- ? Role-based authorization (user/admin)
- ? Google OAuth 2.0 ID token validation
- ? File upload validation (extension checks)
- ? Parameterized queries (EF Core)
- ? CORS configured (allow specific origins in production)
- ?? **TODO:** Rate limiting
- ?? **TODO:** Email verification
- ?? **TODO:** Audit logging

---

## ?? Database

**Type:** SQLite (ecom.db)  
**ORM:** Entity Framework Core 6  
**Migrations:** Version-controlled in `Migrations/` folder

### Key Tables
| Table | Purpose |
|-------|---------|
| Users | User accounts & auth |
| Products | Product catalog |
| Carts | Shopping carts (temporary) |
| Orders | Customer orders |
| OrderItems | Order line items |
| Categories | Product categories |

All FK relationships have cascade delete enabled.

---

## ?? Testing

**Current Status:** No automated tests yet.

**Planned:**
- Unit tests for services (JwtGenerator, PasswordHasher)
- Integration tests for controllers (EF Core in-memory DB)
- API tests for all endpoints

---

## ?? Deployment

### Requirements
- .NET 6 runtime on server
- SQLite database (or switch to managed DB like Azure SQL)
- HTTPS certificate
- Environment variables for secrets

### Steps (Future)
- [ ] Docker containerization
- [ ] GitHub Actions CI/CD
- [ ] Cloud deployment (Azure/AWS)
- [ ] Database backup strategy

See `PROJECT_INSTRUCTIONS.md` ? "Phase 8: Deployment & DevOps" for details.

---

## ?? Contributing

Please refer to `PROJECT_INSTRUCTIONS.md` for:
- Code review checklist
- AI agent workflow
- Feature development process
- Debugging guidelines

### Key Points
1. Read relevant sections in `PROJECT_INSTRUCTIONS.md` before starting
2. Follow naming conventions and code style
3. Check the feature roadmap before implementing new features
4. Test locally with `dotnet run` before pushing
5. No secrets in commits (use `dotnet user-secrets`)

---

## ?? Project Status

**Phase:** Active Development  
**Last Updated:** March 6, 2026  
**Maintainers:** Team (AI-assisted)

### High Priority
- [ ] Order creation endpoints
- [ ] Order history & status management
- [ ] Enable Categories feature
- [ ] Reviews & ratings system

### Known Issues
- `baseUrl` hardcoded in ProductController
- Cart doesn't validate inventory
- No email notifications
- CORS allows all origins (restrict in production)

See `PROJECT_INSTRUCTIONS.md` ? "Known Issues / Technical Debt" for more.

---

## ?? Support

For detailed guidance on:
- **Adding features:** See `PROJECT_INSTRUCTIONS.md` ? "AI Agent Workflow"
- **Code standards:** See `PROJECT_INSTRUCTIONS.md` ? "Project Conventions & Best Practices"
- **Database changes:** See `PROJECT_INSTRUCTIONS.md` ? "How to Work with This Project"
- **Feature roadmap:** See `PROJECT_INSTRUCTIONS.md` ? "Feature Roadmap & TODO List"

---

## ?? License

[Add your license here]

---

## ?? Links

- **GitHub:** https://github.com/namphan2599/Ecomweb
- **API Base:** https://localhost:7077
- **Frontend:** http://localhost:3000 (development)
- **Full Instructions:** See `PROJECT_INSTRUCTIONS.md`

