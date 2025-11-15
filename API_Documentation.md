# Pet Rating and Commenting API Documentation

This document provides details on the API endpoints for rating and commenting on pets.

---

## Rating a Pet

### Update Pet Rating

- **Endpoint:** `PUT /api/pets/{id}/rating`
- **Description:** Updates the rating of a specific pet.
- **Request Body:**
  ```json
  {
    "rating": 4
  }
  ```
- **Response:**
  - **204 No Content:** If the update is successful.
  - **400 Bad Request:** If the rating value is invalid (e.g., not between 1 and 5).
  - **404 Not Found:** If the pet with the specified ID does not exist.

---

## Commenting on a Pet

### Add a Comment to a Pet

- **Endpoint:** `POST /api/pets/{id}/comments`
- **Description:** Adds a new comment to a specific pet.
- **Request Body:**
  ```json
  {
    "text": "This is a great pet!"
  }
  ```
- **Response:**
  - **201 Created:** If the comment is successfully created. Returns the newly created comment object.
  ```json
  {
    "id": 123,
    "text": "This is a great pet!",
    "petId": 1,
    "userId": 1
  }
  ```
  - **404 Not Found:** If the pet with the specified ID does not exist.

### Get Comments for a Pet

- **Endpoint:** `GET /api/pets/{id}/comments`
- **Description:** Retrieves a paginated list of comments for a specific pet.
- **Query Parameters:**
  - `pageNumber` (optional, default: 1): The page number to retrieve.
  - `pageSize` (optional, default: 10): The number of comments per page.
- **Response:**
  - **200 OK:** Returns a paginated list of comments.
  ```json
  {
    "pageNumber": 1,
    "pageSize": 10,
    "totalPages": 5,
    "totalRecords": 50,
    "data": [
      {
        "id": 123,
        "text": "This is a great pet!",
        "petId": 1,
        "userId": 1
      },
      {
        "id": 124,
        "text": "So cute!",
        "petId": 1,
        "userId": 2
      }
    ]
  }
  ```
  - **404 Not Found:** If the pet with the specified ID does not exist.
