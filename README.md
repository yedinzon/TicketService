# TicketService
# Requisitos del Sistema
- .NET 8.0
- SQL Server 2019+

# Pasos de Instalación local
    1- Clonar el repositorio
    2- Configurar conexión a base de datos SQL Server en appsettings.json
    3- Compilar y ejecutar la aplicación. (Las migraciones de db y datos iniciales se ejecutan automaticamente)
    4- Acceder a Swagger /swagger

# Instalación y Ejecución con Docker Compose
    1- Clonar el repositorio
    2- Ubicarse en la raiz del proyecto /TicketService (al mismo nivel del archivo Dokerfile y docker-compose.yml)
    2- Iniciar el Stack de Contenedores ejecutando el comando: 
        docker compose up -d --build
    3- Una vez que los contenedores estén operativos, acceder a la documentación de la API en:
        http://localhost:8080/swagger
    4- Limpiar el Entorno (¡Importante!), para detener los contenedores y eliminar la base de datos:
        docker compose down -v

# Descripción general
En este proyecto se implementa un sistema de gestión de tickets utilizando Domain Driven Design (DDD) con una arquitectura limpia y separación de responsabilidades. 
Proporciona una API RESTful para crear, consultar, actualizar y eliminar tickets con soporte de paginación, validaciones, manejo de errores y logs.

El proyecto está organizado en cuatro capas principales siguiendo los principios de arquitectura limpia:

- Domain Layer: Contiene las entidades de negocio, enums y lógica de dominio fundamental.

- Application Layer: Implementa los casos de uso, servicios de aplicación (Ticket), DTOs, abstracciones del repositorio(interfaces) y configuración de mapeo (AutoMapper)

- Infrastructure Layer: Gestiona la persistencia de datos, implementaciones de repositorios, uso de entity framework, configuración de entidades, migraciones, seed de data inicial.

API Layer: Expone los endpoints RESTful, controladores, configuración de mapeo (AutoMapper) entre requests/response y dtos de la aplicación, validaciones (FluentValidation), middleware de errores,  y configuración de la aplicación

# Justificación del enfoque del proyecto
La arquitectura DDD y frameworks como AutoMapper, FluentValidation, Entity Framework, MStest, NSubstitute y Serilog se utilizan solo para exponer el dominio de patrones modernos. Su uso, simplicidad o complejidad siempre dependerá de las necesidades específicas del proyecto. Esta base podría incluso extenderse con más detalle en logs o autenticación JWT si los requisitos lo exigieran. Tambien se podría incluir test unitarios y de integración usando TestContainers.

# Endpoids disponibles
    GET/api/tickets/all         Lista de todos los tickets	
    GET/api/tickets             Lista paginada de tickets	
    GET/api/tickets/{id}	    Obtener ticket específico	
    POST/api/tickets	        Crear nuevo ticket	
    PUT/api/tickets/{id}	    Actualización completa	
    PATCH/api/tickets/{id}	    Actualización parcial
    DELETE/api/tickets/{id}	    Eliminar ticket

El sistema implementa el estándar JSON Patch
    - Operacion Soportada: replace
    - Ejemplo de JSON: 

    [
      {
        "op":"replace",
        "path":"/usuario",
        "value":"nuevoUsuario"},
      {
        "op":"replace",
        "path":"/estado",
        "value":"Closed"
      }
    ]

# Convenciones del proyecto
Este proyecto implementa las siguientes buenas prácticas de desarrollo, principios SOLID, convención de una clase por archivo para mejorar la mantenibilidad, el uso de camelCase para variables y parámetros, PascalCase para tipos y propiedades, y la adopción del inglés como lenguaje estándar en el código, se utiliza el español exclusivamente para los mensajes de validación dirigidos al usuario y los nombres de campos en los DTOs de request y response.

# Logging
    Archivos de Log: Registro automático en directorio Logs/
    Rotación Diaria: Archivos separados por fecha

# Unit Testing
    MSTest: Framework de testing principal
    NSubstitute: Librería para mocking de dependencias
    Cobertura: Tests para capas Domain y Application

# Arbol del proyecto
    TicketService/
        TicketService.Domain/          # Dominio - Entidades y Lógica de Negocio
        TicketService.Application/     # Casos de Uso - Lógica de Aplicación
        TicketService.Infrastructure/  # Infraestructura - Persistencia
        TicketService.API/             # Presentación - Web API, Controllers
        TicketService.Tests/           # Test unitarios

    TicketService.Tests/
        ApplicationTests/      # Tests unitarios de servicios de aplicación
        DomainTests/           # Tests unitarios de entidades

# Frameworks Utilizados:
# FluentValidation
Se usa para validar los datos de entrada en las peticiones de los endpoints (RequestsDtos)

# AutoMapper
    El proyecto utiliza AutoMapper para transformar objetos entre las diferentes capas:
    - Requests -> Dtos
    - Domain Entities -> DTOs
    - DTOs -> Responses

# Entity Framework
    Entity Framework Core: ORM principal para la persistencia de datos
    SQL Server: Base de datos relacional utilizada
    Code-First Approach: El modelo de dominio define la estructura de la base de datos
    Migraciones Automatizadas: Control de versiones del esquema de base de datos
    Seed Data: Capacidad para inicializar datos de la db

# Serilog
Para registrar logs de errores globales en archivo local