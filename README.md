# Prueba Técnica – API RESTful .NET 8

Este proyecto es una implementación de una **API RESTful** como parte de una prueba técnica para desarrollador .NET. Está desarrollado en **.NET 8** y utiliza **Entity Framework Core InMemory**, **JWT** para autenticación, **BCrypt** para encriptación de contraseñas, **FluentValidation** para validación de datos y **Swagger** para documentación.

---

## KEY

"Jwt": {
"Key": "GrupoUniversalKey-----A7d8F9s2KlmP0Qw9Zt4Rv3Xp2Ns6Tg1H"
}

## Tecnologías utilizadas

- **.NET 8**
- **Entity Framework Core InMemory** (almacenamiento temporal en memoria)
- **JWT (JSON Web Tokens)** para autenticación
- **BCrypt.Net** para encriptación de contraseñas
- **FluentValidation** para validaciones de DTOs
- **Swashbuckle/Swagger** para documentación de la API
- **HTTP Client** para consumo de API externa (`https://jsonplaceholder.typicode.com/posts`)

---

## Estructura del proyecto

Backend/
├─ Controllers/ # Endpoints de la API
│ ├─ UserController.cs
│ ├─ PostsController.cs
├─ DTOs/ # Data Transfer Objects y validadores
│ ├─ UserRegisterDto.cs
│ ├─ UserLoginDto.cs
│ ├─ UserResponseDto.cs
│ ├─ Validators/
├─ Data/ # Contexto de base de datos
│ ├─ AppDbContext.cs
├─ Services/ # Lógica de negocio
│ ├─ UserService.cs
│ ├─ JwtService.cs
├─ Helpers/ # Regex y constantes
│ ├─ RegexSettings.cs
├─ Program.cs # Configuración principal y DI
├─ appsettings.json # Configuración de JWT y otros
└─ README.md
