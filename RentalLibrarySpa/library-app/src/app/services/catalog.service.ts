import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CatalogService {

  private baseUrl = 'https://localhost:7225'; // Replace with your API base URL

  constructor(private http: HttpClient) {}

  public getRoles()
  {
    return this.http.get<any>(`${this.baseUrl}/account/getroles`);
  }
}
