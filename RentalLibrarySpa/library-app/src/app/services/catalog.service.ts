import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from '../models/category';
import { Binding } from '../models/binding';

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

  public addCategory(category:Category)
  {
    return this.http.post<any>(`${this.baseUrl}/catalog/AddCategory`,category);
  }
  
  public addBinding(binding:Binding)
  {
    return this.http.post<any>(`${this.baseUrl}/catalog/AddBinding`,binding);
  }
}
