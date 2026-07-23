
# TimeSeriesProcessing

Приложение для сбора и обработки входных данных из csv-файлов

## Содержание
- [TimeSeriesProcessing](#timeseriesprocessing)
    - [Содержание](#содержание)
    - [Требования](#требования)
    - [Используемые пакеты и версии](#используемые-пакеты-и-версии)
    - [Структура](#структура)
    - [Быстрый старт](#быстрый-старт)
    - [Основные возможности](#основные-возможности)
    - [API Документация](#api-документация)
    - [Автор](#автор)

##  Требования
- .NET 10 SDK
- Docker и Docker Compose
- Доступ к сети для загрузки образа базы данных
- CSV-файлы с заголовками: <Время начала ГГГГ-ММ-ДДTчч-мм-сс.ммммZ>;<Время выполнения в секундах>;<Показатель в виде числа с
  плавающей запятой>

## Используемые пакеты и версии

- Microsoft.NET.Sdk / Target framework: net10.0
- Microsoft.AspNetCore.OpenApi 10.0.9
- Microsoft.EntityFrameworkCore 10.0.10
- Microsoft.EntityFrameworkCore.Abstractions 10.0.10
- Microsoft.EntityFrameworkCore.Design 10.0.10
- Microsoft.EntityFrameworkCore.Tools 10.0.10
- Npgsql.EntityFrameworkCore.PostgreSQL 10.0.2
- Swashbuckle.AspNetCore.SwaggerGen 10.2.3
- Swashbuckle.AspNetCore.SwaggerUI 10.2.3
- CsvHelper 33.1.0
- Microsoft.NET.Test.Sdk 18.0.0
- xunit 2.5.3
- xunit.runner.visualstudio 2.5.3

## Структура

```
TimeSeriesProcessing
│
├── tests                                    → Папка с тест-кейсами(csv)
├── TimeSeriesProcessing.Api                 → Web API (Controllers, Middleware)
├── TimeSeriesProcessing.Application         → Бизнес-логика, DTO, контракты
├── TimeSeriesProcessing.Domain              → Модели и DbContext
├── TimeSeriesProcessing.Infrastructure      → Репозитории, реализация зависимостей
├── TimeSeriesProcessing.Tests               → Тестовый проект
├── .env.example                             → Пример конфигурации
├── compose.yaml                             → Оркестрация контейнеров
└── README.md                                → Этот файл
```

## Быстрый старт:

Клонировать репозиторий:

```bash
git clone https://github.com/TXMMYBXY/TimeSeriesProcessing.git
```

Настроить файл конфигурации `.env` и запустить контейнеры

```bash
docker compose up --build
```

Чтобы удалить контейнеры и тома, используйте

```bash
docker compose down -v
```
## Основные возможности

- Загрузка csv-файлов на сервер. При загрузке, данные валидируется и высчитываются результаты по данным из файла;
- Если при загрузке файла, в БД есть запись с таким же названием, то удаляется старый результат и каскадно все значения. После этого записываются новые данные:
- Возврат результатов с фильтрами, сортировкой и пагинацией;
- Возврат последних 10 записей, отсортированных по дате, из файла.

## API Документация

<details>
<summary>Документация</summary>

```json
{
  "openapi": "3.0.4",
  "info": {
    "title": "TimeSeriesProcessing API",
    "description": "The Time Series Processing API",
    "version": "v1"
  },
  "paths": {
    "/api/results": {
      "get": {
        "tags": [
          "Result"
        ],
        "parameters": [
          {
            "name": "OrderBy",
            "in": "query",
            "schema": {
              "$ref": "#/components/schemas/ResultSortField"
            }
          },
          {
            "name": "Descending",
            "in": "query",
            "schema": {
              "type": "boolean"
            }
          },
          {
            "name": "FileName",
            "in": "query",
            "schema": {
              "type": "string"
            }
          },
          {
            "name": "DateTimeFrom",
            "in": "query",
            "schema": {
              "type": "string",
              "format": "date-time"
            }
          },
          {
            "name": "DateTimeTo",
            "in": "query",
            "schema": {
              "type": "string",
              "format": "date-time"
            }
          },
          {
            "name": "AvgValueFrom",
            "in": "query",
            "schema": {
              "type": "number",
              "format": "double"
            }
          },
          {
            "name": "AvgValueTo",
            "in": "query",
            "schema": {
              "type": "number",
              "format": "double"
            }
          },
          {
            "name": "AvgExecutionTimeFrom",
            "in": "query",
            "schema": {
              "type": "number",
              "format": "double"
            }
          },
          {
            "name": "AvgExecutionTimeTo",
            "in": "query",
            "schema": {
              "type": "number",
              "format": "double"
            }
          },
          {
            "name": "PageSize",
            "in": "query",
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          },
          {
            "name": "PageNumber",
            "in": "query",
            "schema": {
              "type": "integer",
              "format": "int32"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "OK",
            "content": {
              "text/plain": {
                "schema": {
                  "$ref": "#/components/schemas/GetResultsResponse"
                }
              },
              "application/json": {
                "schema": {
                  "$ref": "#/components/schemas/GetResultsResponse"
                }
              },
              "text/json": {
                "schema": {
                  "$ref": "#/components/schemas/GetResultsResponse"
                }
              }
            }
          }
        }
      }
    },
    "/ping": {
      "get": {
        "tags": [
          "TimeSeriesProcessing.Api"
        ],
        "responses": {
          "200": {
            "description": "OK",
            "content": {
              "text/plain": {
                "schema": {
                  "type": "string"
                }
              }
            }
          }
        }
      }
    },
    "/api/values/{fileName}": {
      "get": {
        "tags": [
          "Value"
        ],
        "parameters": [
          {
            "name": "fileName",
            "in": "path",
            "required": true,
            "schema": {
              "type": "string"
            }
          }
        ],
        "responses": {
          "200": {
            "description": "OK",
            "content": {
              "text/plain": {
                "schema": {
                  "$ref": "#/components/schemas/GetValuesResponse"
                }
              },
              "application/json": {
                "schema": {
                  "$ref": "#/components/schemas/GetValuesResponse"
                }
              },
              "text/json": {
                "schema": {
                  "$ref": "#/components/schemas/GetValuesResponse"
                }
              }
            }
          }
        }
      }
    },
    "/api/values/upload": {
      "post": {
        "tags": [
          "Value"
        ],
        "requestBody": {
          "content": {
            "multipart/form-data": {
              "schema": {
                "required": [
                  "File"
                ],
                "type": "object",
                "properties": {
                  "File": {
                    "type": "string",
                    "format": "binary"
                  }
                }
              },
              "encoding": {
                "File": {
                  "style": "form"
                }
              }
            }
          }
        },
        "responses": {
          "200": {
            "description": "OK"
          }
        }
      }
    }
  },
  "components": {
    "schemas": {
      "GetResultsResponse": {
        "type": "object",
        "properties": {
          "results": {
            "type": "array",
            "items": {
              "$ref": "#/components/schemas/ResultItemResponse"
            },
            "nullable": true
          },
          "totalCount": {
            "type": "integer",
            "format": "int32"
          },
          "pageSize": {
            "type": "integer",
            "format": "int32"
          },
          "currentPage": {
            "type": "integer",
            "format": "int32"
          },
          "totalPages": {
            "type": "integer",
            "format": "int32",
            "readOnly": true
          }
        },
        "additionalProperties": false
      },
      "GetValuesResponse": {
        "type": "object",
        "properties": {
          "values": {
            "type": "array",
            "items": {
              "$ref": "#/components/schemas/ValueItemResponse"
            },
            "nullable": true
          }
        },
        "additionalProperties": false
      },
      "ResultItemResponse": {
        "type": "object",
        "properties": {
          "id": {
            "type": "integer",
            "format": "int32"
          },
          "fileName": {
            "type": "string",
            "nullable": true
          },
          "deltaSeconds": {
            "type": "integer",
            "format": "int32"
          },
          "minDate": {
            "type": "string",
            "format": "date-time"
          },
          "avgExecutionTime": {
            "type": "number",
            "format": "double"
          },
          "avgValue": {
            "type": "number",
            "format": "double"
          },
          "medianValue": {
            "type": "number",
            "format": "double"
          },
          "minValue": {
            "type": "number",
            "format": "double"
          },
          "maxValue": {
            "type": "number",
            "format": "double"
          }
        },
        "additionalProperties": false
      },
      "ResultSortField": {
        "enum": [
          0,
          1,
          2,
          3,
          4,
          5,
          6,
          7
        ],
        "type": "integer",
        "format": "int32"
      },
      "ValueItemResponse": {
        "type": "object",
        "properties": {
          "id": {
            "type": "integer",
            "format": "int32"
          },
          "date": {
            "type": "string",
            "format": "date-time"
          },
          "executionTime": {
            "type": "integer",
            "format": "int32"
          },
          "value": {
            "type": "number",
            "format": "double"
          }
        },
        "additionalProperties": false
      }
    }
  },
  "tags": [
    {
      "name": "Result"
    },
    {
      "name": "TimeSeriesProcessing.Api"
    },
    {
      "name": "Value"
    }
  ]
}
```

</details>

## Автор
[Михаил](https://github.com/TXMMYBXY)