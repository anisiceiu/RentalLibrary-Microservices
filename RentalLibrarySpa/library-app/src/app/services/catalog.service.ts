import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from '../models/category';
import { Binding } from '../models/binding';
import { Book } from '../models/book';

@Injectable({
  providedIn: 'root'
})
export class CatalogService {

  private baseUrl = 'https://localhost:7225'; // Replace with your API base URL

  constructor(private http: HttpClient) {}

  public getBooks()
  {
    return this.http.get<any>(`${this.baseUrl}/catalog/getbooks`);
  }

  public getCategories()
  {
    return this.http.get<any>(`${this.baseUrl}/catalog/GetCategories`);
  }
  
  public getBindings()
  {
    return this.http.get<any>(`${this.baseUrl}/catalog/GetBindings`);
  }

  public addCategory(category:Category)
  {
    return this.http.post<any>(`${this.baseUrl}/catalog/AddCategory`,category);
  }
  
  public addBinding(binding:Binding)
  {
    return this.http.post<any>(`${this.baseUrl}/catalog/AddBinding`,binding);
  }

  public addBook(book:Book)
  {
    const formData = new FormData();
    formData.append('title', book.title);
    formData.append('author', book.author);
    formData.append('iSBN', book.iSBN);
    formData.append('publishYear', book.publishYear.toString());
    formData.append('language', book.language);
    formData.append('noOfCopies', book.noOfCopies.toString());
    formData.append('noOfAvailableCopies', book.noOfAvailableCopies.toString());
    formData.append('bindingId', book.bindingId.toString());
    formData.append('categoryId', book.categoryId.toString());
    formData.append('formFile', book.formFile);

    return this.http.post<any>(`${this.baseUrl}/catalog/AddBook`,formData);
  }

}
