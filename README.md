# Pizza Store API

24 Hour C# .Net API Challenge / Project

## Overview

The Pizza Store API is a RESTful service designed to manage and analyze sales data for a pizza store. It allows for querying of pizza details, total sales per pizza within a specified date range and supports filtering by pizza ID, it also allows update pizza details

## API Endpoints

### Get Total Sales of Each Pizza

- **URL:** `/api/data/total-sales`
- **Method:** `GET`
- **Query Parameters:**

  - `startDate` (optional): Start date for filtering sales data (format: `yyyy-MM-dd`)
  - `endDate` (optional): End date for filtering sales data (format: `yyyy-MM-dd`)
  - `pizzaId` (optional): ID of the pizza to filter by

- **Response:**

  - **200 OK**: Returns a list of pizzas with their total sales information.
  - **400 Bad Request**: If the `startDate` is after the `endDate`.

- **Example Request:**

  ```http
  GET /api/data/total-sales?startDate=2015-01-01&endDate=2015-05-31

  ```

- **Example Request:**
  ```http
  [
      {
        "PizzaId": "1",
        "PizzaName": "Margherita",
        "TotalSales": 150
      },
      {
        "PizzaId": "2",
        "PizzaName": "Pepperoni",
        "TotalSales": 200
      }
  ]
  ```

### Get Pizza Details

- **URL:** `/api/data/pizza/{id}`
- **Method:** `GET`
- **URL Params:**

  - `id` (required): ID of the pizza

- **Response:**

  - **200 OK**: Returns the details of the pizza.
  - **400 Bad Request**: If the pizza with the given ID does not exist.

- **Example Request:**

  ```http
  GET /api/data/pizza/bbq_ckn_m

  ```

- **Example Response:**
  ```http
  {
    "pizza_id": "1",
    "size": "Large",
    "price": 12.99,
    "Ingredients": "Tomato, Mozzarella, Basil"
  }
  ```

### Get Pizzas with Pagination

- **URL:** `/api/data/pizzas`
- **Method:** `GET`
- **Query Parameters:**

  - `pageNumber` (optional): Page number for pagination (default: 1)
  - `pageSize` (optional): Number of pizzas per page (default: 10)

- **Response:**

  - **200 OK**: Returns a paginated list of pizzas along with total count..

- **Example Request:**

  ```http
  GET /api/data/pizzas?pageNumber=1&pageSize=5

  ```

- **Example Response:**
  ```http
  {
    "TotalCount": 50,
    "PageNumber": 1,
    "PageSize": 5,
    "Pizzas": [
      {
        "pizza_id": "1",
        "size": "Large",
        "price": 12.99,
        "Ingredients": "Tomato, Mozzarella, Basil"
      },
      {
        "pizza_id": "2",
        "size": "Medium",
        "price": 10.99,
        "Ingredients": "Tomato, Pepperoni"
      }
    ]
  }
  ```

### Update Pizza

- **URL:** `/api/data/update-pizza`
- **Method:** `PUT`
- **Query Parameters:**

  - `pizzaId ` (required): ID of the pizza to update
  - `newPrice ` (optional): New price of the pizza
  - `size` (optional): New size of the pizza

- **Response:**

  - **200 OK**: If the update was successful.
  - **400 Bad Request**: If no changes were made or if required parameters are missing.
  - **404 Not Found**: If the pizza with the given ID does not exist.

- **Example Request:**
  ```http
  PUT /api/data/update-pizza?pizzaId=1&newPrice=13.99&ingredients=Tomato, Mozzarella, Basil, Garlic
  ```

## Contact

For any questions or support, please contact

- **Email:** lawrencepedro.lp@gmail.com
- **LinkedIn:** https://www.linkedin.com/in/lawrence-pedro-095151238/

- **Github:** https://github.com/pedropogi33/
