# Video Platform Services

Este proyecto implementa una plataforma de ejemplo de servicios de video similar a YouTube, con funcionalidades para gestionar usuarios, videos, comentarios y suscripciones.

## Estructura del Proyecto

El proyecto está organizado en los siguientes servicios principales:

- UserService: Gestión de usuarios
- VideoService: Gestión de videos
- CommentService: Gestión de comentarios
- SubscriptionService: Gestión de suscripciones entre usuarios

## Casos de Uso y Cobertura de Pruebas

### UserService

#### Casos de Uso:
1. Registro de nuevo usuario
2. Obtención de perfil de usuario
3. Actualización de datos de usuario
4. Eliminación de cuenta
5. Listado de usuarios
6. Validación de datos únicos (email/username)

#### Pruebas Implementadas:
- `CreateUser_ValidData_ShouldCreateUser`: Verifica el registro exitoso
- `CreateUser_InvalidData_ShouldThrowException`: Valida datos requeridos
- `CreateUser_DuplicateEmail_ShouldThrowException`: Verifica unicidad de email
- `UpdateUser_ValidData_ShouldUpdateUser`: Confirma actualización correcta
- `DeleteUser_ExistingUser_ShouldRemoveUser`: Verifica eliminación de cuenta

### VideoService

#### Casos de Uso:
1. Publicación de nuevo video
2. Visualización de video
3. Búsqueda de videos
4. Actualización de información
5. Eliminación de video
6. Conteo de vistas
7. Listado de videos tendencia

#### Pruebas Implementadas:
- `CreateVideo_ValidData_ShouldCreateVideo`: Verifica publicación correcta
- `SearchVideos_ShouldReturnMatchingVideos`: Valida búsqueda efectiva
- `IncrementViews_ShouldIncreaseViewCount`: Confirma conteo de vistas
- `GetTrendingVideos_ShouldReturnMostViewedVideos`: Verifica algoritmo de tendencias

### CommentService

#### Casos de Uso:
1. Añadir comentario
2. Moderar comentarios
3. Listar comentarios por video
4. Listar comentarios por usuario
5. Actualizar comentario
6. Eliminar comentario

#### Pruebas Implementadas:
- `AddComment_ValidData_ShouldAddComment`: Verifica comentario exitoso
- `GetVideoComments_ShouldReturnCommentsOrderedByDate`: Valida ordenamiento
- `UpdateComment_ValidData_ShouldUpdateContent`: Confirma edición
- `DeleteComment_ExistingComment_ShouldRemoveComment`: Verifica eliminación

### SubscriptionService

#### Casos de Uso:
1. Suscripción a canal
2. Cancelación de suscripción
3. Listado de suscriptores
4. Listado de suscripciones
5. Verificación de estado de suscripción

#### Pruebas Implementadas:
- `Subscribe_ShouldCreateNewSubscription`: Verifica suscripción exitosa
- `Subscribe_ToSelf_ShouldThrowException`: Valida reglas de negocio
- `Unsubscribe_ShouldRemoveSubscription`: Confirma cancelación
- `GetSubscriberCount_ShouldReturnCorrectCount`: Verifica conteo

## Principios de Pruebas

Las pruebas unitarias siguen estos principios:

1. **Arranging**: Preparación del contexto necesario
2. **Acting**: Ejecución de la funcionalidad a probar
3. **Asserting**: Verificación de resultados esperados

Cada prueba:
- Tiene un nombre descriptivo que indica el escenario
- Prueba un único caso de uso específico
- Incluye validaciones positivas y negativas
- Verifica comportamientos de borde

## Cobertura de Pruebas

Cada servicio incluye pruebas para:
- Flujos exitosos (happy paths)
- Manejo de errores
- Validaciones de datos
- Reglas de negocio
- Casos límite

## Ejecución de Pruebas

Las pruebas utilizan xUnit y pueden ejecutarse con:

```bash
dotnet test
```
