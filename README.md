# Yeeff Barber Studio - Sistema de Citas

## Descripción

Aplicación de escritorio desarrollada en C# con Windows Forms para la gestión de citas de la barbería **Yeeff Barber Studio**. Permite a los clientes agendar servicios de barbería y al administrador visualizar las citas registradas en la base de datos.

## Tecnologías utilizadas

- **Lenguaje**: C# (.NET 10.0)
- **Framework**: Windows Forms
- **Base de datos**: SQL Server Express (SQLEXPRESS)
- **Bibliotecas**: System.Data.SqlClient 4.9.1

## Características

### Formulario de Registro de Citas (Form1)
- Registro de cliente con nombre completo y teléfono
- Selección de servicios disponibles:
  - Corte de pelo
  - Cerquillo y barba
  - Corte de niños
- Selección de fecha y hora para la cita mediante controles de fecha y lista desplegable
- Validación de campos obligatorios antes de confirmar la cita
- Interfaz con textos de ayuda (placeholder) en los campos de entrada que desaparecen al escribir

### Visualización de Citas (CitasForm)
- Muestra todas las citas registradas en una tabla interactiva
- Columnas de la tabla: Cliente, Teléfono, Servicios, Fecha y Hora
- Botón de actualización para refrescar la lista de citas en tiempo real
- Botón para volver al formulario principal
- Diseño visual oscuro con acentos en color dorado (RGB 212, 175, 55) que refleja la identidad de la barbería

## Estructura de la Base de Datos

La aplicación utiliza una base de datos SQL Server Express llamada `YeeffBarberDb` con la siguiente tabla:

**Tabla: Citas**
| Columna | Tipo | Descripción |
|---------|------|-------------|
| Id | INT IDENTITY(1,1) | Identificador único de la cita (autoincremental) |
| NombreCliente | NVARCHAR(100) | Nombre completo del cliente |
| TelefonoCliente | NVARCHAR(20) | Número de teléfono de contacto del cliente |
| ServicioID | NVARCHAR(200) | Servicios solicitados por el cliente |
| FechaHora | DATETIME | Fecha y hora programada para la cita |

**Cadena de conexión**: `Server=localhost\SQLEXPRESS;Database=YeeffBarberDb;Integrated Security=True;TrustServerCertificate=True;`

## Requisitos previos

- Visual Studio 2022 o versiones superiores
- SDK de .NET 10.0
- SQL Server Express instalado con el servicio SQLEXPRESS en ejecución
- Sistema operativo Windows 10 u 11

## Instalación y ejecución

1. Clonar el repositorio en tu equipo local
2. Abrir el archivo del proyecto en Visual Studio
3. Verificar que el servicio SQL Server Express esté corriendo correctamente
4. Compilar el proyecto y ejecutarlo presionando F5 o el botón de inicio

La aplicación crea automáticamente la tabla `Citas` en la base de datos al iniciar, en caso de que no exista previamente.

## Estructura del proyecto

```
YeeffBarber_AppointmentSystem/
├── Form1.cs                    # Formulario principal de registro de citas
├── Form1.Designer.cs           # Código generado automáticamente para el diseño del formulario principal
├── CitasForm.cs                # Formulario para visualizar todas las citas registradas
├── Database.cs                 # Clase con la lógica de acceso y operaciones a la base de datos
├── Program.cs                  # Punto de entrada principal de la aplicación
└── YeeffBarber_AppointmentSystem.csproj  # Archivo de configuración del proyecto
```

## Autor

Yeeff Barber Studio - Proyecto desarrollado para la materia de Programación IV

## Notas adicionales

- La aplicación utiliza autenticación de Windows (Integrated Security) para conectarse a SQL Server
- El diseño visual está optimizado para una resolución de 393x852 píxeles (formato móvil)
- Se requiere que el servidor SQL Server Express esté disponible localmente para el funcionamiento correcto de la aplicación
