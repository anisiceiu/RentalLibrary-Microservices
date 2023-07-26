import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

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
}
