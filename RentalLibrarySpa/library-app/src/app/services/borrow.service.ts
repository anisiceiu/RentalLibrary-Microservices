import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReserveRequest } from '../models/request';

@Injectable({
  providedIn: 'root'
})
export class BorrowService {

  private baseUrl = 'https://localhost:7225'; // Replace with your API base URL

  constructor(private http: HttpClient) {}
  
  public addDays(date:Date, days:number):Date {
    var result = new Date(date);
    result.setDate(result.getDate() + days);
    return result;
  }

  public reseveBook(request:ReserveRequest)
  {
    return this.http.post<any>(`${this.baseUrl}/Borrow/BookReserveRequest`,request);
  }

}
